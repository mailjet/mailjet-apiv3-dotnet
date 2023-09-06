using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ICampaigndraftScheduleRepository :
        IRepositoryCreate<CampaigndraftScheduleDataContract, Int64, CampaigndraftScheduleDataContract>, 
        IRepositoryRead<CampaigndraftScheduleDataContract, Int64>,
        IRepositoryUpdate<CampaigndraftScheduleDataContract, Int64, DateTimeOffset>, 
        IRepositoryDelete<CampaigndraftScheduleDataContract, Int64>
    {
    }
}
