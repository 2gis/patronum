using System;
using System.Net;
using System.Xml.Linq;

namespace Patronum.WebService.Driver.HttpRequest.Request
{
    public class SoapRequest : HttpRequest
    {
        public SoapRequest(Uri uri, string action, NetworkCredential credential = null)
            : base(uri)
        {
            Action = action;
            ApplyCredential(credential);
        }

        public string Action { get; set; }


        public HttpResponse Request(XDocument soapEnvelop)
        {           
            PrepareSoapRequest();
            InsertSoapEnvelopeIntoWebRequest(soapEnvelop);

            return base.Request();
        }

        private void PrepareSoapRequest()
        {
            _request.Headers.Add("SOAPAction", Action);
            _request.ContentType = "text/xml;charset=\"utf-8\"";
            _request.Headers.Add(HttpRequestHeader.Accept, "text/xml");
            _request.Method = "POST";
        }
        
        private void InsertSoapEnvelopeIntoWebRequest(XDocument soapEnvelopeXml)
        {
            using (var stream = _request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
