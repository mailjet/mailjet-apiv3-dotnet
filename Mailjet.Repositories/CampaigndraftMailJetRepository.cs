using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Mailjet.Repositories
{
    public class CampaigndraftMailJetRepository : MailJetRepositoryBase, ICampaigndraftRepository
    {
        public CampaigndraftMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }

        public CampaigndraftMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public CampaigndraftDataContract Create(CampaigndraftCreateDataContract model)
        {
            IMailjetClient client = GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Campaigndraft.Resource,
                Body = (JObject)JToken.FromObject(model)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                CampaigndraftDataContract[] results = rawData.ToObject<CampaigndraftDataContract[]>();

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

        public IList<CampaigndraftDataContract> List(PagingRequestBaseDataContract search)
        {
            IMailjetClient client = GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Template.Resource,
                Filters = search.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftDataContract> results = rawData.ToObject<IList<CampaigndraftDataContract>>()!;

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

        public CampaigndraftDataContract Read(Int64 key)
        {
            IMailjetClient client = GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Campaigndraft.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftDataContract> results = rawData.ToObject<IList<CampaigndraftDataContract>>();

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

        public CampaigndraftDataContract Update(CampaigndraftDataContract key)
        {
            throw new NotImplementedException();
        }

        public CampaigndraftDataContract Delete(long item)
        {
            throw new NotImplementedException();
        }
    }
}

//https://api.mailjet.com/v3/REST/campaigndraft
//{
//    "AXFraction": 0,
//    "AXFractionName": "Version A",
//    "AXTesting": 123456,
//    "Current": 345678,
//    "EditMode": "tool2",
//    "IsStarred": false,
//    "IsTextPartIncluded": true,
//    "ReplyEmail": "replyto@mailjet.com",
//    "SenderName": "Your Mailjet Pilot",
//    "TemplateID": 123456,
//    "Title": "My Mailjet Campaign",
//    "ContactsListID": 123456,
//    "Locale": "en_US",
//    "SegmentationID": 123,
//    "Sender": 1234,
//    "SenderEmail": "pilot@mailjet.com",
//    "Subject": "Your email flight plan!"
//}