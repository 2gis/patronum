using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Patronum.WebService.Testing.HttpRequest.Request
{
    public class RestRequest : HttpRequest
    {
        public RestRequest(Uri uri, NetworkCredential credential) : base(uri, credential)
        {
        }

        public HttpResponse Request(Dictionary<string, string> parameters, RequestMethod method = RequestMethod.Get)
        {
            string data = this.PrepareRequestData(parameters);

            var request = method.Equals(RequestMethod.Get) ? PrepareGetRequest(data) : PreparePostRequest(data);
            request.Credentials = new CredentialCache { { ServiceUri, "Negotiate", Credential } };

            return base.Request(request);
        }

        protected HttpWebRequest PreparePostRequest(string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(ServiceUri);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
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

            return request;
        }

        protected string PrepareRequestData(Dictionary<string, string> parameters)
        {
            var result = string.Empty;

            if (parameters == null)
            {
                return result;
            }

            result = parameters.Aggregate(result, (current, parameter) => current + (parameter.Key + '=' + HttpUtility.UrlEncode(parameter.Value) + '&'));
            return result.Trim(new[] { '&' });
        }
    }
}
