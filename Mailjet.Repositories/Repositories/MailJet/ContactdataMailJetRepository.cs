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

namespace Mailjet.Repositories.Repositories.MailJet
{
    public class ContactdataMailJetRepository : MailJetRepositoryBase, IContactdataRepository
    {
        public ContactdataMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public ContactdataDataContract Create(ContactdataCreateDataContract model)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactdata.Resource,
                Body = (JObject)JToken.FromObject(model)
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactdataDataContract> results = rawData.ToObject<IList<ContactdataDataContract>>();

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
        public ContactdataDataContract Read(long key)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Contactdata.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactdataDataContract> results = rawData.ToObject<IList<ContactdataDataContract>>();

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

        public ContactdataDataContract Read(string email)
        {
            IMailjetClient client = this.GetMailjetClient();

            ContactdataDataContract contactdataRequest = new()
            {
                ContactEmail = email
            };

            MailjetRequest request = new()
            {
                Resource = Contactdata.Resource,
                Filters = contactdataRequest.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<ContactdataDataContract> results = rawData.ToObject<IList<ContactdataDataContract>>();

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

    }
}

//https://api.mailjet.com/v3/REST/contactdata
//{
//    "Datatype": "str",
//    "Name": "first_name"
//}