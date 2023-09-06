using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Base
{
    public class PagingRequestBaseDataContract: RequestBaseDataContract
    {
        public Int32? Limit { get; set; }
        public Int32? Offset { get; set; }
        public Boolean? CountOnly { get; set; }
        public String? Sort { get; set; }
    }
}
