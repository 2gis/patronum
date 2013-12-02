
using Patronum.WebService.Testing.WebServiceClients.Rest;

namespace Patronum.WebService.Testing.WebServiceClients.Controller
{
    public abstract class ControllerClient : RestClient
    {
        protected ControllerClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }
    }
}
