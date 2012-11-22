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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.IO;



namespace BOSS
{
	public class BOSS : Part
	{	
    	public Vessel activeVessel;
		protected Rect windowPos;
		protected Rect helpWindowPos;
		private string kspDir;
		private string kspDir2;
		public int screenshotCount,	superSampleValueInt = 1;
		public string superSampleValueString = "1";
		public string screenshotKey = "z";
//		private string screenshotPersistence = "screenshotPersistence.txt";
		private string pluginFolder = "PluginData/BOSS/";
//		private string screenshotCountString;
		public bool showHelp = false;
		//private static int lastFrame = -1;	
		int i;
		
		
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
									
						screenshotMethod();			
				}	
				
				
				
	    		
				showHelp = GUILayout.Toggle(showHelp,"+", new GUIStyle(GUI.skin.button), GUILayout.ExpandWidth(true));
			
				
				GUILayout.EndHorizontal();                
	            GUI.DragWindow(new Rect(0, 0, 10000, 20));		
			
	 
		}	
		
		private void helpGUI(int WindowID)
		{	
			GUILayout.BeginVertical();
			GUILayout.Label("Warning: Don't set SS too high, it can crash KSP.");
			GUILayout.Label("Supersampling is currently set to: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
			GUILayout.Label("Please enter your desired supersample value below: ");
			superSampleValueString = GUILayout.TextField(superSampleValueString);	
			try
			{
				superSampleValueInt = Int32.Parse(superSampleValueString);
				i = 0;
			}
			catch
			{
				while (i < 1){ // stops the catch from spamming the debug log.
				print("You haven't entered an integer.");
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
		
		private void screenshotMethod()
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
            	GUI.skin = HighLogic.Skin;			
            	windowPos = GUILayout.Window(569, windowPos, WindowGUI, "B.O.S.S.", GUILayout.Width(120));
				
				
				 if (showHelp) helpWindowPos = GUILayout.Window(568, helpWindowPos, helpGUI,
                        "More Info.", GUILayout.Width(150), GUILayout.Height(150));
			
		}
		
				
		protected override void onFlightStart()  //Called when vessel is placed on the launchpad
		{				
			kspDir2 = KSPUtil.ApplicationRootPath;			//Thank you to Innsewerants for this bit of code from his mapsat plugin.
			int lastIndex = kspDir2.LastIndexOf('/');
			if (lastIndex != -1)
			{
				kspDir = kspDir2.Substring(0, lastIndex + 1);
			}
			
		   	RenderingManager.AddToPostDrawQueue(3, new Callback(drawGUI));//start the GUI
			
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
		
		
		
		protected override void onPartStart()
		{	
			if ((windowPos.x == 0) && (windowPos.y == 0))//windowPos is used to position the GUI window, lets set it in the center of the screen
				{
          	  		windowPos = new Rect(Screen.width / 2, Screen.height / 2, 10, 10);
   				}			
		}		
			
		
		protected override void onPartUpdate()
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
			print("Loaded BOSS settings.");
			
			
        }		
		
		
//--onPartDestroy is currently not in use as I want people to be able to take screenshots even after their craft is destroyed---//
//	 	protected override void onPartDestroy() 
//		{
//			RenderingManager.RemoveFromPostDrawQueue(3, new Callback(drawGUI)); //close the GUI
//  		}
 
	
	}
}



