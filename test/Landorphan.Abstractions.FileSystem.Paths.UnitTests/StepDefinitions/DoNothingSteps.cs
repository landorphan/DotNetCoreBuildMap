namespace Landorphan.Abstractions.Tests.StepDefinitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class DoNothingSteps
    {
        [Given(@"I have done nothing")]
        [When(@"I do nothing")]
        [Then(@"nothing should occur")]
        public void GivenIHaveDoneNothing()
        {
            // Used to "test the test system" 
            // and for places where the Gherkin needs 
            // to be complete but no action is necessary.
        }
    }
}
