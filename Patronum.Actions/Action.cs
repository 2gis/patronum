
namespace Patronum.Actions
{
    using Intarfaces;

    public abstract class Action : IAction
    {
        protected IApplicationUnderTest ApplicationUnderTest { get; set; }

        protected Action(IApplicationUnderTest application)
        {
            ApplicationUnderTest = application;
        }

        public abstract object Execute(params object[] parameters);
    }
}
