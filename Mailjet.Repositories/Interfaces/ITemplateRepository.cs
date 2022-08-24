using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        TemplateReponseDataContract Create(TemplateDataContract template);
        IList<TemplateReponseDataContract> List();
        TemplateReponseDataContract Get(int templateId);
    }
}
