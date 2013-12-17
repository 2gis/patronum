using System;
using System.Net;

namespace Patronum.WebService.Driver.WebServiceClients
{
    public abstract class WebServiceClient
    {
        protected NetworkCredential _userCredentials;

        protected Uri _serviceBaseUri;

        protected WebServiceClient(string serviceBaseUrl)
        {
            _serviceBaseUri = new Uri(serviceBaseUrl);
        }

        public Uri ServiceBaseUri
        {
            get
            {
                return _serviceBaseUri;
            }
        }

        public NetworkCredential UserCredentials
        {
            get
            {
                return _userCredentials;
            }
        }

        public void SetUserCredentials(NetworkCredential credentials)
        {
            _userCredentials = credentials;
        }
    }
}
