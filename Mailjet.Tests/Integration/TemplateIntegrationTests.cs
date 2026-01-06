using System.Text.Json;
using System.Text.Json.Nodes;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailjet.Tests.Integration;

[TestClass]
public class TemplateIntegrationTests
{
    private MailjetClient _client = null!;
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
        long templateId = await CreateTemplate();
        Assert.IsTrue(templateId > 0);
        await FillTemplateContent(templateId);
        await SendEmailWithTemplate(templateId);
        await DeleteTemplate(templateId);
    }

    private async Task DeleteTemplate(long templateId)
    {
        MailjetRequest request = new MailjetRequest
        {
            Resource = Template.Resource,
            ResourceId = ResourceId.Numeric(templateId)
        };
        MailjetResponse response = await _client.DeleteAsync(request);
        Assert.AreEqual(204, response.StatusCode);
    }

    private async Task FillTemplateContent(long templateId)
    {
        var content = File.ReadAllText(Path.Combine("Resources", "MJMLTemplate.mjml"));
        var headers = new Dictionary<string, string>
        {
            {"Subject", "Test transactional template subject " + DateTime.UtcNow},
            {"SenderName", "Test transactional template"},
            {"SenderEmail", _senderEmail},
            {"From", _senderEmail},
        };

        MailjetRequest request = new MailjetRequest 
        {
            Resource = TemplateDetailcontent.Resource,
            ResourceId = ResourceId.Numeric(templateId)
        }
        .Property(TemplateDetailcontent.MJMLContent, content)
        .Property(TemplateDetailcontent.Headers, JsonSerializer.SerializeToNode(headers));

        MailjetResponse response = await _client.PostAsync(request);

        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.AreEqual(1, response.GetTotal());
        Assert.AreEqual(content, response.GetData().Single()?["MJMLContent"]?.GetValue<string>());
    }

    private async Task<long> CreateTemplate()
    {
        var templateName = "C# integration test template " + DateTime.UtcNow;

        MailjetRequest request = new MailjetRequest { Resource = Template.Resource }
            .Property(Template.Author, "Mailjet team")
            .Property(Template.Copyright, "Mailjet")
            .Property(Template.Description, "Used to send templated emails in C# SDK integration test")
            .Property(Template.EditMode, Template.EditModeValue_MJMLBuilder)
            .Property(Template.IsTextPartGenerationEnabled, true)
            .Property(Template.Locale, "en_US")
            .Property(Template.Name, templateName)
            .Property(Template.OwnerType, Template.OwnerTypeValue_Apikey)
            .Property(Template.Purposes, JsonSerializer.SerializeToNode(new[] { "transactional" }));

        MailjetResponse response = await _client.PostAsync(request);

        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.AreEqual(1, response.GetTotal());
        Assert.AreEqual(templateName, response.GetData().Single()?["Name"]?.GetValue<string>());

        return response.GetData().Single()?["ID"]?.GetValue<long>() ?? throw new InvalidOperationException();
    }

    public async Task SendEmailWithTemplate(long templateId)
    {
        var testArrayWithValues = new[] { new { title = "testTitle1" }, new { title = "testTitle2" } };
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

        var response = await _client.SendTransactionalEmailAsync(email);

        Assert.AreEqual(1, response.Messages?.Length);
        var message = response.Messages![0];
        Assert.AreEqual("success", message.Status);
        Assert.AreEqual(_senderEmail, message.To?.Single().Email);
    }

    public static async Task<string> GetValidSenderEmail(MailjetClient client)
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
