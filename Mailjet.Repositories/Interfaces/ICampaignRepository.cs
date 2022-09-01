using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ICampaignRepository: IRepositoryRead<CampaignDataContract, Int64>, IRepositoryUpdate<CampaignDataContract, DateTimeOffset>, IRepositoryList<CampaignDataContract, PagingRequestBaseDataContract>
    {

    }
}
