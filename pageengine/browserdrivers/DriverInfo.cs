using logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using uk.org.maxwell.genericutils;
using uk.org.maxwell.pageengine.browserdrivers.interfaces;

namespace uk.org.maxwell.pageengine.browserdrivers
{
    public class DriverInfo
    {
        public string Location { get; set; }
        public IDriverHandler driverWrapper { get; set; }

        public DriverInfo()
        {
            string location = AppSettings.Get("driver.location");

            string driverWrapperType = AppSettings.Get("driver.wrapper.class");

            foreach (Assembly a in GenericUtils.GetAllAssemblies())
            {
                try
                {
                    Type[] aType =
                        a.GetTypes() == null ? null :
                        a.GetTypes()
                        .Where(type => (type != null) && type.ToString().EndsWith(driverWrapperType))
                        .ToArray();

                    if ((aType != null) && (aType.Length > 0))
                    {
                        // *** found driver, so create new instance of it
                        // *** (i.e. start selenium) ...

                        driverWrapper = Activator.CreateInstance(
                                aType[0],
                                new object[] { location }) as IDriverHandler;
                        break;
                    }
                }
                catch (ReflectionTypeLoadException e)
                {
                    // *** ignore this error but log it ...
                    Log.Warn("[WARN] Unable to load types: " + e);
                }
                catch (Exception e)
                {
                    Log.ErrAndFail("[ERR] Failed to create instance of driver handler " + driverWrapperType + 
                        ". Looks like there was an issue starting selenium. Exception was '" + e.Message + "'");
                }
            }

            if(driverWrapper==null)
            {
                Log.ErrAndFail("[ERR] Failed to create instance of driver handler " + driverWrapperType +
                    ". It either does not implement IDriverWrapper or the driver location does not exist. Exception was '");
            }
        }
    }
}
