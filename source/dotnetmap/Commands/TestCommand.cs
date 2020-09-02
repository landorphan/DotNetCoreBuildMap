namespace dotnetmap.Commands
{
    public class TestCommand : BuildBase
    {
        public TestCommand() : base(
            "test",
            "Walks the project map and executes tests for each test project in order. " +
            "Test projects are identified as projects containing a known test library or project properties that mark " +
            "the project as a test project.")
        {
        }
    }
}
