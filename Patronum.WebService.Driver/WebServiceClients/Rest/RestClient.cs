using System;
using System.Collections.Generic;
using System.Linq;
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
                                    RequestMethod method = RequestMethod.Get)
        {
            var request = new RestRequest(new Uri(ServiceBaseUri + relativeLink), _userCredentials);

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

            return method == RequestMethod.Get ? 
                request.GetRequest(PrepareRequestData(data)) : 
                request.PostRequest(PrepareRequestData(data), contentType);
        }

        public string PrepareRequestData(object parameters)
        {
            var result = string.Empty;

            if (parameters == null)
            {
                return result;
            }

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

                default:
                    result = ((Dictionary<string, string>)parameters).Aggregate(
                        result,
                        (current, parameter) => current + (parameter.Key + '=' + parameter.Value + '&'));

                    return result.Trim(new[] { '&' });
            }
        }
    }
}
