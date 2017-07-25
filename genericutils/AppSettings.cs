using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.hs2.genericutils
{
    public class AppSettings
    {
        public static string Get(string appSettingsKey)
        {
            string theValue = null;

            try
            {
                theValue = ConfigurationManager.AppSettings[appSettingsKey];

                if (theValue.ToUpper().Contains("%TEMP%"))
                {
                    theValue = theValue.Replace("%TEMP%",
                        Environment.GetEnvironmentVariable("TEMP"));
                }
            }
            catch(Exception e)
            {
                Exception ase = 
                    new Exception("[ERROR] Retreiving appsettings key '"+
                    appSettingsKey+"' - '"+e.Message+"'");

                throw e;
            }

            if (theValue == null)
            {
                throw new Exception("'"+appSettingsKey+"' not found in app.config");
            }

            return theValue;
        }
    }
}
