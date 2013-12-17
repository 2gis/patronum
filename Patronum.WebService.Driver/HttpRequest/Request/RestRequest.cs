using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Patronum.WebService.Driver.HttpRequest.Request
{
    public class RestRequest : HttpRequest
    {
        public RestRequest(Uri uri) : base(uri)
        {
        }

        public RestRequest(Uri uri, NetworkCredential credential)
            : base(uri, credential)
        {
        }

        // todo refactor
        public static CookieCollection Cookies { get; set; }

        public HttpResponse PostRequest(string data, string contentType)
        {
            //string data = parameters.ToString(); //PrepareRequestData(parameters);

            var request = PreparePostRequest(data, contentType);
           
            if (Credential != null)
            {
                request.Credentials = new CredentialCache { { ServiceUri, "Negotiate", Credential } };
            }

            return base.Request(request);
        }

        public HttpResponse GetRequest(string data)
        {
            var request = PrepareGetRequest(data);

            if (Credential != null)
            {
                request.Credentials = new CredentialCache { { ServiceUri, "Negotiate", Credential } };
            }

            return base.Request(request);
        }

        protected HttpWebRequest PreparePostRequest(string data, string contentType)
        {
            var request = (HttpWebRequest)WebRequest.Create(ServiceUri);

            request.Method = "POST";

            if (Cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(ServiceUri, Cookies); 
            }

            request.ContentType = contentType; // "application/x-www-form-urlencoded";
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(data);
                dataStream.Flush();
            }

            return request;
        }

        protected HttpWebRequest PrepareGetRequest(string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(ServiceUri, "?" + data));

            request.Method = "GET";

            if (Cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(ServiceUri, Cookies);
            }

            return request;
        }
    }
}
