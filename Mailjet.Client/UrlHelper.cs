using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
