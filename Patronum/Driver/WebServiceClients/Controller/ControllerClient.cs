
namespace Patronum.Driver.WebServiceClients.Controller
{
    using Rest;

    public abstract class ControllerClient : RestClient
    {
        protected ControllerClient(string serviceBaseUrl) : base(serviceBaseUrl, ContentType.FormData)
        {
        }
    }
}
