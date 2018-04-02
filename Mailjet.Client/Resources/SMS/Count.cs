using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client.Resources.SMS
{
    public static class Count
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("sms/count", null, ResourceType.V4);

        public const string To = "To";
        public const string FromTS = "FromTS";
        public const string ToTS = "ToTS";
        public const string StatusCode = "StatusCode";
    }
}
