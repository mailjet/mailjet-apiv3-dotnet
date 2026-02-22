using Mailjet.Client;
using Mailjet.Client.Helpers;
using Mailjet.Client.TransactionalEmails.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mailjet.Tests
{
    [TestClass]
    public class HttpContentHelperTests
    {
        [TestMethod]
        public async Task GetContentAsync_WhenContentIsNull_ReturnsStatusCode()
        {
            // arrange
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            // act
            var result = await HttpContentHelper.GetContentAsync(new HttpResponseMessage { StatusCode = expectedStatusCode });
            HttpStatusCode statusCode = (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), result["StatusCode"].GetValue<int>().ToString());

            // assert
            Assert.AreEqual(expectedStatusCode, statusCode);
        }

        [TestMethod]
        public async Task GetContentAsync_WhenContentNotNull_ParsesMessagesCorrectly()
        {
            // arrange
            HttpResponseMessage response = 
                GetHttpResponse(HttpStatusCode.BadRequest, "{\"Messages\":[{\"Status\":\"error\",\"Errors\":[{\"ErrorIdentifier\":\"6c1d35cb-3de8-495c-9d0e-0d563d2671da\",\"ErrorCode\":\"mj-0007\",\"StatusCode\":400,\"ErrorMessage\":\"You must provide at least one item in the collection.\",\"ErrorRelatedTo\":[\"To\"]}]}]}");

            // act
            var result = await HttpContentHelper.GetContentAsync(response);

            // assert
            Assert.IsTrue(result[nameof(TransactionalEmailResponse.Messages)].AsArray().Any());
        }

        [TestMethod]
        public async Task GetContentAsync_WhenContentIsGenericError_ReturnsErrorInfo()
        {
            // arrange
            HttpResponseMessage response = 
                GetHttpResponse(HttpStatusCode.Unauthorized, "{\"ErrorIdentifier\":\"e3f64f47-c99a-4a5a-b054-3ce1c5c7e4ce\",\"StatusCode\":401,\"ErrorMessage\":\"API key authentication/authorization failure. You may be unauthorized to access the API or your API key may be expired. Visit API keys management section to check your keys.\"}");

            // act
            var result = await HttpContentHelper.GetContentAsync(response);
            string erroInfo = result[MailjetConstants.ErrorInfo].GetValue<string>();

            // assert
            Assert.IsNotNull(erroInfo);
        }

        [TestMethod]
        public async Task GetContentAsync_WhenContentIsTooManyRequests_ReturnsCorrectErrorInfo()
        {
            // arrange
            HttpResponseMessage response = GetHttpResponse((HttpStatusCode)429, "{\"ErrorIdentifier\":\"e3f64f47-c99a-4a5a-b054-3ce1c5c7e4ce\",\"StatusCode\":429,\"ErrorMessage\":\"Error!\"}");

            // act
            var result = await HttpContentHelper.GetContentAsync(response);
            string erroInfo = result[MailjetConstants.ErrorInfo].GetValue<string>();

            // assert
            Assert.AreEqual(MailjetConstants.TooManyRequestsMessage, erroInfo);
        }

        [TestMethod]
        public async Task GetContentAsync_WhenContentIsInternalServerError_ReturnsCorrectErrorInfo()
        {
            // arrange
            HttpResponseMessage response = GetHttpResponse(HttpStatusCode.InternalServerError, "{\"ErrorIdentifier\":\"e3f64f47-c99a-4a5a-b054-3ce1c5c7e4ce\",\"StatusCode\":500,\"ErrorMessage\":\"Error!\"}");

            // act
            var result = await HttpContentHelper.GetContentAsync(response);
            string erroInfo = result[MailjetConstants.ErrorInfo].GetValue<string>();

            // assert
            Assert.AreEqual(MailjetConstants.InternalServerErrorGeneralMessage, erroInfo);
        }

        private static HttpResponseMessage GetHttpResponse(HttpStatusCode expectedStatusCode, string contentString)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = expectedStatusCode,
                Content = new StringContent(contentString)
            };

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}
