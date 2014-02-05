using System;
using System.IO;
using System.Net;

namespace Patronum.WebService.Driver.HttpRequest.Request
{
    public class RestRequest : HttpRequest
    {
        public RestRequest(Uri uri) : base(uri)
        {
        }

        public HttpResponse PostRequest(string data, string contentType)
        {
            _request.Method = "POST";

            _request.ContentType = contentType;
            using (var dataStream = new StreamWriter(_request.GetRequestStream()))
            {
                dataStream.Write(data);
                dataStream.Flush();
            }

            return base.Request();
        }

        public HttpResponse GetRequest(string data)
        {
            if (data.Length > 0)
            {
                var request = Create(new Uri(_request.RequestUri + "?" + data));
                
                var cookies = ((HttpWebRequest)_request).CookieContainer.GetCookies(_request.RequestUri);

                var credentials = _request.Credentials.GetCredential(_request.RequestUri, "Negotiate");
                
                _request = request;

                ApplyCookies(cookies);

                ApplyCredential(credentials);
            }

            _request.Method = "GET";

            return base.Request();
        }
    }
}
