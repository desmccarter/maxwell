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
using actionengine.actions;
using uk.org.hs2.shareddomainobjects.simplebddobjects;

namespace actionsamples.expedia.actions.implementations
{
	public class TestExpediaDepartures: TestActionStep
	{
        public void RunTest()
        {
            Page page = GetCachedObject("ExpediaDeparturePage", 0) as Page;

            StringDomainObject title = GetCachedObject("ExpediaDeparturePageTitle", 0) as StringDomainObject;

            page.AssertElementAndTextAreEqual("PageTitle", title.Value);
        }
    }
}
