using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace Patronum.WebService.Testing.WebServiceClients.Request
{
    public class SoapRequest
    {
        private readonly NetworkCredential _credential;
        private readonly string _url;
        private readonly string _action;

        public SoapRequest(string url, string action, NetworkCredential credential)
        {
            _url = url;
            _action = action;
            _credential = credential;
        }

        // todo refacroring
        public string Request(XDocument soapEnvelop)
        {           
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelop, webRequest);

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (var rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    return rd.ReadToEnd();
                }
            }
        }

        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            webRequest.Credentials = _credential;

            return webRequest;
        }


        private void InsertSoapEnvelopeIntoWebRequest(XDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
