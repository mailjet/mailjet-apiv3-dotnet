using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Mailjet.IntegrationTests
{
    internal static class IntegrationTestHelper
    {
        public static async Task<string> GetValidSenderEmail(IMailjetClient client)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Sender.Resource
            };

            MailjetResponse response = await client.GetAsync(request);

            Assert.AreEqual(200, response.StatusCode);

            foreach (var emailObject in response.GetData())
            {
                if (emailObject.Type != JTokenType.Object)
                    continue;

                if (emailObject.Value<string>("Status") == "Active")
                    return emailObject.Value<string>("Email");
            }

            Assert.Fail("Cannot find Active sender address under given account");
            throw new AssertFailedException();
        }
    }
}
