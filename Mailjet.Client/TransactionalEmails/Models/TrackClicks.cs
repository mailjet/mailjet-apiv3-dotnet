namespace Mailjet.Client.TransactionalEmails
{
    /// <summary>
    /// Enable or disable open tracking on this message.
    /// This property will overwrite the preferences selected in the Mailjet account.
    /// Equivalent of using X-Mailjet-TrackClick header through SMTP.
    /// </summary>
    public enum TrackClicks
    {
        /// <summary>
        /// use the values specified in the Mailjet account
        /// </summary>
        account_default,

        /// <summary>
        /// disable tracking for this message
        /// </summary>
        disabled,

        /// <summary>
        /// enable tracking for this message
        /// </summary>
        enabled
    }
}