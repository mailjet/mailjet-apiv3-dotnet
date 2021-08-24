using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client.Exceptions;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mailjet.Client.TransactionalEmails
{
    public static class MailjetClientExtensions
    {
        private static readonly JsonSerializer Serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new Newtonsoft.Json.Converters.StringEnumConverter()
            }
        });

        /// <summary>
        /// Sends a single transactional email using send API v3.1
        /// </summary>
        public static Task<TransactionalEmailResponse> SendTransactionalEmailAsync(
            this IMailjetClient mailjetClient,
            TransactionalEmail transactionalEmail, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            return mailjetClient.SendTransactionalEmailsAsync(new[] {transactionalEmail}, isSandboxMode, advanceErrorHandling);
        }

        /// <summary>
        /// Sends transactional emails using send API v3.1
        /// </summary>
        public static async Task<TransactionalEmailResponse> SendTransactionalEmailsAsync(this IMailjetClient mailjetClient,
            IEnumerable<TransactionalEmail> transactionalEmails, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            if (transactionalEmails.Count() > SendV31.MaxEmailsPerBatch || !transactionalEmails.Any())
                throw new MailjetClientConfigurationException($"Send Emails API v3.1 allows to send not more than {SendV31.MaxEmailsPerBatch} emails per call");

            var request = new SendEmailRequest
            {
                Messages = transactionalEmails.ToList(),
                SandboxMode = isSandboxMode,
                AdvanceErrorHandling = advanceErrorHandling
            };

            var clientRequest = new MailjetRequest
            {
                Resource = SendV31.Resource,
                Body = JObject.FromObject(request, Serializer)
            };

            var clientResponse = await mailjetClient.PostAsync(clientRequest)
                .ConfigureAwait(false);

            var result = clientResponse.Content.ToObject<TransactionalEmailResponse>();

            return result;
        }
    }
}
