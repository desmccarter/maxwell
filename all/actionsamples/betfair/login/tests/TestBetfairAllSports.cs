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

namespace actionsamples.betfair.login.tests
{
	[TestFixture]
	public class TestBetfairAllSports : ITestObject
	{
		[Test]
		public void TestBetfairAllSportsLink()
		{
            // *** get an instance of google.com, search
            // *** for James Clerk Maxwell then finally dbl click
            // *** his wiki link ...

            using (Page page = OpenPage("BetfairPage"))
            {
                page.DoubleClick("AllSportsLink");

                using (Page allSportsPage = GetNewPopupPage("BetfairAllSportsPage") as Page)
                {
                    allSportsPage.Click("HorseRacingLink");
                }
            }
		}
	}
}
