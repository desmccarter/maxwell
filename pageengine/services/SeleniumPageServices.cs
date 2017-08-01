using logging;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.maxwell.pageengine.browserdrivers;
using uk.org.maxwell.pageengine.browserdrivers.handlers;
using uk.org.maxwell.pageengine.browserdrivers.interfaces;
using uk.org.maxwell.pageengine.enums;
using uk.org.maxwell.pageengine.pageinterface;
using uk.org.maxwell.pageengine.xml;
using uk.org.maxwell.shareddomainobjects;

namespace uk.org.maxwell.pageengine.services
{
    public abstract class SeleniumPageServices : IPageServices, IDomainObject, IDisposable
    {
        // *** ... always initially has to be 1
        protected static int windowCount = 0;

        protected string windowsHandle = null;

        protected static string currentWindowsHandle = null;

        protected static DriverInfo driverInfo = null;

        protected Type driverWrapperType = null;

        protected bool openedPage = false;

        protected PageTypeEnum pageType = PageTypeEnum.MAIN;

        protected string url = null;

        public static IDriverHandler DriverWrapper
        {
            get
            {
                return driverInfo.driverWrapper;
            }
        }

        protected void InitDriver()
        {
            if (driverInfo == null)
            {
                driverInfo = new DriverInfo();
            }
        }

        protected void SetWindowsHandle()
        {
            // *** wait for any pop-ups ...
            int tries = 0;

            while(
                (driverInfo.driverWrapper.WebDriver.WindowHandles.Count<=windowCount) &&
                tries<10)
            {
                Thread.Sleep(500);
            }

            if(driverInfo.driverWrapper.WebDriver.WindowHandles.Count==windowCount)
            {
                throw new Exception("[ERR] No new window popup found");
            }

            windowCount = driverInfo.driverWrapper.WebDriver.WindowHandles.Count;

            windowsHandle =
                driverInfo.driverWrapper.WebDriver.WindowHandles[
                driverInfo.driverWrapper.WebDriver.WindowHandles.Count - 1];

            if (currentWindowsHandle == null)
            {
                currentWindowsHandle = windowsHandle;
            }
        }

        protected void SetWindowsHandleAsPrimary()
        {
            windowsHandle =
                driverInfo.driverWrapper.WebDriver.WindowHandles[0];

            if (currentWindowsHandle == null)
            {
                currentWindowsHandle = windowsHandle;
            }
        }

        public void SwitchToThisWindow()
        {
            if(currentWindowsHandle==null)
            {
                currentWindowsHandle=
                    driverInfo.driverWrapper.WebDriver.WindowHandles[0];
            }

            if ( windowsHandle != null )
            {
                if (currentWindowsHandle != windowsHandle)
                {
                    currentWindowsHandle = windowsHandle;

                    driverInfo.driverWrapper.WebDriver.SwitchTo().Window(windowsHandle);
                }
            }
            else
            {
                throw new Exception("[ERR] windowsHandle cannot be null");
            }
        }

        public void OpenAsPopup()
        {
            SetWindowsHandle();

            openedPage = true;

            SwitchToThisWindow();

            driverInfo.driverWrapper.WebDriver.Manage().Window.Maximize();

            pageType = PageTypeEnum.POPUP;
        }

        public void OpenAsPrimary()
        {
            SetWindowsHandleAsPrimary();

            openedPage = true;

            SwitchToThisWindow();
        }

        public void Open(string url)
		{
            this.url = url;

            InitDriver();

			DriverWrapper.WebDriver.Navigate().GoToUrl(url);

            SetWindowsHandle();

            openedPage = true;

            SwitchToThisWindow();
		}

        protected void ClearElementText(IWebElement webElement)
        {
            SwitchToThisWindow();

            new Actions(DriverWrapper.WebDriver)
                .MoveToElement(webElement).Click();

            webElement.Clear();
        }

        public void SetElementTextUsingWebElement(IWebElement webElement, string value)
		{
            SwitchToThisWindow();

            ClearElementText(webElement);

            new Actions(DriverWrapper.WebDriver)
            	.MoveToElement(webElement).SendKeys(value).Build().Perform();
        }

