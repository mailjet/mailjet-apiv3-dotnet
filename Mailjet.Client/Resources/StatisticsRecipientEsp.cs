namespace Mailjet.Client.Resources
{
    public static class StatisticsRecipientEsp
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("statistics/recipient-esp");

        public const string AttemptedMessagesCount = "AttemptedMessagesCount";
        public const string ClickedMessagesCount = "ClickedMessagesCount";
        public const string DeferredMessagesCount = "DeferredMessagesCount";
        public const string DeliveredMessagesCount = "DeliveredMessagesCount";
        public const string HardBouncedMessagesCount = "HardBouncedMessagesCount";
        public const string ESPName = "ESPName";
        public const string OpenedMessagesCount = "OpenedMessagesCount";
        public const string SoftBouncedMessagesCount = "SoftBouncedMessagesCount";
        public const string SpamReportsCount = "SpamReportsCount";
        public const string UnsubscribedMessagesCount = "UnsubscribedMessagesCount";

        public const string CampaignID = "CampaignID";
        public const string EspNames = "EspNames";
    }
}
