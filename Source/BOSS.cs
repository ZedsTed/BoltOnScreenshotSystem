/*   The Bolt-On Screenshot System is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Bolt-On Screenshot System is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with The Bolt-On Screenshot System.  If not, see <http://www.gnu.org/licenses/>.*/

//---Warning, here be spaghetti-code. Read at your own risk, I am not responsible for any fits of rage, strokes or haemorrhages that occur from reading this code.---//

using System;
using UnityEngine;
using KSP.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;





public class BOSS : PartModule
{	
	[KSPField]
	public Vessel activeVessel;
	protected Rect windowPos;
	protected Rect helpWindowPos;
	private string kspDir;
	private string kspDir2;
	public int screenshotCount,	superSampleValueInt = 1;
	public string superSampleValueString = "1";
	public string screenshotKey = "z";
	//private string screenshotPersistence = "screenshotPersistence.txt";
	private string pluginFolder = "PluginData/BOSS/";
	//private string screenshotCountString;
	public bool showHelp = false;
	//private static int lastFrame = -1;	
	public int i;
	public bool burstMode = true;
	public bool showGUI = false;
	public string AGroups;
	
	public bossGUI pluginGUI = new bossGUI();

	[KSPEvent(guiActive=true, guiName="Show GUI")]
	public void ShowGUI() 
	{
		Events["ShowGUI"].active = false;
		Events["HideGUI"].active = true;
		showGUI = true;
	}
	
	[KSPEvent(guiActive=true, guiName="Hide GUI")]
	public void HideGUI() 
	{
		Events["ShowGUI"].active = true;
		Events["HideGUI"].active = false;
		showGUI = false;
	}
	
	private void WindowGUI(int windowID)		
	{			 
		pluginGUI.mainGUI();
	}	
	
	private void helpGUI(int WindowID)
	{	
		pluginGUI.helpGUI ();			
	}
	
	public void screenshotMethod()
	{		
		string screenshotFilename =  "Screenshot" + screenshotCount;	 
		print("Screenshot button pressed!");
		print(screenshotFilename);
		print(screenshotCount);
		print(kspDir);
		print(kspDir2);
		print("Your supersample value was " + superSampleValueInt + "!");
		Application.CaptureScreenshot(kspDir + pluginFolder + screenshotFilename + ".png", superSampleValueInt);		
		screenshotCount++;
		saveSettings();			
	}		
	
	private void drawGUI()
	{		
		//lastFrame = Time.frameCount;	
		if (vessel == FlightGlobals.ActiveVessel)
		{
			if (showGUI == true)
			{
				GUI.skin = HighLogic.Skin;			
				windowPos = GUILayout.Window(569, windowPos, WindowGUI, "B.O.S.S.", GUILayout.Width(120));			
				if (showHelp) helpWindowPos = GUILayout.Window(568, helpWindowPos, helpGUI, "More Info.", GUILayout.Width(150), GUILayout.Height(150));
			}			
		}
	}	
	
	public override void OnStart(StartState state)  //Called when vessel is placed on the launchpad
	{				
		kspDir2 = KSPUtil.ApplicationRootPath;			//Thank you to Innsewerants for this bit of code from his mapsat plugin.
		int lastIndex = kspDir2.LastIndexOf('/');
		if (lastIndex != -1)
		{
		kspDir = kspDir2.Substring(0, lastIndex + 1);
		}
		
		if (state != StartState.Editor)
		{
			RenderingManager.AddToPostDrawQueue(3, new Callback(drawGUI));//start the GUI			
		}
		
		
		if ((windowPos.x == 0) && (windowPos.y == 0))//windowPos is used to position the GUI window, lets set it in the center of the screen
		{
			windowPos = new Rect(Screen.width / 2, Screen.height / 2, 10, 10);
		}	
		//screenshotCountString = screenshotCount.ToString();
		//			screenshotCountString = KSP.IO.File.ReadAllText<BOSS>(screenshotPersistence, null);
		try
		{
			//			screenshotCount = Int32.Parse(screenshotCountString);
			loadSettings();
		}
		catch
		{
			print("There is an error reading the screenshot persistence file.");	
		}
	}
	
	
	/*public override void OnAwake ()
	{
		Debug.Log ("OnAwake()");	
		RenderingManager.AddToPostDrawQueue (3, new Callback(drawGUI));
	}*/
		
	
	
	public override void OnUpdate()
	{
		if (Input.GetKeyDown(screenshotKey))
		{					
			screenshotMethod();		
		}			
	}
	
	public Settings settings = new Settings();
	
	private void saveSettings()
	{	
		settings.SetValue("BOSS::screenshotCount", screenshotCount.ToString());			
		settings.SetValue("BOSS::windowPos.x", windowPos.x.ToString());
		settings.SetValue("BOSS::windowPos.y", windowPos.y.ToString());
		settings.SetValue("BOSS::helpWindowPos.x", helpWindowPos.x.ToString());
		settings.SetValue("BOSS::helpWindowPos.y", helpWindowPos.y.ToString());
		settings.SetValue("BOSS::showHelp", showHelp.ToString());
		settings.SetValue("BOSS::screenshotKey", screenshotKey);
		settings.SetValue("BOSS::showGUI", showGUI.ToString());
		settings.Save();
		print("Saved BOSS settings.");
	}
	
	private void loadSettings()
	{
		settings.Load();			
		windowPos.x = Convert.ToSingle(settings.GetValue("BOSS::windowPos.x"));
		windowPos.y = Convert.ToSingle(settings.GetValue("BOSS::windowPos.y"));
		helpWindowPos.x = Convert.ToSingle(settings.GetValue("BOSS::helpWindowPos.x"));
		helpWindowPos.y = Convert.ToSingle(settings.GetValue("BOSS::helpWindowPos.y"));
		screenshotCount = Convert.ToInt32(settings.GetValue("BOSS::screenshotCount"));			
		showHelp = Convert.ToBoolean(settings.GetValue("BOSS::showHelp"));
		screenshotKey = (settings.GetValue("BOSS::screenshotKey"));
		showGUI = Convert.ToBoolean(settings.GetValue("BOSS::showGUI"));
		print("Loaded BOSS settings.");		
	}
	
	public void burstModeMethod()
	{
		/*Coming soon*/
	}
	
	
	//--onPartDestroy is currently not in use as I want people to be able to take screenshots even after their craft is destroyed---//
	//	 	protected override void onPartDestroy() 
	//		{
	//			RenderingManager.RemoveFromPostDrawQueue(3, new Callback(drawGUI)); //close the GUI
	//  		}


}




