using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Models
{
    public interface IContactEmail
    {
        [DataMember]
        string Email { get; set; } 
        [DataMember]
        string Name { get; set; } 
    }
}
