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

    public class HttpRequest
    {
        protected readonly NetworkCredential Credential;

        protected readonly Uri ServiceUri;

        public HttpRequest(Uri uri)
        {
            ServiceUri = uri;
        }

        public HttpRequest(Uri uri, NetworkCredential credential)
        {
            ServiceUri = uri;
            Credential = credential;
        }

        protected HttpResponse Request(HttpWebRequest request)
        {
            var response = new HttpResponse();
            response.Uri = request.RequestUri;

            try
            {
                var result = (HttpWebResponse)(request.GetResponse());
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
    }
}
