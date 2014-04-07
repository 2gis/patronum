
namespace Patronum.Driver.HttpRequest
{
    using System;
    using System.IO;
    using System.Net;

    public class RestRequest : HttpRequest
    {
        public RestRequest(Uri uri)
            : base(uri)
        {
        }

        public HttpResponse PostRequest(string data, string contentType)
        {
            WebRequestSource.Method = "POST";

            WebRequestSource.ContentType = contentType;
            using (var dataStream = new StreamWriter(WebRequestSource.GetRequestStream()))
            {
                dataStream.Write(data);
                dataStream.Flush();
            }

            return Request();
        }

        public HttpResponse GetRequest(string data)
        {
            if (data.Length > 0)
            {
                var request = Create(new Uri(WebRequestSource.RequestUri + "?" + data));

                var cookieContainer = ((HttpWebRequest)WebRequestSource).CookieContainer;
                
                var credentials = WebRequestSource.Credentials.GetCredential(WebRequestSource.RequestUri, "Negotiate");
                
                WebRequestSource = request;

                if (cookieContainer != null)
                {
                    ApplyCookies(cookieContainer.GetCookies(WebRequestSource.RequestUri));
                }

                ApplyCredential(credentials);
            }

            WebRequestSource.Method = "GET";

            return Request();
        }
    }
}
