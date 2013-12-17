
namespace Patronum.Actions
{
    using Patronum.Actions.Intarfaces;

    public abstract class Action<T> : Action where T : IActor, new()
    {
        protected IActor Actor { get; set; }

        protected Action(IApplicationUnderTest application)
            : base(application)
        {
            Actor = new T();
            Actor.SignIn();
        }
    }
}
