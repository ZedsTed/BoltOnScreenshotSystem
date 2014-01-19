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
using Toolbar;
using UnityEngine;
using KSP.IO;

[KSPAddon(KSPAddon.Startup.EveryScene, false)]
public class BOSS : MonoBehaviour
{
    public Vessel activeVessel;
    protected Rect windowPos, helpWindowPos;
    private string kspDir2 = KSPUtil.ApplicationRootPath + @"GameData/BOSS/PluginData/BOSS 2.0/";
    public int screenshotCount, superSampleValueInt = 1, i;
    public string superSampleValueString = "1", screenshotKey = "z",  showGUIKey = "p";
    public bool showHelp = false, burstMode = false, showGUI = false;
    private IButton button1;
    public BOSSSettings BOSSsettings = new BOSSSettings();


    public void Awake()
    {
        if(!File.Exists<BOSS>(kspDir2 + "config.xml"))
        {
            try {createBOSSsettings(); }
            catch{throw new AccessViolationException("Can't create settings file, please confirm directory is writeable.");}  
        }
        loadBOSSsettings();
        initToolbar();
        RenderingManager.AddToPostDrawQueue(60, new Callback(drawGUI));
    }

    private void initToolbar()
    {
        button1 = ToolbarManager.Instance.add("BOSS", "button1");
        button1.TexturePath = showHelp ? "BOSS/bon" : "BOSS/boff";
        button1.ToolTip = "Toggle Bolt-On Screenshot System";
        button1.OnClick += (e) =>
            {
                    if (showHelp) showHelp = false;
                    else if (!showHelp) showHelp = true;
                    button1.TexturePath = showHelp ? "BOSS/bon" : "BOSS/boff";
                    saveBOSSsettings();
            };
    }

    private void helpGUI(int WindowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Current supersample value: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.Label("Supersample value: ");
        
        if(!int.TryParse(superSampleValueString, out superSampleValueInt))
        {
            superSampleValueString = " ";
        }
        superSampleValueString = GUILayout.TextField(superSampleValueString);
        
        GUILayout.Label(screenshotCount + " screenshots taken.");
    
        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void screenshotMethod()
    {
        string screenshotFilename = "Screenshot" + screenshotCount;
        print("Screenshot button pressed!");
        print(screenshotFilename);
        print(screenshotCount);
        print(KSPUtil.ApplicationRootPath);
        print(kspDir2);
        print(kspDir2 + screenshotFilename + ".png");
        print("Your supersample value was " + superSampleValueInt + "!");
        Application.CaptureScreenshot(kspDir2 + screenshotFilename + ".png", superSampleValueInt);
        screenshotCount++;
        saveBOSSsettings();
    }

    private void drawGUI()
    {
        GUI.skin = HighLogic.Skin;
        if (showHelp) helpWindowPos = GUILayout.Window(568, helpWindowPos, helpGUI, "More Info.", GUILayout.Width(150), GUILayout.Height(150));
    }

    private void createBOSSsettings()
    {
        BOSSsettings.SetValue("BOSS::screenshotCount", "0");
        BOSSsettings.SetValue("BOSS::windowPos.x", "250");
        BOSSsettings.SetValue("BOSS::windowPos.y", "250");
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", "400");
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", "400");
        BOSSsettings.SetValue("BOSS::showHelp", "True");
        BOSSsettings.SetValue("BOSS::showGUI", "True");
        BOSSsettings.SetValue("BOSS::screenshotKey", "z");
        BOSSsettings.SetValue("BOSS::showGUIKey", "p");
        BOSSsettings.Save();
        print("Created BOSS settings.");
    }

    private void saveBOSSsettings()
    {
        BOSSsettings.SetValue("BOSS::screenshotCount", screenshotCount.ToString());
        BOSSsettings.SetValue("BOSS::windowPos.x", windowPos.x.ToString());
        BOSSsettings.SetValue("BOSS::windowPos.y", windowPos.y.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", helpWindowPos.x.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", helpWindowPos.y.ToString());
        BOSSsettings.SetValue("BOSS::showHelp", showHelp.ToString());
        BOSSsettings.SetValue("BOSS::showGUI", showGUI.ToString());
        BOSSsettings.SetValue("BOSS::screenshotKey", screenshotKey);
        BOSSsettings.SetValue("BOSS::showGUIKey", showGUIKey);
        BOSSsettings.Save();
        print("Saved BOSS settings.");
    }

    private void loadBOSSsettings()
    {
        BOSSsettings.Load();
        windowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::windowPos.x"));
        windowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::windowPos.y"));
        helpWindowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.x"));
        helpWindowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.y"));
        screenshotCount = Convert.ToInt32(BOSSsettings.GetValue("BOSS::screenshotCount"));
        showHelp = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showHelp"));
        showGUI = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showGUI"));
        screenshotKey = (BOSSsettings.GetValue("BOSS::screenshotKey"));
        showGUIKey = (BOSSsettings.GetValue("BOSS::showGUIKey"));
        print("Loaded BOSS settings.");
    }
}