using Patronum.WebService.Testing;

namespace Patronum.Actions
{
    public abstract class ApplicationActions
    {
        protected IApplicationUnderTest ApplicationUnderTest { get; set; }

        protected ApplicationActions(IApplicationUnderTest app)
        {
            ApplicationUnderTest = app;
        }
    }
}