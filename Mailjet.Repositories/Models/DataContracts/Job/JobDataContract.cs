using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.DataContracts.Job
{
    public class JobDataContract : JobIdDataContract
    {
        public int Count { get; set; }
        public string Error { get; set; }
        public string ErrorFile { get; set; }
        public DateTime JobEnd { get; set; }
        public DateTime JobStart { get; set; }
        public string Status { get; set; }
    }
}
