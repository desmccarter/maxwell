using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.maxwell.pageengine.browserdrivers.interfaces
{
	public abstract class IDriverHandler
	{
		protected string driverPath = null;

		public IWebDriver WebDriver
		{
			get; set;
		}

		public IDriverHandler(string driverPath)
		{
			string executionFolderRoot = 
				Path.GetDirectoryName(
				Assembly.GetAssembly(
				typeof(IDriverHandler)).CodeBase).
				Replace(@"file:\", "");

			this.driverPath = executionFolderRoot +@"\"+driverPath;

			InitialiseDriver();
		}

		protected abstract void InitialiseDriver();
	}
}
