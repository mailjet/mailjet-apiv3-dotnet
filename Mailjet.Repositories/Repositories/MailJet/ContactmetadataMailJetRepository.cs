using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Exceptions;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Repositories.Base;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Interfaces.Repositories;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Mailjet.Repositories.Repositories.MailJet
{
    public class ContactmetadataMailJetRepository : MailJetRepositoryBase, IContactmetadataRepository
    {
        public ContactmetadataMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public ContactmetadataDataContract Create(ContactmetadataDataContract model)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactmetadata.Resource,
                Body = model.ToJObject()
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactmetadataDataContract> results = rawData.ToObject<IList<ContactmetadataDataContract>>()!;

                    return results.Single();
               
            }
            else
            {
                var exceptionData = new MailJetExceptionModel
                {
                    StatusCode = response.StatusCode,
                    ErrorInfo = response.GetErrorInfo(),
                    ErrorMessage = response.GetErrorMessage()
                };

                throw new MailJetException(exceptionData);
            }
        }

        public IList<ContactmetadataDataContract> Create<T>() where T : class, new()
        {
            var modelInstance = new T();
            var properties = modelInstance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(property => {
                Boolean result = property.CanRead && property.CanWrite;
                return result;
            }).ToList();

            IList<ContactmetadataDataContract> existingContactmetadatas = this.List();

            var missingPropertires = properties.Where(x => !existingContactmetadatas.Any(m => m.Name == x.Name)).ToList();

            IList<ContactmetadataDataContract> addedContactmetadatas = new List<ContactmetadataDataContract>();
            foreach (PropertyInfo property in missingPropertires)
            {
                ContactmetadataDataContract contactmetadataUnsaved = new()
                {
                    Name = property.Name
                };

                var contactmetadata = this.Create(contactmetadataUnsaved);

                addedContactmetadatas.Add(contactmetadata);
            }

            return addedContactmetadatas;
        }

        public IList<ContactmetadataDataContract> List()
        {
            IMailjetClient client = this.GetMailjetClient();

            var contactmetadata = new ContactmetadataDataContract
            {
                Limit = 1000
            };

            MailjetRequest request = new()
            {
                Resource = Contactmetadata.Resource,
                Filters = contactmetadata.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactmetadataDataContract> results = rawData.ToObject<IList<ContactmetadataDataContract>>();

                return results;
            }
            else
            {
                var exceptionData = new MailJetExceptionModel
                {
                    StatusCode = response.StatusCode,
                    ErrorInfo = response.GetErrorInfo(),
                    ErrorMessage = response.GetErrorMessage()
                };

                throw new MailJetException(exceptionData);
            }

        }
    }
}


//https://api.mailjet.com/v3/REST/contactmetadataa
//{
//    "Datatype": "str",
//  "Name": "first_name"
//}