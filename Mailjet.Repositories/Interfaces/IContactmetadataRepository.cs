using Mailjet.Repositories.Interfaces.Bases;
using Mailjet.Repositories.Models.DataContracts.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface IContactmetadataRepository :
        IRepositoryCreate<ContactmetadataDataContract>, 
        IRepositoryList<ContactmetadataDataContract>
    {

        IList<ContactmetadataDataContract> Create<T>()
                    where T : class, new();
    }
}
