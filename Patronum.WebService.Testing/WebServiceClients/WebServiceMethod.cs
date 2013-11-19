namespace Patronum.WebService.Testing.WebServiceClients
{
    public abstract class WebServiceMethod
    {
        protected readonly WebServiceClient WebServiceClient;

        protected WebServiceMethod(WebServiceClient webServiceClient)
        {
            WebServiceClient = webServiceClient;
        }
    }
}

