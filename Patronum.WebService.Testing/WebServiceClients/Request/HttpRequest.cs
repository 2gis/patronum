using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Patronum.WebService.Testing.WebServiceClients.Request
{
    public class HttpRequest
    {
        private readonly NetworkCredential _credential;

        private Uri _uri;

        public HttpRequest(Uri uri, NetworkCredential credential)
        {
            _uri = uri;
            _credential = credential;
        }

        public string PrepareRequestData(Dictionary<string, string> parameters)
        {
            string result = string.Empty;
            if (parameters == null)
            {
                return result;
            }

            result = parameters.Aggregate(result, (current, parameter) => current + (parameter.Key + '=' + HttpUtility.UrlEncode(parameter.Value) + '&'));
            return result.Trim(new[] { '&' });
        }

        public HttpResponse Request(Dictionary<string, string> parameters, string method = "GET")
        {
            string data = PrepareRequestData(parameters);

            var response = new HttpResponse();

            if (method.Equals("GET"))
            {
                _uri = new Uri(_uri, "?" + data);
            }

            var request = (HttpWebRequest)WebRequest.Create(_uri);

            request.Credentials = new CredentialCache { { _uri, "Negotiate", _credential } };

            if (method.Equals("POST"))
            {
                request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";
                using (var dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(data);
                    dataStream.Flush();
                    dataStream.Close();
                }
            }

            // response.Request = Uri.ToString();
            try
            {
                var result = (HttpWebResponse)request.GetResponse();
                response.Code = (int)result.StatusCode;
                using (var reader = new StreamReader(result.GetResponseStream()))
                {
                    response.Text = reader.ReadToEnd();
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
