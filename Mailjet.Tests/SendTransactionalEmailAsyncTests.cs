using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;

namespace Mailjet.Tests
{
    [TestClass]
    public class SendTransactionalEmailAsyncTests
    {
        private MockHttpMessageHandler _handler;
        private MailjetClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _handler = new MockHttpMessageHandler();

            _client = new MailjetClient("api-key", "api-secret-key", _handler);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_ReturnsParsedResponse()
        {
            // arrange
            string jsonResponse = File.ReadAllText(@"MockResponses/SendEmailV31Response.json");
            _handler.When("https://api.mailjet.com/v3.1/*")
                .Respond("application/json", jsonResponse);

            // act
            var response = await _client.SendTransactionalEmailAsync(new TransactionalEmail() {DeduplicateCampaign = true});

            // assert
            Assert.AreEqual(1, response.Messages.Length);

            var message = response.Messages[0];
            Assert.AreEqual("CustomValue", message.CustomID);
            Assert.AreEqual("success", message.Status);

            // errors
            var error = message.Errors.Single();
            Assert.AreEqual("1ab23cd4-e567-8901-2345-6789f0gh1i2j", error.ErrorIdentifier);
            Assert.AreEqual("send-0010", error.ErrorCode);
            Assert.AreEqual(400, error.StatusCode);
            Assert.AreEqual("Template ID \"123456789\" doesn't exist for your account.", error.ErrorMessage);
            Assert.AreEqual("TemplateID", error.ErrorRelatedTo.Single());

            // to
            var to = message.To.Single();
            Assert.AreEqual("passenger@mailjet.com", to.Email);
            Assert.AreEqual("1ab23cd4-e567-8901-2345-6789f0gh1i2j", to.MessageUUID);
            Assert.AreEqual(1234567890987654400, to.MessageID);
            Assert.AreEqual("https://api.mailjet.com/v3/message/1234567890987654321", to.MessageHref);

            // cc & bcc
            Assert.IsNotNull(message.Cc);
            Assert.IsNotNull(message.Bcc);
        }

        [TestMethod]
        public async Task SendTransactionalEmailAsync_PassesCorrectRequestToMailjetServer()
        {
            // arrange
            string expectedRequest = File.ReadAllText(@"MockResponses/SendEmailV31Request.json");
            var expectedJObject = JObject.Parse(expectedRequest);

            _handler
                .Expect(HttpMethod.Post, "https://api.mailjet.com/v3.1/send")
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "Basic YXBpLWtleTphcGktc2VjcmV0LWtleQ==")
                .With(message => 
                {
                    var content = message.Content.ReadAsStringAsync().Result;

                    var actualJObject = JObject.Parse(content);
                    return JToken.DeepEquals(actualJObject, expectedJObject);
                })
                .Respond("application/json", "{}");

            var email = new TransactionalEmailBuilder()
                .WithFrom(new Contact("pilot@mailjet.com", "Your Mailjet Pilot"))
                .WithHtmlPart("<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!")
                .WithSubject("Your email flight plan!")
                .WithTextPart("Dear passenger, welcome to Mailjet! May the delivery force be with you!")
                .WithTo(new Contact("passenger@mailjet.com", "Passenger 1"))
                .Build();

            // act
            var response = await _client.SendTransactionalEmailAsync(email, true);

            // assert
            _handler.VerifyNoOutstandingExpectation();
        }
    }
}
