using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Repositories.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories
{
    public class TemplateMailJetRepository : MailJetRepositoryBase, ITemplateRepository
    {
        public TemplateMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }
        public TemplateMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public IList<TemplateReponseDataContract> List(PagingRequestBaseDataContract query)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Template.Resource,
                Filters = query.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<TemplateReponseDataContract> results = rawData.ToObject<IList<TemplateReponseDataContract>>()!;

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

        public TemplateReponseDataContract Create(TemplateDataContract template)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Template.Resource,
                Body = (JObject)JToken.FromObject(template)
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                TemplateReponseDataContract[] results = rawData.ToObject<TemplateReponseDataContract[]>()!;

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

        public TemplateReponseDataContract Read(long templateId)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Template.Resource,
                ResourceId = ResourceId.Numeric(templateId)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                TemplateReponseDataContract[] results = rawData.ToObject<TemplateReponseDataContract[]>()!;

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

        public TemplateReponseDataContract ReadLast()
        {
            IMailjetClient client = this.GetMailjetClient();

            PagingRequestBaseDataContract query = new()
            {
                Limit = 1,
                Sort = "CreatedAt DESC",
            };

            MailjetRequest request = new()
            {
                Resource = Template.Resource,
                Filters = query.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<TemplateReponseDataContract> results = rawData.ToObject<IList<TemplateReponseDataContract>>()!;

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
    }
}

//https://api.mailjet.com/v3/REST/template
//{
//    "Author": "John Doe",
//    "Categories": [
//        "commerce"
//    ],
//    "Copyright": "John Doe",
//    "Description": "Used for discount promotion.",
//    "EditMode": 1,
//    "IsStarred": true,
//    "IsTextPartGenerationEnabled": true,
//    "Locale": "en_US",
//    "Name": "Promo Code",
//    "OwnerType": "apikey",
//    "Presets": "{\"h1\":{\"fontFamily\":\"'Arial Black', Helvetica, Arial, sans-serif\"}}",
//    "Purposes": [
//        "marketing"
//    ]
//}
