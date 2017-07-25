Description: 	Maxwell Project
Author:		Des McCarter

Selenium based page factory testOB framework.


Sample pagei setup : (//github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/pages/pages.xml)

<?xml version="1.0" encoding="utf-8" ?>
<Pages>

  <Page Name="ExpediaPage" Url="http://www.expedia.co.uk">
    <Element Name="FlightsLink" XPath="//button[@data-lob='flight']"/>
    <Element Name="FlightsFromBox" XPath="//input[@id='flight-origin-hp-flight']"/>
    <Element Name="FlightsToBox" XPath="//input[@id='flight-destination-hp-flight']"/>
    
    <Element Name="AdultsDropdown" XPath="//select[@id='flight-adults-hp-flight']"/>
    <Element Name="ChildrenDropdown" XPath="//select[@id='flight-children-hp-flight']"/>
    <Element Name="SearchButton" XPath="//*[@id='gcw-flights-form-hp-flight']/div[9]/label/button"/>
    <Element Name="ChildAge1Dropdown" XPath="//select[@id='flight-age-select-1-hp-flight']"/>
    <Element Name="ChildAge2Dropdown" XPath="//select[@id='flight-age-select-2-hp-flight']"/>
    <Element Name="ChildAge3Dropdown" XPath="//select[@id='flight-age-select-3-hp-flight']"/>
    <Element Name="ChildAge4Dropdown" XPath="//select[@id='flight-age-select-4-hp-flight']"/>
    <Element Name="ReturningBox" XPath="//input[@id='flight-returning-hp-flight']"/>
    <Element Name="DepartingBox" XPath="//input[@id='flight-departing-hp-flight']"/>
  </Page>
  
</Pages>

Sample code usage (of page):

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
                page.Click("FlightsLink");
                page.SetText("FlightsFromBox", "London, England, UK (LHR-Heathrow)");
                page.SetText("FlightsToBox", "Melbourne, VIC, Australia(MEL - All Airports)");
                page.SetDropdown("AdultsDropdown", "3");
                page.SetText("DepartingBox", "21/10/2017");
                page.SetText("ReturningBox", "29/11/2017");
                page.SetDropdown("ChildrenDropdown", "3");
                page.SetDropdown("ChildAge1Dropdown", "10");
                page.SetDropdown("ChildAge2Dropdown", "12");
                page.SetDropdown("ChildAge3Dropdown", "14");


                page.DoubleClick("SearchButton");
                int x = 0;
            }
		}
	}
}





