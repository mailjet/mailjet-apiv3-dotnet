using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Mailjet.Client
{
    public class UrlHelper
    {
        public static string CombineUrl(string url1, string url2)
        {
            url1 = url1.TrimEnd('/');
            url2 = url2.TrimStart('/');
            return string.Format("{0}/{1}", url1, url2);
        }

        public static string AddQuerryString(string url, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection != null && nameValueCollection.Any())
            {
                string querryString = string.Join("&", nameValueCollection.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value))));
                url = url.TrimEnd('/');
                url = string.Format("{0}?{1}", url, querryString);
            }

            return url;
        }
    }
}