        protected void SetElementValueUsingJavaScript(string xpath, string value)
        {
            ((IJavaScriptExecutor)DriverWrapper.WebDriver).
                ExecuteScript(
                "var element = document.evaluate(\"" + xpath + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; element.setAttribute(\"value\",\"" + value + "\");");
        }

        protected void ClickElementUsingJavaScript(string xpath)
        {
            SwitchToThisWindow();

            // string script=
            //     "var element = document.evaluate(\"" + xpath + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; element.click();";

            string script =
                "document.evaluate( '//body', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null );";

            IJavaScriptExecutor jse = (IJavaScriptExecutor)DriverWrapper.WebDriver;

            jse.ExecuteScript(script);

            ((IJavaScriptExecutor)DriverWrapper.WebDriver).
                ExecuteScript(
                script);
        }

        protected void DoubleClickElementUsingJavaScript(string xpath)
        {
            string javascript =
                "var element = document.evaluate(\"" + xpath + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "var clickEvent = document.createEvent(\"MouseEvents\");" +
                "clickEvent.initEvent(\"dblclick\", true, true);" +
                "element.dispatchEvent(clickEvent);";

            ((IJavaScriptExecutor)DriverWrapper.WebDriver).
                ExecuteScript(javascript);
        }

        public void SetElementTextUsingXPath(string xpath, string value)
        {
            SwitchToThisWindow();

            WaitForElement(xpath);

            SetElementValueUsingJavaScript(xpath, value);
        }

        protected IWebElement FindElement(string xpath)
		{
            SwitchToThisWindow();

            return DriverWrapper.WebDriver.FindElement(By.XPath(xpath));
		}

		protected void WaitForElement(string xpath)
		{
            SwitchToThisWindow();

            int waitTimeInSeconds = 5;

			var wait =
				new WebDriverWait(DriverWrapper.WebDriver,
				TimeSpan.FromSeconds(waitTimeInSeconds));

			Log.Debug("[INFO] Waiting for element XPath " + xpath + " for " +
				waitTimeInSeconds + " seconds ...");

			wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));

			Log.Debug("[INFO] ELEMENT FOUND for XPath " + xpath);
		}

		public string GetElementAttributeUsingXPath(string xpath, string attrName)
		{
            SwitchToThisWindow();

            IWebElement webElement = FindElement(xpath);
			new Actions(DriverWrapper.WebDriver).MoveToElement(webElement).DoubleClick().Perform();
			return webElement.GetAttribute(attrName);
		}

		public string GetElementValueUsingXPath(string xpath)
		{
            SwitchToThisWindow();

            // *** we need to check if we're getting an attribute.
            // *** if so then we want to call the getelementattribute
            // *** method, else getelementtext ...

            string value = null;


			// *** if we are loading an attribute ...
			Match m = null;

			if ((m = new Regex("^(.*)/@?([a-z|A-Z| ]+)$",
				RegexOptions.RightToLeft).Match(xpath)).Success)
			{
				// *** extract thew new XPath ...
				string newxpath = m.Groups[1].Value;

				// *** extract the name of the attribute we need
				// *** to get data from ...

				string attributeName = m.Groups[2].Value;


				WaitForElement(newxpath);

				// *** get the elements attribute value ...
				value =
				GetElementAttributeUsingXPath(
					newxpath,
					attributeName);
			}
			else
			{
				// *** just extract the text
				// *** from the element ...

				WaitForElement(xpath);

				value = FindElement(xpath).Text;
			}

			return value;
		}

        public List<string> GetElementTextListUsingXPath(string xpath)
        {
            SwitchToThisWindow();

            WaitForElement(xpath);

            ReadOnlyCollection<IWebElement> webElements =
                DriverWrapper.WebDriver.FindElements(By.XPath(xpath));

            return webElements.Select(item => item.Text).ToList();
        }
		
		public void ClickElementUsingXPath(string xpath)
		{
            SwitchToThisWindow();

            WaitForElement(xpath);

            new Actions(DriverWrapper.WebDriver)
				.MoveToElement(FindElement(xpath)).Click().Build().Perform();
		}

		public void DoubleClickElementUsingXPath(string xpath)
		{
            SwitchToThisWindow();

            WaitForElement(xpath);

            // new Actions(DriverWrapper.WebDriver)
            //	.MoveToElement(FindElement(xpath)).DoubleClick().Build().Perform();
            //new Actions(DriverWrapper.WebDriver)
            //   .MoveToElement(FindElement(xpath)).DoubleClick().Build().Perform();

            DoubleClickElementUsingJavaScript(xpath);
        }


        public void SetDropdownUsingXPath(string xpath, string value)
        {
            SwitchToThisWindow();

            WaitForElement(xpath);

            IWebElement webElement = FindElement(xpath);

            webElement.FindElement(
                By.CssSelector("option[value='"+value+"']")).Click();
        }

        public void Dispose()
        {
            /// disposed only if main window ...
            if ( pageType.Equals(PageTypeEnum.MAIN) &&
                    openedPage && (DriverWrapper != null) &&
                (DriverWrapper.WebDriver != null))
            {
                DriverWrapper.WebDriver.Quit();
                DriverWrapper.WebDriver = null;
                openedPage = false;
            }
        }
    }
}
