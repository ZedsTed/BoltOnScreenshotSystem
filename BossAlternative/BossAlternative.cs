using System;
using UnityEngine;
using KSP.IO;
using System.Collections;

namespace BossAlternative
{
	public class BossAlternative : Part
	{
		private int count = 0;
		private string kspDir;
		private string kspDir2;
		private string pluginFolder = "PluginData/BOSS2/";
		
 
    protected override void onPartUpdate()
    {
        if (Input.GetKeyDown("z"))
            StartCoroutine(ScreenshotEncode());
    }
 
    IEnumerator ScreenshotEncode()
    {
        // wait for graphics to render
        //yield return new WaitForEndOfFrame();
 
        // create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
 
        // put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
		
 
        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;
 
        byte[] bytes = texture.EncodeToPNG();
		
 
        // save our test image (could also upload to WWW)		
        KSP.IO.File.WriteAllBytes<BossAlternative>(bytes, kspDir + pluginFolder + "screenshot" + count + ".png", null);
        count++;
 
        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject( texture );
 
        //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
    }
		
	protected override void onFlightStart()
		{
			kspDir2 = KSPUtil.ApplicationRootPath;			//Thank you to Innsewerants for this bit of code from his mapsat plugin.
			int lastIndex = kspDir2.LastIndexOf('/');
			if (lastIndex != -1)
			{
				kspDir = kspDir2.Substring(0, lastIndex + 1);
			}
		}
	}
}

