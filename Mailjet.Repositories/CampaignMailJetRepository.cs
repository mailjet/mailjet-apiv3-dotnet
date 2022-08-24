using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;
using Mailjet.Repositories.Repositories.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Repositories
{
    public class CampaignMailJetRepository : MailJetRepositoryBase, ICampaignRepository
    {
        public CampaignMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }
        public CampaignMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public CampaignDataContract Create(CampaignDataContract model)
        {
            IMailjetClient client = GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Campaign.Resource
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                CampaignDataContract[] results = rawData.ToObject<CampaignDataContract[]>();

                return results.Single();
            }
            else
            {
                var exceptionData = new MailJetExceptionModel();

                exceptionData.StatusCode = response.StatusCode;
                exceptionData.ErrorInfo = response.GetErrorInfo();
                exceptionData.ErrorMessage = response.GetErrorMessage();

                throw new MailJetException(exceptionData);

            }
        }

        public CampaignDataContract Delete(int item)
        {
            throw new NotImplementedException();
        }

        public IList<CampaignDataContract> List(PagingRequestBaseDataContract search)
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

                IList<CampaignDataContract> results = rawData.ToObject<IList<CampaignDataContract>>()!;

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

        public CampaignDataContract Read(int key)
        {
            throw new NotImplementedException();
        }

        public CampaignDataContract Update(CampaignDataContract key)
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