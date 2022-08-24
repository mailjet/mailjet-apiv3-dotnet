using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface IContactdataRepository : IRepositoryCreate<ContactdataDataContract, ContactdataCreateDataContract>
        , IRepositoryRead<ContactdataDataContract, long>
        , IRepositoryRead<ContactdataDataContract, string>
    {

    }
}
