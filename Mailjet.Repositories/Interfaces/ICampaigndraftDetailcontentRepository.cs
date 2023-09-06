using Mailjet.Client.Resources;
using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ICampaigndraftDetailcontentRepository :
        IRepositoryCreate<CampaigndraftDetailcontentDataContract, Int64, CampaigndraftDetailcontentDataContract>,
        IRepositoryRead<CampaigndraftDetailcontentDataContract, Int64>
    {
    }
}
