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

namespace actionsamples.bbc.news.tests
{
	[TestFixture]
	public class TestBbcNews : ITestObject
	{
		[Test]
		public void TestBbcNewsSportPage()
		{
            // *** get an instance of bbc news and navigate to sport ...

            using (Page page = GetOpenedPage("BbcNewsPage"))
            {
                page.DoubleClick("Sport");
            }
		}
	}
}
