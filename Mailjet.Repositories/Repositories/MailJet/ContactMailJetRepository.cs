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
using Mailjet.Repositories.Models.DataContracts.Contact;
using Newtonsoft.Json.Linq;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;

namespace Mailjet.Repositories.Repositories.MailJet
{
    public class ContactMailJetRepository : MailJetRepositoryBase, IContactRepository
    {
        public ContactMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public ContactDataContract Create(ContactDataContract model)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contact.Resource,
                Body = model.ToJObject()
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactDataContract> results = rawData.ToObject<IList<ContactDataContract>>();

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

        public ContactDataContract Read(long key)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contact.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactDataContract> results = rawData.ToObject<IList<ContactDataContract>>()!;

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

        public ContactDataContract? Read(string email)
        {
            IMailjetClient client = this.GetMailjetClient();

            ContactDataContract contactRequest = new ()
            {
                Email = email
            };

            MailjetRequest request = new()
            {
                Resource = Contact.Resource,
                Filters = contactRequest.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactDataContract> results = rawData.ToObject<IList<ContactDataContract>>()!;

                if (results.Any())
                {
                    return results.Single();
                }
                else
                {
                    return null;
                }
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

        public ContactDataContract Update(ContactDataContract key)
        {
            throw new NotImplementedException();
        }

        public ContactDataContract Delete(long item)
        {
            throw new NotImplementedException();
        }

        public IList<ContactDataContract> List(PagingRequestBaseDataContract search)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contact.Resource,
                Filters = search.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactDataContract> results = rawData.ToObject<IList<ContactDataContract>>();

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

        public IList<ContactDataContract> ListByContacsList(Int64 contactslistId)
        {

            IMailjetClient client = this.GetMailjetClient();

            var contactDataContract = new ContactDataContract
            {
                Limit = 5000,
                ContactsList = contactslistId.ToString()
            };

            MailjetRequest request = new()
            {
                Resource = Contact.Resource,
                Filters = contactDataContract.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactDataContract> results = rawData.ToObject<IList<ContactDataContract>>();

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
