
namespace Patronum.WebService.Testing.WebServiceClients
{
    public abstract class ClientCreator
    {
        protected readonly WebServiceUnderTest WebService;

        protected ClientCreator(WebServiceUnderTest webService)
        {
            WebService = webService;
        }

        public abstract WebServiceClient CreateClient();
    }
}
