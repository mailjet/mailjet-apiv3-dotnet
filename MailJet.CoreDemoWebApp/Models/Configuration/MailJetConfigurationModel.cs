using Mailjet.Repositories.Interfaces.Configuration;

namespace MailJet.CoreDemoWebApp.Models.Configuration
{
    public class MailJetConfigurationModel : IMailJetConfiguration
    {
        public string MailJetApiPublicKey { get; set; } = "";

        public string MailJetApiPrivateKey { get; set; } = "";
    }
}
