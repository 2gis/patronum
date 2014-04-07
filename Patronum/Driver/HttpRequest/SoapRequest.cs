
namespace Patronum.Driver.HttpRequest
{
    using System;
    using System.Net;
    using System.Xml.Linq;

    public class SoapRequest : HttpRequest
    {
        public SoapRequest(Uri uri, string action)
            : base(uri)
        {
            Action = action;
        }

        public string Action { get; set; }


        public HttpResponse Request(XDocument soapEnvelop)
        {           
            PrepareSoapRequest();
            InsertSoapEnvelopeIntoWebRequest(soapEnvelop);

            return Request();
        }

        private void PrepareSoapRequest()
        {
            WebRequestSource.Headers.Add("SOAPAction", Action);
            WebRequestSource.ContentType = "text/xml;charset=\"utf-8\"";
           ((HttpWebRequest)WebRequestSource).Accept = "text/xml";
            WebRequestSource.Method = "POST";
        }

        private void InsertSoapEnvelopeIntoWebRequest(XDocument soapEnvelopeXml)
        {
            using (var stream = WebRequestSource.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
