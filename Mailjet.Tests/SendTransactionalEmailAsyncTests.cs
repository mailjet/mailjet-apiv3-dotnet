using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mailjet.Tests;

[TestClass]
public class SendTransactionalEmailAsyncTests
{
    private MockHttpMessageHandler _handler = null!;
    private MailjetClient _client = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _handler = new MockHttpMessageHandler();
        _client = new MailjetClient("api-key", "api-secret-key", _handler);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_ReturnsParsedResponse()
    {
        string jsonResponse = File.ReadAllText(@"MockResponses/SendEmailV31Response.json");
        _handler.When("https://api.mailjet.com/v3.1/*").Respond("application/json", jsonResponse);

        var response = await _client.SendTransactionalEmailAsync(new TransactionalEmail() { DeduplicateCampaign = true });

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("CustomValue", message.CustomID);
        Assert.AreEqual("success", message.Status);
        var error = message.Errors!.Single();
        Assert.AreEqual("1ab23cd4-e567-8901-2345-6789f0gh1i2j", error.ErrorIdentifier);
        Assert.AreEqual("send-0010", error.ErrorCode);
        Assert.AreEqual(400, error.StatusCode);
        Assert.AreEqual("Template ID \"123456789\" doesn't exist for your account.", error.ErrorMessage);
        Assert.AreEqual("TemplateID", error.ErrorRelatedTo!.Single());
        var to = message.To!.Single();
        Assert.AreEqual("passenger@mailjet.com", to.Email);
        Assert.IsNotNull(message.Cc);
        Assert.IsNotNull(message.Bcc);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_PassesCorrectRequestToMailjetServer()
    {
        string expectedRequest = File.ReadAllText(@"MockResponses/SendEmailV31Request.json");
        var expectedJObject = JsonNode.Parse(expectedRequest)?.AsObject();

        _handler.Expect(HttpMethod.Post, "https://api.mailjet.com/v3.1/send")
            .WithHeaders("Accept", "application/json")
            .WithHeaders("Authorization", "Basic YXBpLWtleTphcGktc2VjcmV0LWtleQ==")
            .With(message => {
                var content = message.Content!.ReadAsStringAsync().Result;
                var actualJObject = JsonNode.Parse(content)?.AsObject();
                
                // Compare key properties instead of full JSON string
                return CompareJsonObjects(expectedJObject, actualJObject);
            })
            .Respond("application/json", "{}");

        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("pilot@mailjet.com", "Your Mailjet Pilot"))
            .WithHtmlPart("<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!")
            .WithSubject("Your email flight plan!")
            .WithTextPart("Dear passenger, welcome to Mailjet! May the delivery force be with you!")
            .WithTo(new SendContact("passenger@mailjet.com", "Passenger 1"))
            .Build();

        await _client.SendTransactionalEmailAsync(email, true);
        _handler.VerifyNoOutstandingExpectation();
    }

    private static bool CompareJsonObjects(JsonObject? expected, JsonObject? actual)
    {
        if (expected == null || actual == null) return false;

        // Compare SandboxMode
        if (expected["SandboxMode"]?.GetValue<bool>() != actual["SandboxMode"]?.GetValue<bool>())
            return false;

        // Compare AdvanceErrorHandling
        if (expected["AdvanceErrorHandling"]?.GetValue<bool>() != actual["AdvanceErrorHandling"]?.GetValue<bool>())
            return false;

        // Compare Messages
        var expectedMessages = expected["Messages"]?.AsArray();
        var actualMessages = actual["Messages"]?.AsArray();
        
        if (expectedMessages?.Count != actualMessages?.Count)
            return false;

        var expectedMsg = expectedMessages?[0]?.AsObject();
        var actualMsg = actualMessages?[0]?.AsObject();

        if (expectedMsg == null || actualMsg == null) return false;

        // Compare From
        var expectedFrom = expectedMsg["From"]?.AsObject();
        var actualFrom = actualMsg["From"]?.AsObject();
        if (expectedFrom?["Email"]?.GetValue<string>() != actualFrom?["Email"]?.GetValue<string>())
            return false;
        if (expectedFrom?["Name"]?.GetValue<string>() != actualFrom?["Name"]?.GetValue<string>())
            return false;

        // Compare essential fields
        if (expectedMsg["HTMLPart"]?.GetValue<string>() != actualMsg["HTMLPart"]?.GetValue<string>())
            return false;
        if (expectedMsg["Subject"]?.GetValue<string>() != actualMsg["Subject"]?.GetValue<string>())
            return false;
        if (expectedMsg["TextPart"]?.GetValue<string>() != actualMsg["TextPart"]?.GetValue<string>())
            return false;

        // Compare To
        var expectedTo = expectedMsg["To"]?.AsArray()?[0]?.AsObject();
        var actualTo = actualMsg["To"]?.AsArray()?[0]?.AsObject();
        if (expectedTo?["Email"]?.GetValue<string>() != actualTo?["Email"]?.GetValue<string>())
            return false;
        if (expectedTo?["Name"]?.GetValue<string>() != actualTo?["Name"]?.GetValue<string>())
            return false;

        return true;
    }
}
