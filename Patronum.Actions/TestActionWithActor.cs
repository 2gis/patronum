
using Patronum.Actions.Interfaces;

namespace Patronum.Actions
{
    public abstract class TestAction<T> : TestAction where T : ITestActor, new()
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
