using System.Xml.Linq;
using Patronum.WebService.Testing.WebServiceClients.Request;

namespace Patronum.WebService.Testing.WebServiceClients.Clients.Soap
{
    public abstract class SoapClient : WebServiceClient
    {
        protected SoapClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public string Request(string action, XDocument soapEnvelop)
        {
            var request = new SoapRequest(ServiceBaseUri.ToString(), action, UserCredentials);
            return request.Request(soapEnvelop);
        }
    }
}
