using System;
using System.Net;
using System.Xml.Linq;

namespace Patronum.WebService.Testing.HttpRequest.Request
{
    public class SoapRequest : HttpRequest
    {
        protected readonly string Action;

        public SoapRequest(Uri uri, string action, NetworkCredential credential) : base(uri, credential)
        {
            Action = action;
        }


        public HttpResponse Request(XDocument soapEnvelop)
        {           
            HttpWebRequest webRequest = PrepareSoapRequest(ServiceUri, Action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelop, webRequest);

            return base.Request(webRequest);
        }

        private HttpWebRequest PrepareSoapRequest(Uri uri, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            webRequest.Credentials = Credential;

            return webRequest;
        }
        
        private void InsertSoapEnvelopeIntoWebRequest(XDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
