using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public class ResourceId
    {
        public enum ResourceIdType
        {
            Alphanumeric,
            Numeric,
        }

        public ResourceIdType Type { get; private set; }

        public string Id { get; private set; }

        public ResourceId(long id)
        {
            Type = ResourceIdType.Numeric;
            Id = id.ToString();
        }

        public ResourceId(string id)
        {
            Type = ResourceIdType.Alphanumeric;
            Id = id;
        }

        public static ResourceId Alphanumeric(string id)
        {
            return new ResourceId(id);
        }

        public static ResourceId Numeric(long id)
        {
            return new ResourceId(id);
        }
    }
}
