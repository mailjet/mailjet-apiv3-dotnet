using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet
{
    public class MailJetExceptionModel
    {
        public int StatusCode { get; set; }
        public string ErrorInfo { get; set; }
        public string ErrorMessage { get; set; }
    }
}
