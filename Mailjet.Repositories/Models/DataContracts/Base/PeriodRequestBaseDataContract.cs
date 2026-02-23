
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Base
{
    public class PeriodRequestBaseDataContract : PagingRequestBaseDataContract
    {
        public String? Period { get; set; }
        public DateTime? FromTS { get; set; }
        public DateTime? ToTS { get; set; }

    }
}