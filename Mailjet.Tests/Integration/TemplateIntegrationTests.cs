using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Mailjet.Tests.Integration
{
    [TestClass]
    public class TemplateIntegrationTests
    {
        private MailjetClient _client;
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

            MailjetRequest request = new MailjetRequest
            {
                Resource = TemplateDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(2095787)
            };

            var resp = await _client.GetAsync(request);



            long templateId = await CreateTemplate();

            Assert.IsTrue(templateId > 0);

            await FillTemplateContent(templateId);

            await SendEmailWithTemplate(templateId);

            await DeleteTemplate(templateId);
        }

        private async Task DeleteTemplate(long templateId)
        {
            // arrange
            MailjetRequest request = new MailjetRequest
            {
                Resource = Template.Resource,
                ResourceId = ResourceId.Numeric(templateId)
            };

            // act
            MailjetResponse response = await _client.DeleteAsync(request);

            // assert
            Assert.AreEqual(204, response.StatusCode);
        }

        private async Task FillTemplateContent(long templateId)
        {
            // arrange
            var content = File.ReadAllText(@"Resources\MJMLTemplate.mjml");

            MailjetRequest request = new MailjetRequest 
            {
                Resource = TemplateDetailcontent.Resource,
                ResourceId = ResourceId.Numeric(templateId)
            }
            .Property(TemplateDetailcontent.MJMLContent, content)
            .Property(TemplateDetailcontent.Headers, JObject.FromObject(new Dictionary<string, string>()
            {
                {"Subject", "Test transactional template subject " + DateTime.UtcNow},
                {"SenderName", "Test transactional template"},
                {"SenderEmail", _senderEmail},
                {"From", _senderEmail},
            }));

            // act
            MailjetResponse response = await _client.PostAsync(request);

            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(1, response.GetTotal());
            Assert.AreEqual(content, response.GetData().Single().Value<string>("MJMLContent"));
        }

        private async Task<long> CreateTemplate()
        {
            // arrange
            var templateName = "C# integration test template " + DateTime.UtcNow;

            MailjetRequest request = new MailjetRequest
            {
                Resource = Template.Resource,
            }
            .Property(Template.Author, "Mailjet team")
            .Property(Template.Copyright, "Mailjet")
            .Property(Template.Description, "Used to send templated emails in C# SDK integration test")
            .Property(Template.EditMode, Template.EditModeValue_MJMLBuilder)
            .Property(Template.IsTextPartGenerationEnabled, true)
            .Property(Template.Locale, "en_US")
            .Property(Template.Name, templateName)
            .Property(Template.OwnerType, Template.OwnerTypeValue_Apikey)
            .Property(Template.Purposes, JArray.FromObject(new[]{ "transactional" }));

            // act
            MailjetResponse response = await _client.PostAsync(request);

            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Assert.AreEqual(1, response.GetTotal());
            Assert.AreEqual(templateName, response.GetData().Single().Value<string>("Name"));

            long templateId = response.GetData().Single().Value<long>("ID");
            return templateId;
        }

        public async Task SendEmailWithTemplate(long templateId)
        {
            // arrange
            var testArrayWithValues = new []
            {
                new {title = "testTitle1"},
                new {title = "testTitle2"},
            };

            var variables = new Dictionary<string, object>
            {
                {"testVariableName", "testVariableValue"},
                {"items", testArrayWithValues}
            };

            var email = new TransactionalEmailBuilder()
                .WithTo(new SendContact(_senderEmail))
                .WithFrom(new SendContact(_senderEmail))
                .WithSubject("Test subject " + DateTime.UtcNow)
                .WithTemplateId(templateId)
                .WithVariables(variables)
                .WithTemplateLanguage(true)
                .WithTemplateErrorDeliver(true)
                .WithTemplateErrorReporting(new SendContact(_senderEmail))
                .Build();

            // act
            var response = await _client.SendTransactionalEmailAsync(email);

            // assert
            Assert.AreEqual(1, response.Messages.Length);
            var message = response.Messages[0];

            Assert.AreEqual("success", message.Status);
            Assert.AreEqual(_senderEmail, message.To.Single().Email);
        }

        public static async Task<string> GetValidSenderEmail(MailjetClient client)
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
