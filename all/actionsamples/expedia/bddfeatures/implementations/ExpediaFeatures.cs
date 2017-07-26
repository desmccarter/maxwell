using actionengine.actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace actionsamples.expedia.bddfeatures.implementations
{
    [Binding]
    public class ExpediaFeatures
    {
        [Given("an[ ]+Expedia[ ]+customer[ ]+(.*)")]
        public void GivenCustomerExecutesAction(string actionMatch)
        {
            ActionFactory.ExecuteActionUsingMatch(actionMatch);
        }

        [Then("the[ ]+customer[ ]+should[ ]+successfuly[ ]+verify[ ]+that[ ]+(.*)")]
        public void ThenCustomerExecutesAction(string actionMatch)
        {
            ActionFactory.ExecuteActionUsingMatch(actionMatch);
        }
    }
}
