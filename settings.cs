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
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.IO;

namespace BOSS
{
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
}

