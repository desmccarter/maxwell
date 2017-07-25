Description: 	Maxwell Project
Author:		Des McCarter

Selenium based page factory testOB framework.


Set-up:

a. GIT clone https://github.com/desmccarter/maxwell
b. Open solution maxwell/all/all.sln
c. Build solution
d. Sample tests are currently based on raw NUNIT: main project = actionsamples


Sample page setup : 

//github.com/desmccarter/maxwell/blob/master/all/actionsamples/expedia/flights/pages/pages.xml)


Snippet ...

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
