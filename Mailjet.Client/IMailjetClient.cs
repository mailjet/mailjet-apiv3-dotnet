using System.Collections.Generic;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;

namespace Mailjet.Client
{
    public interface IMailjetClient
    {
        Task<MailjetResponse> GetAsync(MailjetRequest request);
        Task<MailjetResponse> PostAsync(MailjetRequest request);
        Task<MailjetResponse> PutAsync(MailjetRequest request);
        Task<MailjetResponse> DeleteAsync(MailjetRequest request);
        Task<TransactionalEmailResponse> SendTransactionalEmailAsync(TransactionalEmail transactionalEmail, bool isSandboxMode = false, bool advanceErrorHandling = true);
        Task<TransactionalEmailResponse> SendTransactionalEmailsAsync(IEnumerable<TransactionalEmail> transactionalEmails, bool isSandboxMode = false, bool advanceErrorHandling = true);
    }
}
