using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Mailjet.Tests
{
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
            MailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);

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
