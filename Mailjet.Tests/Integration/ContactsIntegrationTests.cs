using System.Text.Json.Nodes;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailjet.Tests.Integration;

[TestClass]
public class ContactsIntegrationTests
{
    private static MailjetClient _client = null!;
    private static string _contactName = null!;
    private static string _contactEmail = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        _client = new MailjetClient(
            Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC") ?? throw new InvalidOperationException(),
            Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE") ?? throw new InvalidOperationException());
        _contactName = $"contact_name_{Guid.NewGuid()}";
        _contactEmail = $"{_contactName}@mailjet.com";
    }

    [TestMethod]
    public async Task ContactCRD_Test()
    {
        var createdContactId = await AssertCreateContact();
        await AssertGetContact(createdContactId);
        await AssertDeleteContact(createdContactId);
    }

    public async Task<long> AssertCreateContact()
    {
        var request = new MailjetRequest { Resource = Contact.Resource }
            .Property(Contact.IsExcludedFromCampaigns, "true")
            .Property(Contact.Name, _contactName)
            .Property(Contact.Email, _contactEmail);

        var response = await _client.PostAsync(request);

        Assert.AreEqual(201, response.StatusCode);
        Assert.AreEqual(1, response.GetCount());
        Assert.AreEqual(1, response.GetTotal());

        var firstObject = response.GetData()[0];
        Assert.AreEqual(true, firstObject?["IsExcludedFromCampaigns"]?.GetValue<bool>());
        Assert.AreEqual(_contactName, firstObject?["Name"]?.GetValue<string>());
        Assert.AreEqual(_contactEmail, firstObject?["Email"]?.GetValue<string>());

        return firstObject?["ID"]?.GetValue<long>() ?? throw new InvalidOperationException();
    }

    private async Task AssertGetContact(long contactId)
    {
        MailjetRequest request = new MailjetRequest
        {
            Resource = Contact.Resource,
            ResourceId = ResourceId.Numeric(contactId)
        };

        MailjetResponse response = await _client.GetAsync(request);

        Assert.AreEqual(200, response.StatusCode);
        Assert.AreEqual(1, response.GetCount());
        Assert.AreEqual(1, response.GetTotal());

        var firstObject = response.GetData()[0];
        Assert.AreEqual(true, firstObject?["IsExcludedFromCampaigns"]?.GetValue<bool>());
        Assert.AreEqual(_contactName, firstObject?["Name"]?.GetValue<string>());
        Assert.AreEqual(_contactEmail, firstObject?["Email"]?.GetValue<string>());
        Assert.AreEqual(contactId, firstObject?["ID"]?.GetValue<long>());
    }

    private async Task AssertDeleteContact(long contactId)
    {
        MailjetRequest request = new MailjetRequest
        {
            Resource = Contacts.Resource,
            ResourceId = ResourceId.Numeric(contactId)
        };

        MailjetResponse response = await _client.DeleteAsync(request);
        Assert.AreEqual(200, response.StatusCode);
    }
}
