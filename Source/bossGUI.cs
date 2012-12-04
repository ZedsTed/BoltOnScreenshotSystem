using System;
using UnityEngine;
using KSP.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class bossGUI : MonoBehaviour
{
	
	private static BOSS _boss;
	private BOSS boss 
	{
		get
		{
			if (_boss == null)
			_boss = new BOSS();
			return _boss;
		}

	}
	
	//public BOSS boss = new BOSS();
	
	public void mainGUI ()
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
			if (boss.burstMode == true)
			{
				boss.burstModeMethod();	
			}
		else{boss.screenshotMethod();}
		
		}			
		boss.showHelp = GUILayout.Toggle(boss.showHelp,"+", GUILayout.ExpandWidth(true));		
		GUILayout.EndHorizontal();                
		GUI.DragWindow(new Rect(0, 0, 10000, 20));		
	}
	
	public void helpGUI ()
	{
		GUILayout.BeginVertical();
		GUILayout.Label("Current supersample value: " + boss.superSampleValueInt.ToString(), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
		GUILayout.Label("Supersample value: ");
		boss.superSampleValueString = GUILayout.TextField(boss.superSampleValueString);	
		try
		{
			boss.superSampleValueInt = Int32.Parse(boss.superSampleValueString);
			boss.i = 0;
		}
		catch
		{
			while (boss.i < 1)
			{ // stops the catch from spamming the debug log.
				Debug.Log("You haven't entered an integer.");
				boss.i++;
			}
		}
		GUILayout.Label("You have taken " + boss.screenshotCount + " screenshots.");
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
}


