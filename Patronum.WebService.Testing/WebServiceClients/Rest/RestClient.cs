using System;
using System.Collections.Generic;
using Patronum.WebService.Testing.HttpRequest;
using Patronum.WebService.Testing.HttpRequest.Request;

namespace Patronum.WebService.Testing.WebServiceClients.Rest
{
    public abstract class RestClient : WebServiceClient
    {
        protected RestClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public HttpResponse Request(string url, Dictionary<string, string> data, RequestMethod method = RequestMethod.Get)
        {
            var request = new RestRequest(new Uri(ServiceBaseUri + url), UserCredentials);
            return request.Request(data, method);
        }
    }
}
