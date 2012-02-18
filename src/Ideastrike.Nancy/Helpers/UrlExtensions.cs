//
using System;
using System.Web;

namespace Ideastrike.Nancy.Helpers
{
    public static class UrlExtensions
    {
        public static string ToPublicUrl(this HttpContext httpContext, Uri relativeUri)
        {

            var uriBuilder = new UriBuilder
            {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            // Always set for now as we want to include the port number
            uriBuilder.Port = httpContext.Request.Url.Port;
           
            var err = new ElmahErrorHandler.LogEvent(uriBuilder + " ---- " + new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri);
            err.Raise();

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}