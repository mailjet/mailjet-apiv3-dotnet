using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Configuration
{
    public interface IMailJetConfiguration
    {
        string GetMailJetApiPublicKey();

        string GetMailJetApiPrivateKey();

    }
}
