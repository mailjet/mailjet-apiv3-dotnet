using Mailjet.Client.Resources;
using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Repositories
{
    public interface IContactslistRepository : 
        IRepositoryCreate<ContactslistDataContract>,
        IRepositoryList<ContactslistDataContract>,
        IRepositoryRead<ContactslistDataContract, Int64>
    {
    }
}
