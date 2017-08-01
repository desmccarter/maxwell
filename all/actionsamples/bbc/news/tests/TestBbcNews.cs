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

namespace actionsamples.bbc.news.tests
{
	[TestFixture]
	public class TestBbcNews : ITestObject
	{
		[Test]
		public void TestBbcNewsSportPage()
		{
            // *** get an instance of bbc news and navigate to sport ...

            using (Page page = OpenPage("BbcNewsPage"))
            {
                page.DoubleClick("Sport");
            }
		}
	}
}
