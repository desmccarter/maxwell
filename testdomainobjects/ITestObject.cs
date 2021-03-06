﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using uk.org.maxwell.pageengine.factories;
using uk.org.maxwell.pageengine.services;
using uk.org.maxwell.pageengine.xml;

namespace uk.org.maxwell.shareddomainobjects
{
	public abstract class ITestObject : IDomainObject, IDisposable
	{
        protected static Hashtable pageTable = new Hashtable();
        protected static int pageCount = 0;

        protected string GetCompiledPageResourceLocation(string uncompiledLocation)
        {
            return Regex.Match(uncompiledLocation,
               @"^([^\.]*\.[^\.]*).*$").Groups[1].Value + @".pages.pages.xml";
        }

        protected Page GetNewPopupPage(String pageName)
        {
            string pageLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            pageLocation = GetCompiledPageResourceLocation(pageLocation);

            Page page = PageFactory.GetPage(pageName, pageLocation) as Page;

            page.OpenAsPopup();

            pageTable.Add(pageCount++, page);

            return page;
        }

        protected Page GetPage(String pageName)
        {
            string pageLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            pageLocation = GetCompiledPageResourceLocation(pageLocation);

            Page page = PageFactory.GetPage(pageName, pageLocation) as Page;

            page.OpenAsPrimary();

            pageTable.Add(pageCount++, page);

            return page;
        }

        protected Page OpenPage(String pageName)
        {
            string pageLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            pageLocation = GetCompiledPageResourceLocation(pageLocation);

            return GetOpenedPage(pageName, pageLocation);
        }

		protected Page GetOpenedPage(string pageName,
			string pageFactoryLocation)
		{
			Page p =
				PageFactory.GetOpenedPage(pageName,
					pageFactoryLocation);

            pageTable.Add(pageCount++, p);

            return p;
		}

        public void Dispose()
        {
            if ( pageTable.Count > 0)
            {
                for(int i=0; i<pageTable.Count; i++)
                {
                    Page p = pageTable[i] as Page;

                    if(p==null)
                    {
                        throw new Exception("[ERR] Null page object found whist trying to dispose of page");
                    }

                    p.Dispose();
                }
            }
        }
    }
}
