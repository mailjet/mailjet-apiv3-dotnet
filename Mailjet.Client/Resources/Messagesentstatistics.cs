using System;

namespace Mailjet.Client.Resources
{
    [Obsolete]
    public static class Messagesentstatistics
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("messagesentstatistics");

        public const string ArrivalTs = "ArrivalTs";
        public const string Blocked = "Blocked";
        public const string Bounce = "Bounce";
        public const string BounceDate = "BounceDate";
        public const string BounceReason = "BounceReason";
        public const string Campaign = "Campaign";
        public const string Click = "Click";
        public const string CntRecipients = "CntRecipients";
        public const string ComplaintDate = "ComplaintDate";
        public const string Contact = "Contact";
        public const string Details = "Details";
        public const string FBLSource = "FBLSource";
        public const string MessageID = "MessageID";
        public const string Open = "Open";
        public const string Queued = "Queued";
        public const string Sent = "Sent";
        public const string Spam = "Spam";
        public const string State = "State";
        public const string StatePermanent = "StatePermanent";
        public const string Status = "Status";
        public const string ToEmail = "ToEmail";
        public const string Unsub = "Unsub";
        public const string AllMessages = "AllMessages";
        public const string BounceEventFromTs = "BounceEventFromTs";
        public const string BounceEventToTs = "BounceEventToTs";
        public const string CampaignID = "CampaignID";
        public const string CampaignStatus = "CampaignStatus";
        public const string ContactsList = "ContactsList";
        public const string CustomCampaign = "CustomCampaign";
        public const string CustomID = "CustomID";
        public const string From = "From";
        public const string FromDomain = "FromDomain";
        public const string FromID = "FromID";
        public const string FromTS = "FromTS";
        public const string FromType = "FromType";
        public const string IsDeleted = "IsDeleted";
        public const string IsNewsletterTool = "IsNewsletterTool";
        public const string IsStarred = "IsStarred";
        public const string MessageStatus = "MessageStatus";
        public const string Period = "Period";
        public const string RawData = "RawData";
        public const string ShowExtraData = "ShowExtraData";
        public const string SpamEventFromTs = "SpamEventFromTs";
        public const string SpamEventToTs = "SpamEventToTs";
        public const string ToTS = "ToTS";
        public const string Limit = "Limit";
        public const string Offset = "Offset";
        public const string Sort = "Sort";
        public const string CountOnly = "CountOnly";

        public const string SoftbouncedCount = "SoftbouncedCount";
        public const string HardbouncedCount = "HardbouncedCount";
        public const string DeferredCount = "DeferredCount";
    }
}


