﻿using logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using uk.org.hs2.genericutils;
using uk.org.hs2.pageengine.services;
using uk.org.hs2.pageengine.xml;

namespace uk.org.hs2.pageengine.factories
{
    public class PageFactory
    {
        public static Page GetOpenedPage(string name, string pageFactoryLocation)
        {
            Page page = GetPage(name, pageFactoryLocation);

            page.Open();

            return page;
        }

        public static Page GetPage(string name, string pageFactoryLocation)
        {
            if ((pageFactoryLocation == null) || (pageFactoryLocation.Length == 0))
			{
				throw new Exception("[ERR] pageFactory location cannot be null or zero length");
			}

			Page page = null;

			// *** go through all the methods that have called this method
			// *** and get the resource (using the methods assembly) location ...

			Stream[] s =
				new StackTrace().GetFrames().Where(
					frame => frame.GetMethod().DeclaringType.Assembly.
					GetManifestResourceStream(pageFactoryLocation) != null).
					Select(fr => fr.GetMethod().DeclaringType.Assembly.
					GetManifestResourceStream(pageFactoryLocation)).ToArray();

			if (s.Length == 0)
			{
				Log.ErrAndFail("[ERR] Cannot find page factory resource " + pageFactoryLocation);
			}

			using (s[0])
			{
				using (StreamReader r = new StreamReader(s[0]))
				{
					Pages pages = new XmlSerializer(typeof(Pages)).Deserialize(r) as Pages;

					Page[] parr =
						(pages as Pages)
						.Page.Where(
							item =>
								item.Name.Equals(name)
									).ToArray() as Page[];

					page = parr.Length > 0 ? parr[0] : null;
				}
			}

            if (page == null)
            {
                throw new Exception("Page '" + name + "' does not exist in " + pageFactoryLocation);
            }

            // *** look for pages that we need to also
            // *** take elements from for "this" page ...

            if ( page.InheritsPages != null )
			{
				foreach (string parentPageName in
					page.InheritsPages.Page.Select(item => item.Name))
				{
					Page parentPage = GetOpenedPage(parentPageName, pageFactoryLocation);

					if (parentPage == null)
					{
						throw new Exception("[ERR] Parent page does not exist");
					}

					page.Element =
						page.Element.ToList().Concat(parentPage.Element.ToList())
						.ToArray();
				}
			}

			return page;
		}
    }
}
