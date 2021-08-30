using System.Threading.Tasks;

namespace Mailjet.Client
{
    public interface IMailjetClient
    {
        Task<MailjetResponse> GetAsync(MailjetRequest request);
        Task<MailjetResponse> PostAsync(MailjetRequest request);
        Task<MailjetResponse> PutAsync(MailjetRequest request);
        Task<MailjetResponse> DeleteAsync(MailjetRequest request);
        MailjetResponse Get(MailjetRequest request);
        MailjetResponse Post(MailjetRequest request);
        MailjetResponse Put(MailjetRequest request);
        MailjetResponse Delete(MailjetRequest request);
    }
}
