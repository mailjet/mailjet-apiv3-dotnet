using System.Net;

namespace Mailjet.Client;

public static class UrlHelper
{
    public static string CombineUrl(string url1, string url2)
    {
        url1 = url1.TrimEnd('/');
        url2 = url2.TrimStart('/');
        return $"{url1}/{url2}";
    }

    public static string AddQuerryString(string url, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
    {
        if (nameValueCollection != null && nameValueCollection.Any())
        {
            string querryString = string.Join("&", nameValueCollection.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
            url = url.TrimEnd('/');
            url = $"{url}?{querryString}";
        }

        return url;
    }
}
