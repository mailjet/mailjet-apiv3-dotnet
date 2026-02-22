using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json.Nodes;
using sms = Mailjet.Client.Resources.SMS;

namespace Mailjet.Tests;

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
    private string API_TOKEN = null!;

    [TestInitialize]
    public void TestInit() { API_TOKEN = "ApiToken"; }

    [TestMethod]
    public void TestGetAsync()
    {
        var expectedData = new JsonArray { new JsonObject { { Apikey.APIKey, "ApiKeyTest" } } };
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v3/*").Respond(JsonMediaType, GenerateJsonResponse(1, 1, expectedData));
        IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = Apikey.Resource };
        MailjetResponse response = client.GetAsync(request).Result;
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.AreEqual(1, response.GetTotal());
        Assert.AreEqual(1, response.GetCount());
    }

    [TestMethod]
    public void TestTooManyRequestsStatus()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v3/*").Respond((HttpStatusCode)429);
        IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = Apikey.Resource };
        MailjetResponse response = client.GetAsync(request).Result;
        Assert.AreEqual(429, response.StatusCode);
        Assert.AreEqual("Too many requests", response.GetErrorInfo());
    }

    [TestMethod]
    public void TestInternalServerErrorStatus()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v3/*").Respond(HttpStatusCode.InternalServerError);
        IMailjetClient client = new MailjetClient(ApiKeyTest, ApiSecretTest, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = Apikey.Resource };
        MailjetResponse response = client.GetAsync(request).Result;
        Assert.AreEqual(500, response.StatusCode);
        Assert.AreEqual("Internal Server Error", response.GetErrorInfo());
    }

    [TestMethod]
    public void TestSmsCountAsync()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v4/*").Respond(JsonMediaType, GenerateJsonResponse(1, 1, new JsonArray()));
        IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = sms.Count.Resource }
            .Filter(sms.Count.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
            .Filter(sms.Count.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
        MailjetResponse response = client.GetAsync(request).Result;
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public void TestSmsExportAsync()
    {
        var status = new JsonObject { { Code, 1 }, { Name, "PENDING" }, { Description, "The request is accepted." } };
        var smsExportResponse = new JsonObject { { Status, status } };
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v4/*").Respond(JsonMediaType, GenerateJsonResponse(smsExportResponse));
        IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = sms.Export.Resource }
            .Property(sms.Export.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            .Property(sms.Export.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 1000);
        MailjetResponse response = client.PostAsync(request).Result;
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public void TestSmsStatisticsAsync()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.mailjet.com/v4/*").Respond(JsonMediaType, GenerateJsonResponse(1, 1, new JsonArray()));
        IMailjetClient client = new MailjetClient(API_TOKEN, mockHttp);
        MailjetRequest request = new MailjetRequest { Resource = sms.SMS.Resource }
            .Filter(sms.SMS.FromTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
            .Filter(sms.SMS.ToTS, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
        MailjetResponse response = client.GetAsync(request).Result;
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    private static string GenerateJsonResponse(int total, int count, JsonArray data)
    {
        return new JsonObject { { TotalKey, total }, { CountKey, count }, { DataKey, data } }.ToJsonString();
    }

    private static string GenerateJsonResponse(JsonObject jObject) => jObject.ToJsonString();
}
