Feature: testdepartures
	In order to make a flight booking
	As a flights customer
	I want to specify a departure plan

Scenario: Expedia Customer successfully submits flight details and gets departures page
	Given an Expedia customer Submits Flight Details
	Then the customer should successfuly verify that The Departures Page is Shown
