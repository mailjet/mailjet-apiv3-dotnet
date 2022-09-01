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
using System.Reflection;
using Microsoft.VisualBasic;

namespace Mailjet.Repositories
{
    public class CampaigndraftScheduleMailJetRepository : MailJetRepositoryBase, ICampaigndraftScheduleRepository
    {
        public CampaigndraftScheduleMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }

        public CampaigndraftScheduleMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public CampaigndraftScheduleDataContract Create(long key, CampaigndraftScheduleDataContract model)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = CampaigndraftSchedule.Resource,
                ResourceId = ResourceId.Numeric(key),
                Body = (JObject)JToken.FromObject(model)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftScheduleDataContract> results = rawData.ToObject<IList<CampaigndraftScheduleDataContract>>()!;

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

        public CampaigndraftScheduleDataContract Read(long key)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = CampaigndraftSchedule.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftScheduleDataContract> results = rawData.ToObject<IList<CampaigndraftScheduleDataContract>>()!;

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

        public CampaigndraftScheduleDataContract Update(long key, DateTimeOffset update)
        {
            IMailjetClient client = this.GetMailjetClient();

            var request = new MailjetRequest()
            {
                Resource = CampaigndraftSchedule.Resource,
                ResourceId = ResourceId.Numeric(key)
            }.Property(CampaigndraftSchedule.Date, update);

            MailjetResponse response = client.PutAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftScheduleDataContract> results = rawData.ToObject<IList<CampaigndraftScheduleDataContract>>()!;

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

        public CampaigndraftScheduleDataContract Delete(long item)
        {
            IMailjetClient client = this.GetMailjetClient();

            var request = new MailjetRequest()
            {
                Resource = CampaigndraftSchedule.Resource,
                ResourceId = ResourceId.Numeric(item)
            };

            MailjetResponse response = client.DeleteAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<CampaigndraftScheduleDataContract> results = rawData.ToObject<IList<CampaigndraftScheduleDataContract>>()!;

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
