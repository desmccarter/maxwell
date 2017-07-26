using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.hs2.pageengine.factories;
using uk.org.hs2.pageengine.services;
using uk.org.hs2.pageengine.xml;
using uk.org.hs2.shareddomainobjects;

namespace actionsamples.betfair.login.tests
{
	[TestFixture]
	public class TestBetfairLogin : ITestObject
	{
		[Test]
		public void TestBetfairLoginPage()
		{
            // *** get an instance of google.com, search
            // *** for James Clerk Maxwell then finally dbl click
            // *** his wiki link ...

            using (Page page = OpenPage("BetfairPage"))
            {
                page.SetText("Username", "monkeybone1979");
                page.SetText("Password", "password01");
                page.DoubleClick("Login");
            }
		}
	}
}
