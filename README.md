Description: 	Maxwell Project (Named after James Clerk Maxwell - https://en.wikipedia.org/wiki/James_Clerk_Maxwell)

Author:		Des McCarter

XML based test flow automation and Selenium page factory framework.

(Think of this framework as "married" with Selenium, as Angular JS is married to Javascript/JQuery)


Full documentation can be found in this repository at https://github.com/desmccarter/maxwell/blob/master/README.docx.

Framework download and set-up:

a. GIT clone https://github.com/desmccarter/maxwell

b. Open solution maxwell/all/all.sln

c. Build solution using Visual Studio 2015+ (MSBuild)

d. Sample tests are currently based on raw NUNIT: main project = actionsamples


Note: Sample tests will be listed as either NUNIT or Specflow based within Test Explorer in Visual Studio

Dependencies are listed in the comprehensive README doc (.docx)

Sample XML based page definitions retrived from PageFactory.GetPage(): 

//github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/pages/pages.xml)

Page definiteion snapshot (Expedia Flights) ...

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

Sample code usage of (above) page page instance (retrived by calling PageFactory.GetPage("ExpediaPage") behind the sceens: 

https://github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/tests/TestExpediaFlights.cs)
