using Mailjet.Repositories.Interfaces.Models;
using Mailjet.Repositories.Models.DataContracts.Base;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.DataContracts.SendV31
{
    public class SendV31MessageDataContract: RequestBaseDataContract
    {
        [DataMember]
        public IContactEmail From { get; set; } = new ContactEmailDataContract();

        [DataMember] 
        public ListDataContract<ContactEmailDataContract> To { get; set; } = new ListDataContract<ContactEmailDataContract>();

        [DataMember]
        public ListDataContract<ContactEmailDataContract> Cc { get; set; } = new ListDataContract<ContactEmailDataContract>();

        [DataMember]
        public ListDataContract<ContactEmailDataContract> Bcc { get; set; } = new ListDataContract<ContactEmailDataContract>();

        [DataMember] 
        public Int64 TemplateID { get; set; }

        [DataMember] 
        public Boolean TemplateLanguage { get; set; }

        [DataMember] 
        public string Subject { get; set; } = String.Empty;

        [DataMember] 
        public string Locale { get; set; }
    }

}
