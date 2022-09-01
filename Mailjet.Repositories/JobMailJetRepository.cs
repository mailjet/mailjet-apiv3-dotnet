using Mailjet.Client.Resources;
using Mailjet.Client;
using Mailjet.Repositories.Exceptions;
using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.MailJet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Repositories.Base;
using Mailjet.Repositories.Interfaces.Configuration;
using Newtonsoft.Json.Linq;
using Mailjet.Repositories.Models.DataContracts.Job;
using System.Collections;
using Mailjet.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace Mailjet.Repositories
{
    public class JobMailJetRepository : MailJetRepositoryBase, IJobRepository
    {
        public JobMailJetRepository(IOptions<IMailJetConfiguration> configurationRepositoryOptions)
           : base(configurationRepositoryOptions)
        {

        }
        public JobMailJetRepository(IMailJetConfiguration configurationRepository) : base(configurationRepository)
        {

        }

        public JobDataContract CreateMergeContactsIntoListJob<T>(long contactslistId, IList<ContactBasicDataContract<T>> contacts)
            where T : class, new()
        {
            IMailjetClient client = this.GetMailjetClient();

            ContactslistManagemanycontactsDataContract contactslist = new()
            {
                Action = "addnoforce",
                Contacts = (IList)contacts
            };

            MailjetRequest request = new()
            {
                Resource = ContactslistManagemanycontacts.Resource,
                Body = (JObject)JToken.FromObject(contactslist),
                ResourceId = ResourceId.Numeric(contactslistId)
            };

            MailjetResponse response = client.PostAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<JobIdDataContract> results = rawData.ToObject<IList<JobIdDataContract>>()!;

                JobIdDataContract jobId = results.Single();

                JobDataContract job = ReadContactListJobStatus(jobId.JobID, contactslistId);

                return job;
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

        public JobDataContract ReadContactListJobStatus(long jobId, long contactslistId)
        {

            IMailjetClient client = this.GetMailjetClient();

            MailjetRequest request = new()
            {
                Resource = ContactslistManagemanycontacts.Resource,
                ResourceId = ResourceId.Numeric(contactslistId),
                ActionId = jobId
            };

            MailjetResponse response = client.GetAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var rawData = response.GetData();

                IList<JobDataContract> results = rawData.ToObject<IList<JobDataContract>>();

                JobDataContract result = results.Single();

                result.JobID = jobId;

                return result;
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
