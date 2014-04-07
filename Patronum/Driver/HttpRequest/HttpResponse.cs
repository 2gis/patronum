
namespace Patronum.Driver.HttpRequest
{
    using System;
    using System.Net;

    public class HttpResponse : WebResponse
    {
        public int Code;

        public string Text;

        public Uri Uri;
    }
}
