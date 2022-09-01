using Mailjet.Client.Resources;
using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface IContactslistRepository :
        IRepositoryCreate<ContactslistDataContract>,
        IRepositoryList<ContactslistDataContract, PagingRequestBaseDataContract>,
        IRepositoryRead<ContactslistDataContract, long>
    {
        ContactslistDataContract ReadLast();
    }
}
