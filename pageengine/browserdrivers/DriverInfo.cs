using logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using uk.org.hs2.genericutils;
using uk.org.hs2.pageengine.browserdrivers.interfaces;

namespace uk.org.hs2.pageengine.browserdrivers
{
    public class DriverInfo
    {
        public string Location { get; set; }
        public IDriverHandler driverWrapper { get; set; }

        public DriverInfo()
        {
            string location = AppSettings.Get("driver.location");

            string driverWrapperType = AppSettings.Get("driver.wrapper.class");

            Type classType = null;

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type assType in a.GetTypes())
                {
                    string assTypeStr = assType.ToString();

                    if( assTypeStr.EndsWith(driverWrapperType) )
                    {
                        classType = assType; break;
                    }
                }
            }

            if(classType==null)
            {
                Log.ErrAndFail("[ERR] Cannot find class " + driverWrapperType + " anywhere within app domain");
            }

            try
            {
                driverWrapper = Activator.CreateInstance(
                        classType,
                        new object[] { location }) as IDriverHandler;
            }
            catch(Exception e)
            {
                Log.ErrAndFail("[ERR] Failed to create instance of driver handler " + driverWrapperType + ". It either does not implement IDriverWrapper or the driver location does not exist. Exception was '"+e.Message+"'");
            }
        }
    }
}
