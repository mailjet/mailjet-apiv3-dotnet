using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Mailjet.Tests.Integration
{
    [TestClass]
    public class BounceStatisticsIntegrationTests
    {
        private MailjetClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _client = new MailjetClient("7cc6e40ff4f14b82307deb03044aa6a4",
                "17b92c5ec33cf336859c0864dfb9bb42");
        }

        [TestMethod]
        public async Task GetBounceStatistics_ReturnsData()
        {
            // arrange
            MailjetRequest request = new MailjetRequest
            {
                Resource = Bouncestatistics.Resource,
            };
            MailjetResponse response = await _client.GetAsync(request);

            // act


            // assert


        }
    }
}
