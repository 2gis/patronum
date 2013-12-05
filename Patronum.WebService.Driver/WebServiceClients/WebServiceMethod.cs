namespace Patronum.WebService.Driver.WebServiceClients
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

