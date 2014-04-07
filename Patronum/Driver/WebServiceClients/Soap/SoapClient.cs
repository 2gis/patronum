
namespace Patronum.Driver.WebServiceClients.Soap
{
    using System.Xml.Linq;

    using HttpRequest;

    public abstract class SoapClient : WebServiceClient
    {
        protected SoapClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public HttpResponse Request(string action, XDocument soapEnvelop)
        {
            var request = new SoapRequest(ServiceBaseUri, action);

            request.ApplyCredential(UserNetworkCredentials);

            return request.Request(soapEnvelop);
        }
    }
}
