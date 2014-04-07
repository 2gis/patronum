
namespace Patronum.Driver.HttpRequest
{
    using System;
    using System.IO;
    using System.Net;

    public enum RequestMethod
    {
        Post,
        Get
    }

    public class HttpRequest : WebRequest
    {
        protected WebRequest WebRequestSource;

        public HttpRequest(Uri uri)
        {
            WebRequestSource = Create(uri);
        }

        public HttpResponse Request()
        {
            var response = new HttpResponse { Uri = WebRequestSource.RequestUri };

            try
            {
                var result = (HttpWebResponse)WebRequestSource.GetResponse();
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
                WebRequestSource.Credentials = new CredentialCache { { WebRequestSource.RequestUri, "Negotiate", credential } };
            }
        }

        public void ApplyCookies(CookieCollection cookie)
        {
            if (cookie != null)
            {
                var httpRequest = (HttpWebRequest)WebRequestSource;

                httpRequest.CookieContainer = new CookieContainer();
                httpRequest.CookieContainer.Add(WebRequestSource.RequestUri, cookie);
            }
        }
    }
}
