using Mailjet.Repositories.Interfaces.Configuration;
using Mailjet.Repositories.Interfaces;
using Mailjet.Repositories.Repositories.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mailjet.Repositories.SendV31MailJetRepository;
using Mailjet.Repositories.Models.DataContracts.SendV31;
using Mailjet.Client.Resources;
using Mailjet.Client;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Models.DataContracts.Campaign;
using Mailjet.Repositories.Models.MailJet;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Mailjet.Client.TransactionalEmails.Response;

namespace Mailjet.Repositories
{
    public class SendV31MailJetRepository : MailJetRepositoryBase, ISendV31Repository
    {
        public SendV31MailJetRepository(IOptions<IMailJetConfiguration> sqlConnectionStringConfiguration)
            : base(sqlConnectionStringConfiguration)
        {
        }

        public SendV31MailJetRepository(IMailJetConfiguration configurationRepository)
            : base(configurationRepository)
        {
        }

        public IList<MessageResult> Send(SendV31RequestDataContract sendV31Request)
        {
            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = SendV31.Resource,
                Body = sendV31Request.ToJObject()
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<MessageResult> results = rawData.ToObject<IList<MessageResult>>()!;

                return results;
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
