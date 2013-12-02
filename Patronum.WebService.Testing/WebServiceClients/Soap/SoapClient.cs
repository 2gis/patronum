using System.Xml.Linq;
using Patronum.WebService.Testing.HttpRequest;
using Patronum.WebService.Testing.HttpRequest.Request;

namespace Patronum.WebService.Testing.WebServiceClients.Soap
{
    public abstract class SoapClient : WebServiceClient
    {
        protected SoapClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public HttpResponse Request(string action, XDocument soapEnvelop)
        {
            var request = new SoapRequest(ServiceBaseUri, action, UserCredentials);
            return request.Request(soapEnvelop);
        }
    }
}
