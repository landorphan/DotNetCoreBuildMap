namespace Landorphan.BuildMap.UnitTests.StepDefinitions
{
    using TechTalk.SpecFlow;

    [Binding]
    internal class DoNothingSteps
    {
        [Given(@"I have done nothing")]
        [When(@"I do nothing")]
        [Then(@"nothing should occur")]
        public void NothingMethod()
        {
            // no-op by design
        }
    }
}
