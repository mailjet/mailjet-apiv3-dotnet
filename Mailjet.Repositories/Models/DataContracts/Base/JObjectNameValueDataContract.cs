using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Base
{
    public class JObjectNameValueDataContract
    {
        public String Name { get; set; }
        public JObject Value { get; set; }
    }
}
