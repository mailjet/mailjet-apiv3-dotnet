using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Repositories.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories
{
    public class TemplateDetailcontentMailJetRepository : MailJetRepositoryBase, ITemplateDetailcontentRepository
    {
        public TemplateDetailcontentMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }
        public TemplateDetailcontentMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }
        public TemplateDetailcontentDataContract Read(Int64 templateId)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = TemplateDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(templateId)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<TemplateDetailcontentDataContract> results = rawData.ToObject<IList<TemplateDetailcontentDataContract>>()!;

                return results.Single();
            }
            else
            {
                MailJetExceptionModel exceptionData = new()
                {
                    StatusCode = response.StatusCode,
                    ErrorInfo = response.GetErrorInfo(),
                    ErrorMessage = response.GetErrorMessage()
                };

                throw new MailJetException(exceptionData);
            }
        }

        public TemplateDetailcontentDataContract Create(Int64 templateId, TemplateDetailcontentDataContract templateDetailcontent)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = TemplateDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(templateId),
                Body = (JObject)JToken.FromObject(templateDetailcontent)
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<TemplateDetailcontentDataContract> results = rawData.ToObject<IList<TemplateDetailcontentDataContract>>()!;

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

//https://api.mailjet.com/v3/REST/template/{{template_ID}}/detailcontent
//{
//    "Headers": {
//        "Subject": "Hello There!",
//        "From": "John Doe <email@example.com>",
//        "Reply-To": ""
//    },
//    "Html-part": "<html><body><p>Hello {{var:name}}</p></body></html>",
//    "Text-part": "Hello {{var:name}}",
//    "Mjml-part": ""
//}