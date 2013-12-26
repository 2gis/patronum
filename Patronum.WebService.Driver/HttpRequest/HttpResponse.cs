
using System;
using System.Net;

namespace Patronum.WebService.Driver.HttpRequest
{
    public class HttpResponse : WebResponse
    {
        public int Code;

        public string Text;

        public Uri Uri;
    }
}
