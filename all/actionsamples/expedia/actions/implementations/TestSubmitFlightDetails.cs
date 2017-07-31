using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.hs2.shareddomainobjects;
using uk.org.hs2.pageengine.factories;
using uk.org.hs2.pageengine.services;
using uk.org.hs2.pageengine.xml;
using actionengine.actions;
using uk.org.hs2.shareddomainobjects.simplebddobjects;

namespace actionsamples.expedia.actions.implementations
{
	public class TestSubmitFlightDetails : TestActionStep
	{
        public void RunTest()
        {
            Page page = OpenPage("ExpediaPage");

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

            Page departurePage = GetPage("ExpediaDeparturePage");

            string title = "Select your departure to Melbourne";

            departurePage.AssertElementAndTextAreEqual("PageTitle", title);

            CacheObject("ExpediaDeparturePage", 0, departurePage);
            CacheObject("ExpediaDeparturePageTitle", 0, new StringDomainObject(title) );
        }
	}
}
