using System;
using System.IO;
using System.Net;

namespace Patronum.WebService.Driver.HttpRequest.Request
{
    public class RestRequest : HttpRequest
    {
        private readonly NetworkCredential _credential;

        public RestRequest(Uri uri, NetworkCredential credential)
            : base(uri)
        {
            _credential = credential;
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

            ApplyCredential(_credential);

            return base.Request();
        }

        public HttpResponse GetRequest(string data)
        {
            if (data.Length > 0)
            {
                var request = Create(new Uri(_request.RequestUri + "?" + data));
                _request = request;
            }

            _request.Method = "GET";

            ApplyCredential(_credential);

            return base.Request();
        }
    }
}
