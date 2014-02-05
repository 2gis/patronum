using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Patronum.WebService.Driver.HttpRequest;
using Patronum.WebService.Driver.HttpRequest.Request;

namespace Patronum.WebService.Driver.WebServiceClients.Rest
{
    public enum ContentType
    {
        FormData,
        Json,
        Xml,
        Jsv,
        Csv
    }

    public abstract class RestClient : WebServiceClient
    {
        protected RestClient(string serviceBaseUrl, ContentType contentType) : base(serviceBaseUrl)
        {
            ContentType = contentType;
        }

        public ContentType ContentType { get; set; }

        public HttpResponse Request(
                                    string relativeLink, 
                                    object data, 
                                    RequestMethod method = RequestMethod.Get,
                                    CookieCollection cookies = null)
        {
            var request = new RestRequest(new Uri(ServiceBaseUri + relativeLink));

            request.ApplyCredential(_userCredentials);

            request.ApplyCookies(cookies);

            if (method == RequestMethod.Get)
            {
                return request.GetRequest(PrepareRequestData(data, method));
            }
            else
            {
                string contentType;
                switch (ContentType)
                {
                    case ContentType.Xml:
                        contentType = "application/xml";
                        break;

                    case ContentType.Json:
                        contentType = "application/json";
                        break;

                    case ContentType.Csv:
                        contentType = "test/csv";
                        break;

                    case ContentType.Jsv:
                        contentType = "test/jsv";
                        break;

                    default:
                        contentType = "application/x-www-form-urlencoded";
                        break;
                }

                return request.PostRequest(PrepareRequestData(data, method), contentType);
            }
        }

        public string PrepareRequestData(object parameters, RequestMethod method)
        {
            var result = string.Empty;

            if (parameters == null)
            {
                return result;
            }

            if (method == RequestMethod.Post)
            {
                switch (ContentType)
                {
                    case ContentType.Json:
                        return JsonConvert.SerializeObject(parameters);

                    case ContentType.Xml:
                        using (var sw = new System.IO.StringWriter())
                        {
                            var serializer = new XmlSerializer(parameters.GetType());
                            serializer.Serialize(sw, this);
                            return sw.ToString();
                        }
                }
            }

            result = ((Dictionary<string, object>)parameters).Aggregate(
                result,
                (current, parameter) => current + (parameter.Key + '=' + parameter.Value.ToString() + '&'));

            return result.Trim(new[] {'&'});
        }
    }
}
