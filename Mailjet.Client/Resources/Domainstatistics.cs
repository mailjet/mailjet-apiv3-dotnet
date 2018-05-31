using System;

namespace Mailjet.Client.Resources
{
    public static class Domainstatistics
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("domainstatistics");

        public const string BlockedCount = "BlockedCount";
        public const string BouncedCount = "BouncedCount";
        public const string ClickedCount = "ClickedCount";
        public const string DeliveredCount = "DeliveredCount";
        public const string Domain = "Domain";
        public const string ID = "ID";
        public const string OpenedCount = "OpenedCount";
        public const string ProcessedCount = "ProcessedCount";
        public const string QueuedCount = "QueuedCount";
        public const string SpamComplaintCount = "SpamComplaintCount";
        public const string UnsubscribedCount = "UnsubscribedCount";
        public const string CampaignID = "CampaignID";

        [Obsolete]
        public const string CampaignStatus = "CampaignStatus";

        public const string ContactsList = "ContactsList";
        public const string CustomCampaign = "CustomCampaign";
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
        public const string ToTS = "ToTS";
        public const string Limit = "Limit";
        public const string Offset = "Offset";
        public const string Sort = "Sort";
        public const string CountOnly = "CountOnly";
    }
}


