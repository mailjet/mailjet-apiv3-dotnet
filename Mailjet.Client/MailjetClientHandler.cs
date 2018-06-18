using System.Net.Http;

namespace Mailjet.Client
{
    public class MailjetClientHandler : HttpClientHandler
    {
        public MailjetClientHandler()
        {
        }

        public MailjetClientHandler(string proxyUri)
        {
            Proxy = new DefaultProxy(proxyUri);
            UseProxy = true;
        }
    }
}
