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
//--Much thanks to cybutek for his code to fix multiple instances of code being run--//

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
	private string kspDir = KSPUtil.ApplicationRootPath;
	private string kspDir2 = KSPUtil.ApplicationRootPath + @"PluginData/boss/";
	public int screenshotCount,	superSampleValueInt = 1;
	public string superSampleValueString = "1";
	public string screenshotKey = "z";
	//private string screenshotPersistence = "screenshotPersistence.txt";
	private string pluginFolder = "PluginData/BOSS/";
	//private string screenshotCountString;
	public bool showHelp = false;
	//private static int lastFrame = -1;	
	public int i;
	public bool burstMode = false;
	public bool showGUI = false;
	
	 public bool IsPrimary { get; protected set; }
	
	/*private static bossGUI _pluginGUI;
	private bossGUI pluginGUI 
	{
		get
		{
			if (_pluginGUI == null)
			_pluginGUI = new bossGUI();
			return _pluginGUI;
		}

	}
	*/
	
	//public bossGUI pluginGUI = new bossGUI();

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
	
	 protected bool CheckIsPrimary(string className, List<Part> partList)
        {
            // Check if this part is attached to anything.
            if (this.part.parent == null)
                return false;

            // Loop through all parts on the ship.
            foreach (Part part in partList)
            {
                // Check if it is not this part and is an BOSS part.
                if (part != this.part && part.Modules.Contains(className))
                {
                    // Convert part to type BOSS to access it's properties.
                    BOSS partMod = (part.Modules[className] as BOSS);

                    // Check if the part is primary already.
                    if (partMod.IsPrimary)
                    {
                        // If the part is primary, return that this part cannot be primary.
                        print("BOSS: CheckIsPrimary(" + className + ") = false");
                        return false;
                    }
                }
            }

            // If no other parts found to be primary, return that this part can be primary.
            print("BOSS: CheckIsPrimary(" + className + ") = true");
            return true;
        }
	
	
	private void WindowGUI(int windowID)		
	{			 
		GUIStyle mainGUI = new GUIStyle(GUI.skin.button); 
		mainGUI.normal.textColor = mainGUI.focused.textColor = Color.white;
		mainGUI.hover.textColor = mainGUI.active.textColor = Color.yellow;
		mainGUI.onNormal.textColor = mainGUI.onFocused.textColor = mainGUI.onHover.textColor = mainGUI.onActive.textColor = Color.green;
		mainGUI.padding = new RectOffset(8, 8, 8, 8);			
		
		
		GUIStyle infoGUI = new GUIStyle(GUI.skin.button);
		infoGUI.normal.textColor = infoGUI.focused.textColor = Color.white;
		infoGUI.hover.textColor = infoGUI.active.textColor = Color.yellow;
		infoGUI.onNormal.textColor = infoGUI.onFocused.textColor = infoGUI.onHover.textColor = infoGUI.onActive.textColor = Color.green;
		infoGUI.padding = new RectOffset(8, 8, 8, 8);
		GUILayout.BeginHorizontal();			
		
		
		
		if (GUILayout.Button("Screenshot",mainGUI,GUILayout.Width(85)))//GUILayout.Button is "true" when clicked
		{	
			if (burstMode == true)
			{
				burstModeMethod();	
			}
		else{screenshotMethod();}
		
		}			
		showHelp = GUILayout.Toggle(showHelp,"+", GUILayout.ExpandWidth(true));		
		GUILayout.EndHorizontal();                
		GUI.DragWindow(new Rect(0, 0, 10000, 20));		
	}	
	
	private void helpGUI(int WindowID)
	{	
		GUILayout.BeginVertical();
		GUILayout.Label("Current supersample value: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
		GUILayout.Label("Supersample value: ");
		superSampleValueString = GUILayout.TextField(superSampleValueString);	
		try
		{
			superSampleValueInt = Int32.Parse(superSampleValueString);
			i = 0;
		}
		catch
		{
			while (i < 1)
			{ // stops the catch from spamming the debug log.
				Debug.Log("You haven't entered an integer.");
				i++;
			}
		}
		GUILayout.Label("You have taken " + screenshotCount + " screenshots.");
		/*burstModeSettingString = GUILayout.TextField(burstModeSetting.ToString());	
		try
		{
		burstModeSetting = Int32.Parse(burstModeSettingString);
		}
		catch
		{
		print("You haven't entered an integer.");
		}*/
		GUILayout.EndVertical();
		GUI.DragWindow(new Rect(0, 0, 10000, 20));	
	}
	
	public void screenshotMethod()
	{		
		string screenshotFilename =  "Screenshot" + screenshotCount;	 
		print("Screenshot button pressed!");
		print(screenshotFilename);
		print(screenshotCount);
		print(KSPUtil.ApplicationRootPath);
		print(kspDir);
		print("Your supersample value was " + superSampleValueInt + "!");
		Application.CaptureScreenshot(kspDir2 + screenshotFilename + ".png", superSampleValueInt);		
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
		if (state != StartState.Editor && this.vessel.rootPart == FlightGlobals.ActiveVessel.rootPart)
		{			
		activeVessel = this.vessel;
		IsPrimary = CheckIsPrimary("BOSS", activeVessel.parts);
		if (IsPrimary)
                {
					
                    // Load the settings for the window position.
                    try
					{
			//			screenshotCount = Int32.Parse(screenshotCountString);
						loadSettings();
					}
					catch
					{
						print("There is an error reading the screenshot persistence file.");	
					}

                    // If no saved position was found, set default.
                    
					if ((windowPos.x == 0) && (windowPos.y == 0))//windowPos is used to position the GUI window, lets set it in the center of the screen
					{
							windowPos = new Rect(Screen.width / 2, Screen.height / 2, 10, 10);
					}	

                    // Add the DrawGUI callback method into the GUI rendering queue.
                   
                    // Instantiate the Rendezvous object with this module.
                    
						RenderingManager.AddToPostDrawQueue(3, new Callback(drawGUI));//start the GUI			
					                   
                    //this.part.force_activate();

                    // Set that the BOSS has now started and exit method.
                    //BOSSStarted = true;
                    return;
                }
		}
		
		
		}
		
		
		
		
		//screenshotCountString = screenshotCount.ToString();
		//			screenshotCountString = KSP.IO.File.ReadAllText<BOSS>(screenshotPersistence, null);
		
	
	
	
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




