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
using Mailjet.Repositories.Models.MailJet.DataContracts.Template;

namespace Mailjet.Repositories
{
    public class CampaigndraftDetailcontentMailJetRepository : MailJetRepositoryBase, ICampaigndraftDetailcontentRepository
    {
        public CampaigndraftDetailcontentMailJetRepository(IMailJetConfiguration configurationRepository) : base(configurationRepository)
        {

        }

        public CampaigndraftDetailcontentDataContract Create(long campaigndraftId, CampaigndraftDetailcontentDataContract campaigndraftDetailcontent)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = CampaigndraftDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(campaigndraftId),
                Body = (JObject)JToken.FromObject(campaigndraftDetailcontent)
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftDetailcontentDataContract> results = rawData.ToObject<IList<CampaigndraftDetailcontentDataContract>>()!;

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

        public CampaigndraftDetailcontentDataContract Read(Int64 campaigndraftDataId)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = CampaigndraftDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(campaigndraftDataId)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftDetailcontentDataContract> results = rawData.ToObject<IList<CampaigndraftDetailcontentDataContract>>()!;

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

    }
}
