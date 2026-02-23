using Mailjet.Client.TransactionalEmails.Response;
using Mailjet.Repositories.Models.DataContracts.SendV31;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces
{
    public interface ISendV31Repository
    {
        IList<MessageResult> Send(SendV31RequestDataContract sendV31Request);
    }
}
