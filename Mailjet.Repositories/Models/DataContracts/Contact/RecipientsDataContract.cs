using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{

    [DataContract]
    public class RecipientsDataContract
    {
        [DataMember]
        public IList<ContactEmailDataContract> Recipients { get; set; } = new List<ContactEmailDataContract>();
    }
}
