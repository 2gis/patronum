using System;
using System.Net;

namespace Patronum.WebService.Driver.WebServiceClients
{
    public abstract class WebServiceClient
    {
        protected NetworkCredential UserCredentials;

        protected WebServiceClient(string serviceBaseUrl)
        {
            ServiceBaseUri = new Uri(serviceBaseUrl);
        }

        protected Uri ServiceBaseUri { get; set; }

        public void SetUserCredentials(NetworkCredential credentials)
        {
            UserCredentials = credentials;
        }

    }
}
