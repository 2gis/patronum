
using Patronum.WebService.Driver.WebServiceClients.Rest;

namespace Patronum.WebService.Driver.WebServiceClients.Controller
{
    public abstract class ControllerClient : RestClient
    {
        protected ControllerClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }
    }
}
