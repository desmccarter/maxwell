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
using actionengine.actions;
using uk.org.maxwell.shareddomainobjects.simplebddobjects;

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
