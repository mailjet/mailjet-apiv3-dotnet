using System;
using System.Net;

namespace Mailjet.Client
{
    public class DefaultProxy : IWebProxy
    {
        private readonly Uri _proxyUri;

        public DefaultProxy(string proxyUri)
        {
            _proxyUri = new Uri(proxyUri);
        }

        public DefaultProxy(Uri proxyUri)
        {
            _proxyUri = proxyUri;
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return _proxyUri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}
