using System;
using System.Net;

namespace Patronum.WebService.Testing.WebServiceClients
{
    public abstract class WebServiceClient
    {
        protected NetworkCredential UserCredentials;

        protected WebServiceClient(string serviceBaseUrl)
        {
            ServiceBaseUri = new Uri(serviceBaseUrl);
        }

        protected WebServiceUnderTest WebService { get; set; }

        protected Uri ServiceBaseUri { get; set; }

        public void SetUserCredentials(NetworkCredential credentials)
        {
            UserCredentials = credentials;
        }

    }
}
