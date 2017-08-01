using System;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using uk.org.maxwell.pageengine.browserdrivers.interfaces;

namespace uk.org.maxwell.pageengine.browserdrivers.handlers
{
	public class IEDriverHandler : IDriverHandler
	{
		public IEDriverHandler(string driverPath) : base(driverPath)
		{}

		protected override void InitialiseDriver()
		{
			if (driverPath == null)
			{
				throw new Exception("[ERR] DriverPath not set");
			}

			if (WebDriver == null)
			{
				InternetExplorerOptions ieOptions = new InternetExplorerOptions();
				ieOptions.IgnoreZoomLevel = true;
				ieOptions.EnableNativeEvents = true;
				ieOptions.EnsureCleanSession = true;
				ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
				WebDriver = new InternetExplorerDriver(driverPath, ieOptions);

                WebDriver.Manage().Window.Maximize();
			}
		}
	}
}
