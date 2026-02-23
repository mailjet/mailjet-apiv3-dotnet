using Mailjet.Repositories.Interfaces.Methods;
using Mailjet.Repositories.Models.DataContracts.SendV31;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.DataContracts.Base
{
    public class ListDataContract<T> : List<T>, IConvertToJSON
        where T : class, IConvertToJSON
    {

        public string ToJSON()
        {
            var entries = this.Select(d => $"{d.ToJSON()}");
            return "[" + string.Join(",", entries) + "]";
        }
    }
}
