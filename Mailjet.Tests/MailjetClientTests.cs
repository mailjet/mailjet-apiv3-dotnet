namespace Mailjet.Tests
{
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using RichardSzalay.MockHttp;
    using sms = Mailjet.Client.Resources.SMS;

    [TestClass]
    public class MailjetClientTests
    {
        private const string JsonMediaType = "application/json";
        private const string ApiKeyTest = "apikeytest";
        private const string ApiSecretTest = "apisecrettest";
        private const string TotalKey = "Total";
        private const string CountKey = "Count";
        private const string DataKey = "Data";

        [TestMethod]
        public void TestGetAsync()
        {
            int expectedTotal = 1;
            int expectedCount = 1;

            var expectedData = new JArray
            {
                new JObject
                {
                    { Apikey.APIKey, "ApiKeyTest" },
                },
            };

            var mockHttp = new MockHttpMessageHandler();

            string jsonResponse = GenerateJsonResponse(expectedTotal, expectedCount, expectedData);

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://api.mailjet.com/v3/*")
                    .Respond(JsonMediaType, jsonResponse); // Respond with JSON

            // Inject the handler into your application code
            IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Apikey.Resource,
            };

            MailjetResponse response = client.GetAsync(request).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(expectedTotal, response.GetTotal());
            Assert.AreEqual(expectedCount, response.GetCount());
            Assert.IsTrue(JToken.DeepEquals(expectedData, response.GetData()));
        }

        [TestMethod]
        public void TestSmsSendAsync()
        {
            IMailjetClient client = new MailjetClient("e1a937c6a01f4b988f41d0228ca3d8eb")
            {
                Version = ApiVersion.V4
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.Send.Resource
            }
                .Property(sms.Send.From, "MJPilot")
                .Property(sms.Send.To, "+33000000")
                .Property(sms.Send.Text, "Demo");

            MailjetResponse response = client.PostAsync(request).Result;

        }

        [TestMethod]
        public void TestSmsCountAsync()
        {
            IMailjetClient client = new MailjetClient("e1a937c6a01f4b988f41d0228ca3d8eb")
            {
                Version = ApiVersion.V4
            };



            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.Count.Resource
            }
            .Filter(sms.Count.FromTS, "1033552800")
            .Filter(sms.Count.ToTS, "1033574400");

            MailjetResponse result = client.GetAsync(request).Result;

            Assert.IsTrue(result.IsSuccessStatusCode);

        }

        [TestMethod]
        public void TestSmsExportAsync()
        {
            IMailjetClient client = new MailjetClient("e1a937c6a01f4b988f41d0228ca3d8eb")
            {
                Version = ApiVersion.V4
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.Export.Resource
            }
            .Property(sms.Export.FromTS, 1527084481)
            .Property(sms.Export.ToTS, 1527085481);


            MailjetResponse response = client.PostAsync(request).Result;


            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void TestSmsAsync()
        {
            IMailjetClient client = new MailjetClient("e1a937c6a01f4b988f41d0228ca3d8eb")
            {
                Version = ApiVersion.V4
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.SMS.Resource
            }
            .Filter(sms.SMS.FromTS, 1527084481)
            .Filter(sms.SMS.ToTS, 1527085481);

            MailjetResponse result = client.GetAsync(request).Result;

            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        private string GenerateJsonResponse(int total, int count, JArray data)
        {
            var jObject = new JObject()
            {
                { TotalKey, total },
                { CountKey, count },
                { DataKey, data },
            };

            return jObject.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
