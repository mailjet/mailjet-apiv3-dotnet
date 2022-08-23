using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet
{
    public class MailJetPingResponseModel
    {
        public int Count { get; set; }

        public MailJetPingModel[] Data { get; set; }

        public int Total { get; set; }
    }
}
