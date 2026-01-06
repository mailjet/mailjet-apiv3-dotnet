using Mailjet.Client;
using Mailjet.Client.Helpers;
using Mailjet.Client.TransactionalEmails.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text.Json.Nodes;

namespace Mailjet.Tests;

[TestClass]
public class HttpContentHelperTests
{
    [TestMethod]
    public async Task GetContentAsync_WhenContentIsNull_ReturnsStatusCode()
    {
        var result = await HttpContentHelper.GetContentAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
        Assert.AreEqual((int)HttpStatusCode.OK, result["StatusCode"]?.GetValue<int>());
    }

    [TestMethod]
    public async Task GetContentAsync_WhenContentNotNull_ParsesMessagesCorrectly()
    {
        var response = GetHttpResponse(HttpStatusCode.BadRequest, "{\"Messages\":[{\"Status\":\"error\"}]}");
        var result = await HttpContentHelper.GetContentAsync(response);
        var messages = result[nameof(TransactionalEmailResponse.Messages)]?.AsArray();
        Assert.IsNotNull(messages);
        Assert.IsTrue(messages.Count > 0);
    }

    [TestMethod]
    public async Task GetContentAsync_WhenContentIsGenericError_ReturnsErrorInfo()
    {
        var response = GetHttpResponse(HttpStatusCode.Unauthorized, "{\"ErrorIdentifier\":\"test\",\"StatusCode\":401}");
        var result = await HttpContentHelper.GetContentAsync(response);
        Assert.IsNotNull(result[MailjetConstants.ErrorInfo]?.GetValue<string>());
    }

    [TestMethod]
    public async Task GetContentAsync_WhenContentIsTooManyRequests_ReturnsCorrectErrorInfo()
    {
        var response = GetHttpResponse((HttpStatusCode)429, "{\"StatusCode\":429}");
        var result = await HttpContentHelper.GetContentAsync(response);
        Assert.AreEqual(MailjetConstants.TooManyRequestsMessage, result[MailjetConstants.ErrorInfo]?.GetValue<string>());
    }

    [TestMethod]
    public async Task GetContentAsync_WhenContentIsInternalServerError_ReturnsCorrectErrorInfo()
    {
        var response = GetHttpResponse(HttpStatusCode.InternalServerError, "{\"StatusCode\":500}");
        var result = await HttpContentHelper.GetContentAsync(response);
        Assert.AreEqual(MailjetConstants.InternalServerErrorGeneralMessage, result[MailjetConstants.ErrorInfo]?.GetValue<string>());
    }

    private static HttpResponseMessage GetHttpResponse(HttpStatusCode statusCode, string content)
    {
        var response = new HttpResponseMessage { StatusCode = statusCode, Content = new StringContent(content) };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return response;
    }
}
