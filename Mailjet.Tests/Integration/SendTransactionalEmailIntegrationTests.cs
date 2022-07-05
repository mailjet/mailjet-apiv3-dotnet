using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Mailjet.Tests.Integration
{
    [TestClass]
    public class SendTransactionalEmailIntegrationTests
    {
        private IMailjetClient _client;
        private string _senderEmail;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"),
                Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));

            _senderEmail = await GetValidSenderEmail(_client);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_SendsEmail()
        {
            // arrange
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_senderEmail))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Header</h1>")
                .WithTo(new SendContact(_senderEmail))
                .Build();

            // act
            var response = await _client.SendTransactionalEmailAsync(email);

            // assert
            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("success", message.Status);
            Assert.AreEqual(_senderEmail, message.To.Single().Email);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_WithCustomHeaders_SendsEmail()
        {
            // arrange
            var base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes("Test file content"));
            var attachment = new Attachment("test1.txt", "text/plain", base64Content);

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_senderEmail))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Header</h1>")
                .WithHeader("header1", "value1")
                .WithHeader("header2", "value2")
                .WithAttachment(attachment)
                .WithCustomId("customIdValue")
                .WithTo(new SendContact(_senderEmail))
                .Build();

            // act
            var response = await _client.SendTransactionalEmailAsync(email);

            // assert
            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("success", message.Status);
            Assert.AreEqual("customIdValue", message.CustomID);
            Assert.AreEqual(_senderEmail, message.To.Single().Email);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_TemplateIsMissing_ReturnsError()
        {
            // arrange
            long nonExistentTemplateId = 12345;
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_senderEmail))
                .WithSubject("Test subject")
                .WithTemplateId(nonExistentTemplateId)
                .WithTrackOpens(TrackOpens.enabled)
                .WithTo(new SendContact(_senderEmail))
                .Build();

            // act
            var response = await _client.SendTransactionalEmailAsync(email);

            // assert
            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("error", message.Status);
            Assert.AreEqual(1, message.Errors.Count);

            var error = message.Errors.Single();
            Assert.AreEqual(400, error.StatusCode);
            Assert.AreEqual("Template id \"12345\" doesn't exist for your account.", error.ErrorMessage);
            Assert.AreEqual("send-0010", error.ErrorCode);
            Assert.AreEqual("TemplateID", error.ErrorRelatedTo.Single());
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_TemplateIdReturnsWrongSenderType_advanceErrorHandlingTrue()
        {
            System.Collections.Generic.Dictionary<string, object> variables = new System.Collections.Generic.Dictionary<string, object>() { { "actionLink", "https://anywhere.com" } };

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_senderEmail))
                .WithTo(new SendContact(_senderEmail))
                .WithTemplateId(3120707)
                .WithVariables(variables)
                .Build();


            // invoke API to send email
            var response = await _client.SendTransactionalEmailAsync(email, advanceErrorHandling: true);

            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("error", message.Status);
            Assert.AreEqual(1, message.Errors.Count);

            var error = message.Errors.Single();
            Assert.AreEqual(400, error.StatusCode);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_TemplateIdReturnsWrongSenderType_advanceErrorHandlingFalse()
        {
            System.Collections.Generic.Dictionary<string, object> variables = new System.Collections.Generic.Dictionary<string, object>() { { "actionLink", "https://anywhere.com" } };

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_senderEmail))
                .WithTo(new SendContact(_senderEmail))
                .WithTemplateId(3120707)
                .WithVariables(variables)
                .Build();


            // invoke API to send email
            var response = await _client.SendTransactionalEmailAsync(email, advanceErrorHandling: false);

            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("success", message.Status);
            Assert.IsNull(message.Errors);
        }

        public static async Task<string> GetValidSenderEmail(IMailjetClient client)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Sender.Resource
            };

            MailjetResponse response = await client.GetAsync(request);

            Assert.AreEqual(200, response.StatusCode);

            foreach (var emailObject in response.GetData())
            {
                if (emailObject.Type != JTokenType.Object)
                    continue;

                if (emailObject.Value<string>("Status") == "Active")
                    return emailObject.Value<string>("Email");
            }

            Assert.Fail("Cannot find Active sender address under given account");
            throw new AssertFailedException();
        }
    }
}
