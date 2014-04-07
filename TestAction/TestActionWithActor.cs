
namespace TestActions
{
    using Interfaces;

    public abstract class TestAction<T> : TestActions.TestAction where T : ITestActor, new()
    {
        protected ITestActor Actor { get; set; }

        protected TestAction(IApplicationUnderTest application)
            : base(application)
        {
            Actor = new T();
            Actor.SignIn();
        }
    }
}
