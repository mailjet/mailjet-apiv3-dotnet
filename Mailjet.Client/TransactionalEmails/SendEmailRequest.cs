using System.Collections.Generic;

namespace Mailjet.Client.TransactionalEmails
{
    public class SendEmailRequest
    {
        public bool SandboxMode { get; set; }

        public bool AdvanceErrorHandling { get; set; }

        public List<TransactionalEmail> Messages { get; set; }
    }
}