using System.Text;
using System.Text.Json.Nodes;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailjet.Tests.Integration;

[TestClass]
public class SendTransactionalEmailIntegrationTests
{
    private IMailjetClient _client = null!;
    private string _senderEmail = null!;

    [TestInitialize]
    public async Task TestInitialize()
    {
        _client = new MailjetClient(
            Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC") ?? throw new InvalidOperationException(),
            Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE") ?? throw new InvalidOperationException());
        _senderEmail = await GetValidSenderEmail(_client);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_SendsEmail()
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_senderEmail))
            .WithSubject("Test subject")
            .WithHtmlPart("<h1>Header</h1>")
            .WithTo(new SendContact(_senderEmail))
            .Build();

        var response = await _client.SendTransactionalEmailAsync(email);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("success", message.Status);
        Assert.AreEqual(_senderEmail, message.To?.Single().Email);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_WithCustomHeaders_SendsEmail()
    {
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

        var response = await _client.SendTransactionalEmailAsync(email);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("success", message.Status);
        Assert.AreEqual("customIdValue", message.CustomID);
        Assert.AreEqual(_senderEmail, message.To?.Single().Email);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_TemplateIsMissing_ReturnsError()
    {
        long nonExistentTemplateId = 12345;
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_senderEmail))
            .WithSubject("Test subject")
            .WithTemplateId(nonExistentTemplateId)
            .WithTrackOpens(TrackOpens.enabled)
            .WithTo(new SendContact(_senderEmail))
            .Build();

        var response = await _client.SendTransactionalEmailAsync(email);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("error", message.Status);
        Assert.AreEqual(1, message.Errors?.Count);

        var error = message.Errors!.Single();
        Assert.AreEqual(400, error.StatusCode);
        Assert.AreEqual("Template id \"12345\" doesn't exist for your account.", error.ErrorMessage);
        Assert.AreEqual("send-0010", error.ErrorCode);
        Assert.AreEqual("TemplateID", error.ErrorRelatedTo?.Single());
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_TemplateIdReturnsWrongSenderType_advanceErrorHandlingTrue()
    {
        var variables = new Dictionary<string, object> { { "actionLink", "https://anywhere.com" } };

        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_senderEmail))
            .WithTo(new SendContact(_senderEmail))
            .WithTemplateId(3120707)
            .WithVariables(variables)
            .Build();

        var response = await _client.SendTransactionalEmailAsync(email, advanceErrorHandling: true);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("error", message.Status);
        Assert.AreEqual(1, message.Errors?.Count);

        var error = message.Errors!.Single();
        Assert.AreEqual(400, error.StatusCode);
    }

    [TestMethod]
    public async Task SendTransactionalEmailAsync_TemplateIdReturnsWrongSenderType_advanceErrorHandlingFalse()
    {
        var variables = new Dictionary<string, object> { { "actionLink", "https://anywhere.com" } };

        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_senderEmail))
            .WithTo(new SendContact(_senderEmail))
            .WithTemplateId(3120707)
            .WithVariables(variables)
            .Build();

        var response = await _client.SendTransactionalEmailAsync(email, advanceErrorHandling: false);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("success", message.Status);
        Assert.IsNull(message.Errors);
    }

    public static async Task<string> GetValidSenderEmail(IMailjetClient client)
    {
        MailjetRequest request = new MailjetRequest { Resource = Sender.Resource };
        MailjetResponse response = await client.GetAsync(request);

        Assert.AreEqual(200, response.StatusCode);

        foreach (var emailObject in response.GetData())
        {
            if (emailObject is not JsonObject obj) continue;
            if (obj["Status"]?.GetValue<string>() == "Active")
                return obj["Email"]?.GetValue<string>() ?? throw new InvalidOperationException();
        }

        Assert.Fail("Cannot find Active sender address under given account");
        throw new AssertFailedException();
    }
}
