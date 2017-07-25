using bddobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using uk.org.hs2.pageengine.factories;
using uk.org.hs2.pageengine.services;
using uk.org.hs2.pageengine.xml;

namespace uk.org.hs2.npsdomainobjects
{
	public abstract class ITestObject : IDomainObject
	{
        protected Page GetNewPopupPage(String pageName)
        {
            string pageLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            pageLocation = Regex.Match(pageLocation,
                @"^(.*)\.[^\.]*$").Groups[1].Value + @".pages.pages.xml";

            Page page = PageFactory.GetPage(pageName, pageLocation) as Page;

            page.OpenAsPopup();
            
            return page;
        }

        protected Page GetOpenedPage(String pageName)
        {
            string pageLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            pageLocation = Regex.Match(pageLocation,
                @"^(.*)\.[^\.]*$").Groups[1].Value+@".pages.pages.xml";

            return GetOpenedPage(pageName, pageLocation);
        }

		protected Page GetOpenedPage(string pageName,
			string pageFactoryLocation)
		{
			return
				PageFactory.GetOpenedPage(pageName,
					pageFactoryLocation);
		}
	}
}
