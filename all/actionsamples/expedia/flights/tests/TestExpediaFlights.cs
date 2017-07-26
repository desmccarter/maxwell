using bddobjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.hs2.npsdomainobjects;
using uk.org.hs2.pageengine.factories;
using uk.org.hs2.pageengine.services;
using uk.org.hs2.pageengine.xml;

namespace actionsamples.expedia.flights.tests
{
	[TestFixture]
	public class TestExpediaFlights : ITestObject
	{
		[Test]
		public void TestExpediaFlightsLink()
		{
            using (Page page = GetOpenedPage("ExpediaPage"))
            {
                // *** go to flights link ...
                page.Click("FlightsLink");

                // *** fill in details ...
                page.SetText("FlightsFromBox", "London, England, UK (LHR-Heathrow)");
                page.SetText("FlightsToBox", "Melbourne, VIC, Australia(MEL - All Airports)");
                page.SetDropdown("AdultsDropdown", "2");
                page.SetText("DepartingBox", "21/10/2017");
                page.SetText("ReturningBox", "29/11/2017");
                page.SetDropdown("ChildrenDropdown", "4");
                page.SetDropdown("ChildAge1Dropdown", "10");
                page.SetDropdown("ChildAge2Dropdown", "12");
                page.SetDropdown("ChildAge3Dropdown", "14");
                page.SetDropdown("ChildAge4Dropdown", "17");

                // *** do a search ...
                page.DoubleClick("SearchButton");

                // *** assert that search comes up with page containing 
                // *** correct heading ...

                using (Page departurePage = GetPage("ExpediaDeparturePage"))
                {
                    departurePage.AssertElementAndTextAreEqual("PageTitle", "Select your departure to Melbourne");
                }
            }
		}
	}
}
