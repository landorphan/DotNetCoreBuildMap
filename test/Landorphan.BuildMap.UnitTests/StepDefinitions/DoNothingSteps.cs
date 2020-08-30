using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.UnitTests.StepDefinitions
{
    using TechTalk.SpecFlow;

    [Binding]
    class DoNothingSteps
    {
        [Given(@"I have done nothing")]
        [When(@"I do nothing")]
        [Then(@"nothing should occur")]
        public void NothingMethod()
        {
        }

    }
}
