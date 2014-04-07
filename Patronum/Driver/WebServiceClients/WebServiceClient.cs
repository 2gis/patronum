
namespace Patronum.Driver.WebServiceClients
{
    using System;
    using System.Net;

    public abstract class WebServiceClient
    {
        protected NetworkCredential UserNetworkCredentials;

        protected Uri ServiceUriBasePart;

        protected WebServiceClient(string serviceBaseUrl)
        {
            ServiceUriBasePart = new Uri(serviceBaseUrl);
        }

        public Uri ServiceBaseUri
        {
            get
            {
                return ServiceUriBasePart;
            }
        }

        public NetworkCredential UserCredentials
        {
            get
            {
                return UserNetworkCredentials;
            }
        }

        public void SetUserCredentials(NetworkCredential credentials)
        {
            UserNetworkCredentials = credentials;
        }
    }
}
