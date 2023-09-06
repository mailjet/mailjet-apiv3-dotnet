using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Models.MailJet;
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
using Mailjet.Repositories.Models.DataContracts.Sender;

namespace Mailjet.Repositories
{
    public class SenderMailJetRepository : MailJetRepositoryBase, ISenderRepository
    {
        public SenderMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }

        public SenderMailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {

        }

        public IList<SenderDataContract> List(PagingRequestBaseDataContract query)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Sender.Resource,
                Filters = query.ToDictionary()
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<SenderDataContract> results = rawData.ToObject<IList<SenderDataContract>>()!;

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

        public SenderDataContract Read(Int64 key)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = Sender.Resource,
                ResourceId = ResourceId.Numeric(key)
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<SenderDataContract> results = rawData.ToObject<IList<SenderDataContract>>()!;

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
