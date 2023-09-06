using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Exceptions;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Repositories.Models.MailJet.DataContracts;
using Mailjet.Repositories.Models.MailJet;
using Mailjet.Repositories.Interfaces.Configuration;
using Microsoft.Extensions.Options;

namespace Mailjet.Repositories.Repositories.Base
{
    public class MailJetRepositoryBase
    {
        protected IOptions<IMailJetConfiguration>? ConfigurationRepositoryOptions { get; }

        protected IMailJetConfiguration? ConfigurationRepository;

        public MailJetRepositoryBase(IMailJetConfiguration configurationRepository)
        {
            this.ConfigurationRepository = configurationRepository;
        }

        public MailJetRepositoryBase(IOptions<IMailJetConfiguration> sqlConnectionStringConfiguration)
        {
            this.ConfigurationRepositoryOptions = sqlConnectionStringConfiguration;
        }


        public IMailjetClient GetMailjetClient()
        {
            if (this.ConfigurationRepository != null)
            {
                return GetMailjetClient(this.ConfigurationRepository.MailJetApiPublicKey, this.ConfigurationRepository.MailJetApiPrivateKey);
            }
            else if(this.ConfigurationRepositoryOptions != null)
            {
                return GetMailjetClient(this.ConfigurationRepositoryOptions.Value.MailJetApiPublicKey, this.ConfigurationRepositoryOptions.Value.MailJetApiPrivateKey);
            }
            else
            {
                throw new Exception("MailJet Configuration Not Found");
            }
        }

        public IMailjetClient GetMailjetClient(string apiKey, string apiPrivateKey)
        {
            IMailjetClient client = new MailjetClient(apiKey, apiPrivateKey);

            return client;
        }

        public async Task<MailJetPingResponseModel> RunTestAsync()
        {
            if (this.ConfigurationRepository != null)
            {
                return await RunTestAsync(this.ConfigurationRepository.MailJetApiPublicKey, this.ConfigurationRepository.MailJetApiPrivateKey);
            }
            else if (this.ConfigurationRepositoryOptions != null)
            {
                return await RunTestAsync(this.ConfigurationRepositoryOptions.Value.MailJetApiPublicKey, this.ConfigurationRepositoryOptions.Value.MailJetApiPrivateKey);
            }
            else
            {
                throw new Exception("MailJet Configuration Not Found");
            }

             }

        public async Task<MailJetPingResponseModel> RunTestAsync(string apiKey, string apiPrivateKey)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Apikey.Resource,
            };

            IMailjetClient client = GetMailjetClient(apiKey, apiPrivateKey);

            MailjetResponse response = await client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var mailJetPingResponse = new MailJetPingResponseModel();

                mailJetPingResponse.Total = response.GetTotal();
                mailJetPingResponse.Count = response.GetCount();
                var x = response.GetData();
                var y = new List<MailJetPingModel>();
                mailJetPingResponse.Data = x.ToObject<MailJetPingModel[]>();
                //mailJetPingResponse.Data = 
                return mailJetPingResponse;
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
    }
}
