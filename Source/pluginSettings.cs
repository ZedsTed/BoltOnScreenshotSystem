using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.IO;


public class Settings
{
	PluginConfiguration pluginsettings = PluginConfiguration.CreateForType<BOSS>(null);

    public void Load()
    {	
        pluginsettings.load();
    }

    public void Save()
    {	
		pluginsettings.save();
    }

    public void SetValue(string name, string value)
    {			
        pluginsettings.SetValue(name, value);
    }

    public string GetValue(string name)
    {	
		
        return pluginsettings.GetValue<string>(name);
    }
}


