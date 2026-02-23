using Mailjet.Client;
using Mailjet.Repositories.Models.DataContracts.Base;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.SendV31
{
    /// <summary>
    /// Represents Send API v3.1
    /// </summary>
    [DataContract]
    public class SendV31RequestDataContract: RequestBaseDataContract
    {
        public ListDataContract<SendV31MessageDataContract> Messages { get; set; } = new ListDataContract<SendV31MessageDataContract>();
        
        [DataMember]
        public Boolean SandboxMode { get; set; }

        public int MaxEmailsPerBatch { get; set; }
    }
}
