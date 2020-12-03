using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using sms = Mailjet.Client.Resources.SMS;

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
        private const string Status = "Status";
        private const string Code = "Code";
        private const string Name = "Name";
        private const string Description = "Description";

        private string API_TOKEN;

        [TestInitialize]
        public void TestInit()
        {
            API_TOKEN = "ApiToken";
        }

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
        public void TestTooManyRequestsStatus()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.mailjet.com/v3/*")
                   .Respond(((HttpStatusCode) 429));

            // Inject the handler into your application code
            IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Apikey.Resource,
            };

            MailjetResponse response = client.GetAsync(request).Result;

            Assert.AreEqual(429, response.StatusCode);
            Assert.AreEqual("Too many requests", response.GetErrorInfo());
        }


        [TestMethod]
        public void TestInternalServerErrorStatus()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.mailjet.com/v3/*")
                   .Respond(HttpStatusCode.InternalServerError);


            // Inject the handler into your application code
            IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Apikey.Resource,
            };

            MailjetResponse response = client.GetAsync(request).Result;

            Assert.AreEqual(500, response.StatusCode);
            Assert.AreEqual("Internal Server Error", response.GetErrorInfo());
        }

        [TestMethod]
        public void TestSmsCountAsync()
        {
            int expectedTotal = 1;
            int expectedCount = 1;
            var expectedData = new JArray();

            var mockHttp = new MockHttpMessageHandler();

            var jsonResponse = GenerateJsonResponse(expectedTotal, expectedCount, expectedData);

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://api.mailjet.com/v4/*")
                    .Respond(JsonMediaType, jsonResponse); // Respond with JSON

            IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.Count.Resource
            }
            .Filter(sms.Count.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
            .Filter(sms.Count.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());


            MailjetResponse response = client.GetAsync(request).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(expectedTotal, response.GetTotal());
            Assert.AreEqual(expectedCount, response.GetCount());
            Assert.IsTrue(JToken.DeepEquals(expectedData, response.GetData()));
        }

        [TestMethod]
        public void TestSmsExportAsync()
        {
            int expectedCode = 1;
            string expectedName = "PENDING";
            string expectedDescription = "The request is accepted.";

            var status = new JObject
            {
                { Code, expectedCode},
                { Name, expectedName},
                { Description, expectedDescription}
            };

            var smsExportResponse = new JObject
            {
                { Status, status }
            };

            var mockHttp = new MockHttpMessageHandler();
            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://api.mailjet.com/v4/*")
                    .Respond(JsonMediaType, GenerateJsonResponse(smsExportResponse)); // Respond with JSON

            // timsestamp range offset
            int offset = 1000;

            IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.Export.Resource
            }
            .Property(sms.Export.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            .Property(sms.Export.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + offset);


            MailjetResponse response = client.PostAsync(request).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(expectedCode, response.GetData()[0][Status].Value<int>(Code));
            Assert.AreEqual(expectedName, response.GetData()[0][Status].Value<string>(Name));
            Assert.AreEqual(expectedDescription, response.GetData()[0][Status].Value<string>(Description));
        }

        [TestMethod]
        public void TestSmsStatisticsAsync()
        {
            var expectedData = new JArray();
            var mockHttp = new MockHttpMessageHandler();
            var jsonResponse = GenerateJsonResponse(1, 1, expectedData);

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://api.mailjet.com/v4/*")
                    .Respond(JsonMediaType, jsonResponse); // Respond with JSON

            IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);

            MailjetRequest request = new MailjetRequest
            {
                Resource = sms.SMS.Resource
            }
            .Filter(sms.SMS.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
            .Filter(sms.SMS.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());

            MailjetResponse response = client.GetAsync(request).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
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

            return GenerateJsonResponse(jObject);
        }

        private string GenerateJsonResponse(JObject jObject)
        {
            return jObject.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
