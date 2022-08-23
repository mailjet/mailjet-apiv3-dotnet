using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Base
{
    [DataContract]
    public abstract class RequestBaseDataContract
    {
        internal JObject ToJObject()
        {
            return (JObject)JToken.FromObject(this.ToDictionary());
        }


        internal IList<NameValueDataContract> ToNameValueList()
        {
           var dictionary = this.ToDictionary();

            return dictionary.Select((x) => new NameValueDataContract() { Name = x.Key, Value = x.Value }).ToList();
        }

        
        internal Dictionary<String, String> ToDictionary()
        {
            Dictionary<String, String> dctionary = this.GetType().GetProperties()
                .Where(p =>
                {
                    object value = p.GetValue(this, null);
                    if (value == null)
                    {
                        return false;
                    }
                    else if (value.ToString() == "0" || String.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return false;
                    }
                    return true;
                }).ToDictionary(d => d.Name,
                    d =>
                    {
                        //if (!d.GetType().IsPrimitive)
                        //{
                        //    return JsonSerializer.Serialize(d.GetValue(this, null));
                        //}
                        //else
                        //{
                            return d.GetValue(this, null)!.ToString();
                        //}
                    }
                 );

            return dctionary;
        }
    }
}