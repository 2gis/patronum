using System;
using System.IO;
using System.Net;

namespace Patronum.WebService.Driver.HttpRequest
{
    public enum RequestMethod
    {
        Post,
        Get
    }

    public class HttpRequest : WebRequest
    {
        protected WebRequest _request;

        public HttpRequest(Uri uri)
        {
            _request = Create(uri);
        }

        public HttpResponse Request()
        {
            var response = new HttpResponse { Uri = _request.RequestUri };

            try
            {
                var result = (HttpWebResponse)_request.GetResponse();
                response.Code = (int)result.StatusCode;

                var responseStream = result.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        response.Text = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                try
                {
                    response.Code = (int)((HttpWebResponse)((WebException)e).Response).StatusCode;
                }
                catch (Exception)
                {
                    response.Code = 500;
                }
            }

            return response;
        }

        public void ApplyCredential(NetworkCredential credential)
        {
            if (credential != null)
            {
                _request.Credentials = new CredentialCache { { _request.RequestUri, "Negotiate", credential } };
            }
        }

        public void ApplyCookies(CookieCollection cookie)
        {
            if (cookie != null)
            {
                var httpRequest = (HttpWebRequest)_request;

                httpRequest.CookieContainer = new CookieContainer();
                httpRequest.CookieContainer.Add(_request.RequestUri, cookie);
            }
        }
    }
}
