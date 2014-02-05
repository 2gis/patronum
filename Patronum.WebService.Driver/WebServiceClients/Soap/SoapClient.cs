using System.Xml.Linq;
using Patronum.WebService.Driver.HttpRequest;
using Patronum.WebService.Driver.HttpRequest.Request;

namespace Patronum.WebService.Driver.WebServiceClients.Soap
{
    public abstract class SoapClient : WebServiceClient
    {
        protected SoapClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public HttpResponse Request(string action, XDocument soapEnvelop)
        {
            var request = new SoapRequest(ServiceBaseUri, action);

            request.ApplyCredential(_userCredentials);

            return request.Request(soapEnvelop);
        }
    }
}
