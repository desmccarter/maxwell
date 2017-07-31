Description: 	Maxwell Project (Named after James Clerk Maxwell - https://en.wikipedia.org/wiki/James_Clerk_Maxwell)

Author:		Des McCarter

XML based Selenium page factory and test flow automation framework.

Full documentation can be found in this repository at https://github.com/desmccarter/maxwell/blob/master/README.docx.


Framework download and set-up:

a. GIT clone https://github.com/desmccarter/maxwell

b. Open solution maxwell/all/all.sln

c. Build solution using Visual Studio 2015+ (MSBuild)

d. Sample tests are currently based on raw NUNIT: main project = actionsamples


Note: Sample tests will be listed as either NUNIT or Specflow based within Test Explorer in Visual Studio

Dependencies (automatically resolved on installation):

Software				Version			Installation/location
Visual Studio 2015 Community Edition	14.0.25431.01 Update 3	https://www.visualstudio.com/downloads/
Specflow				2.2.0			NUGET
Specflow.Runner				1.6.0			NUGET
SpecRun.Specflow			1.6.0			NUGET
Selenium.WebDriver			3.4.0			NUGET
WebDriver.ChromeDriver.win32		2.30.0			NUGET
Selenium.Support			3.4.0			NUGET
NUNIT					3.7.1			NUGET


Sample XML based page setup : 

//github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/pages/pages.xml)


Sample usage of page (Expedia Flights) ...

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

Sample code usage of (above) page/XML: 

https://github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/tests/TestExpediaFlights.cs)
