using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Repositories
{
    public interface ITemplateDetailcontentRepository
    {
        TemplateDetailcontentDataContract Get(int templateId);
        TemplateDetailcontentDataContract Create(TemplateDetailcontentDataContract templateDetailcontent);
    }
}