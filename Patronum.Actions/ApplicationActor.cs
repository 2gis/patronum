using System.Net;
using Patronum.WebService.Testing;

namespace Patronum.Actions
{
    public abstract class ApplicationActor
    {
        protected NetworkCredential NetworkCredential { get; set; }

        protected IApplicationUnderTest ApplicationUnderTest { get; set; }

        protected ApplicationActor(IApplicationUnderTest app)
        {
            ApplicationUnderTest = app;
        }


        public abstract bool SignIn();

        public abstract bool SignOut();
    }
}
