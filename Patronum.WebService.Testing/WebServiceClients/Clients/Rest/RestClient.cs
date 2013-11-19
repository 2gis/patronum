using System;
using System.Collections.Generic;
using Patronum.WebService.Testing.WebServiceClients.Request;

namespace Patronum.WebService.Testing.WebServiceClients.Clients.Rest
{
    public abstract class RestClient : WebServiceClient
    {
        protected RestClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public HttpResponse Request(string url, Dictionary<string, string> data, string method = "GET")
        {
            var request = new HttpRequest(new Uri(ServiceBaseUri + url), UserCredentials);
            return request.Request(data, method);
        }
    }
}
