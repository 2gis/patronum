
using Patronum.WebService.Testing.WebServiceClients.Clients.Rest;

namespace Patronum.WebService.Testing.WebServiceClients.Clients.Controller
{
    public abstract class ControllerClient : RestClient
    {
        protected ControllerClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }
    }
}
