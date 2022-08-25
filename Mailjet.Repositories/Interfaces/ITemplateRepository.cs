using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ITemplateRepository :
        IRepositoryCreate<TemplateReponseDataContract, TemplateDataContract>,
        IRepositoryRead<TemplateReponseDataContract, Int64>,
        IRepositoryList<TemplateReponseDataContract, PagingRequestBaseDataContract>
    {

    }
}
