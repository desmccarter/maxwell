﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.maxwell.pageengine.factories;
using uk.org.maxwell.pageengine.services;
using uk.org.maxwell.pageengine.xml;
using uk.org.maxwell.shareddomainobjects;

namespace actionsamples.google.simplesearch.tests
{
	[TestFixture]
	public class TestGoogleSearch : ITestObject
	{
		[Test]
		public void TestGoogleSearchOnce()
		{
            // *** get an instance of google.com, search
            // *** for James Clerk Maxwell then finally dbl click
            // *** his wiki link ...

            using (Page page = OpenPage("GoogleSearchPage"))
            {
                page.SetText("SearchBox", "James Clerk Maxwell");
                page.Click("SearchButton");
                page.DoubleClick("JamesClerkMaxwellWikiLink");
            }
		}

		[Test]
		public void TestGoogleSearchTwice()
		{
            // *** get an instance of google.com, search
            // *** for Barack Obama then finally dbl click
            // *** his wiki link ...

            using (Page page = OpenPage("GoogleSearchPage"))
            {
                page.SetText("SearchBox", "Barack Obama");
                page.Click("SearchButton");
                page.DoubleClick("BarackObamaWikiLink");
            }

			// *** get an instance of google.com, search
			// *** for Barack Obama then finally dbl click
			// *** his wiki link ...

			using (Page page = OpenPage("GoogleSearchPage") )
			{
				page.SetText("SearchBox", "James Clerk Maxwell");
				page.Click("SearchButton");
				page.DoubleClick("JamesClerkMaxwellWikiLink");				
			}
		}
	}
}
