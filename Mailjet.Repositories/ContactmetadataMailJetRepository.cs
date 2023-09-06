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
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Mailjet.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;

namespace Mailjet.Repositories
{
    public class ContactmetadataMailJetRepository : MailJetRepositoryBase, IContactmetadataRepository
    {
        public ContactmetadataMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }
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
            var properties = modelInstance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(property =>
            {
                bool result = property.CanRead && property.CanWrite;
                return result;
            }).ToList();

            IList<String> metadatas = properties.Select(x=>x.Name).ToList();

            return this.Create(metadatas);
        }

        public IList<ContactmetadataDataContract> Create(IList<string> metadatas)
        {
            IList<ContactmetadataDataContract> existingContactmetadatas = this.List(new ContactmetadataDataContract
            {
                Limit = 1000
            });

            IList<String> missingMetadatas = metadatas.Where(x => !existingContactmetadatas.Any(m => m.Name == x)).ToList();

            IList<ContactmetadataDataContract> addedContactmetadatas = new List<ContactmetadataDataContract>();
            foreach (String metadata in missingMetadatas)
            {
                ContactmetadataDataContract contactmetadataUnsaved = new()
                {
                    Name = metadata
                };

                var contactmetadata = Create(contactmetadataUnsaved);

                addedContactmetadatas.Add(contactmetadata);
            }

            return addedContactmetadatas;
        }

        public IList<ContactmetadataDataContract> List(PagingRequestBaseDataContract query)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactmetadata.Resource,
                Filters = query.ToDictionary()
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