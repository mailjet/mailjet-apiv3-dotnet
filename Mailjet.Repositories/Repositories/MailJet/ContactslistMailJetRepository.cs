using Mailjet.Client.Resources;
using Mailjet.Client;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Interfaces.Repositories;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Models;

namespace Mailjet.Repositories.Repositories.MailJet
{
    public class ContactslistMailJetRepository : MailJetRepositoryBase, IContactslistRepository
    {
        public ContactslistMailJetRepository(IMailJetConfiguration configurationRepository) : base(configurationRepository)
        {
        }

        public ContactslistDataContract Create(ContactslistDataContract model)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactslist.Resource,
                Body = model.ToJObject()
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactslistDataContract> results = rawData.ToObject<IList<ContactslistDataContract>>()!;

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

        public ContactslistDataContract Delete(int item)
        {
            throw new NotImplementedException();
        }

        public IList<ContactslistDataContract> List()
        {
            IMailjetClient client = this.GetMailjetClient();

            var contactslist = new ContactslistDataContract
            {
                Limit = 1000
            };

            MailjetRequest request = new()
            {
                Resource = Contactslist.Resource,
                Filters = contactslist.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactslistDataContract> results = rawData.ToObject<IList<ContactslistDataContract>>();

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

        public ContactslistDataContract Read(Int64 key)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactslist.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactslistDataContract> results = rawData.ToObject<IList<ContactslistDataContract>>();

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

        public ContactslistDataContract Update(ContactslistDataContract key)
        {
            throw new NotImplementedException();
        }
    }
}
