using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet
{
    public class MailJetPingModel
    {
        public string ACL { get; set; }
        public string APIKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public bool IsMaster { get; set; }
        public string Name { get; set; }
        public int QuarantineValue { get; set; }
        public int RegionID { get; set; }
        public string Runlevel { get; set; }
        public string SecretKey { get; set; }
        public int Skipspamd { get; set; }
        public string TrackHost { get; set; }
        public int UserID { get; set; }
    }
}
