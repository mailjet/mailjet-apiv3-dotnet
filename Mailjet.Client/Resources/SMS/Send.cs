using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client.Resources.SMS
{
    public static class Send
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("sms-send", null, ResourceType.V4);

        public const string From = "From";
        public const string To = "To";
        public const string Text = "Text";
        public const string MessageID = "MessageID";
        public const string SMSCount = "SMSCount";
        public const string CreationTS = "CreationTS";
        public const string SentTS = "SentTS";
        public const string Cost = "Cost";
        public const string Status = "Status";
    }
}
