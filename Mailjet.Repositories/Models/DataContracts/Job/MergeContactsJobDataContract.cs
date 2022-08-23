using Mailjet.Repositories.Models.DataContracts.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.DataContracts.Job
{
    public class MergeContactsJobDataContract<T>
        where T : class, new()
    {
        //public IList<ContactslistManageDataContract> ContactsLists { get; set }
        public IList<ContactBasicDataContract<T>>? Contacts { get; set; }

    }
}
