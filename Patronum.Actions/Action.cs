using Patronum.WebService.Test;

namespace Patronum.Actions
{
    public abstract class Action<T> : IAction where T : IActor, new()
    {
        protected IApplicationUnderTest ApplicationUnderTest { get; set; }

        protected IActor Actor { get; set; }

        protected Action(IApplicationUnderTest application)
        {
            ApplicationUnderTest = application;

            Actor = new T();
            Actor.SignIn();
        }

        public abstract object Execute(params object[] list);
    }
}