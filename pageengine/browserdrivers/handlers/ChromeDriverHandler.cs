using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using uk.org.hs2.pageengine.browserdrivers.interfaces;

namespace uk.org.hs2.pageengine.browserdrivers.handlers
{
	public class ChromeDriverHandler : IDriverHandler
	{
		public ChromeDriverHandler(string driverPath) : base(driverPath)
		{}

		protected override void InitialiseDriver()
		{
			if (driverPath == null)
			{
				throw new Exception("[ERR] DriverPath not set");
			}

			if (WebDriver == null)
			{
				WebDriver = new ChromeDriver(driverPath);
			}
		}
	}
}
