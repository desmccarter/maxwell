using actionengine.actions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.maxwell.shareddomainobjects;
using uk.org.maxwell.pageengine.factories;
using uk.org.maxwell.pageengine.services;
using uk.org.maxwell.pageengine.xml;

namespace actionsamples.expedia.flights.tests
{
	[TestFixture]
	public class TestExpediaFlights : ITestObject
	{
		[Test]
		public void TestExpediaFlightsLink()
		{
            // *** the following 'using' clause is to call all
            // *** disposables after test is complete ...

            using (this)
            {
                ActionFactory.ExecuteActionUsingMatch("Submit Flight Details");
            }
		}
	}
}
