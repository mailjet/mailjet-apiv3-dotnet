using Mailjet.Repositories.Interfaces.Methods;
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
using System.Xml.Linq;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Base
{
    [DataContract]
    public abstract class RequestBaseDataContract : IConvertToJSON
    {
        internal JObject ToJObject()
        {
            return JObject.Parse(this.ToJSON());
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
                        return d.GetValue(this, null)!.ToString()!;
                        //}
                    }
                 );

            return dctionary;
        }

        public string ToJSON()
        {
            IList<String> listResult = new List<String>();

            IList<PropertyInfo> properties = this.GetType().GetProperties()
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
                }).ToList();

            foreach (PropertyInfo property in properties)
            {
                var name = property.Name;
                object value = property.GetValue(this, null)!;
                TypeCode typeCode = Type.GetTypeCode(value.GetType());

                if (value is IConvertToJSON)
                {
                    listResult.Add($"\"{name}\": {((IConvertToJSON)value).ToJSON()}");
                }
                else if (typeCode == TypeCode.Boolean)
                {
                    listResult.Add($"\"{name}\": {value.ToString()!.ToLower()}");
                }
                else if (typeCode == TypeCode.Byte
                         || typeCode == TypeCode.SByte
                         || typeCode == TypeCode.UInt16
                         || typeCode == TypeCode.UInt32
                         || typeCode == TypeCode.UInt64
                         || typeCode == TypeCode.Int16
                         || typeCode == TypeCode.Int32
                         || typeCode == TypeCode.Int64
                         || typeCode == TypeCode.Decimal
                         || typeCode == TypeCode.Double
                         || typeCode == TypeCode.Single)
                {
                    listResult.Add($"\"{name}\": {value}");
                }
                else
                {
                    listResult.Add($"\"{name}\": \"{value}\"");
                }
            }

            return "{" + string.Join(",", listResult) + "}";
        }
    }
}