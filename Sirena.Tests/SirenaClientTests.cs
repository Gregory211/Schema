using System.Security.Cryptography;
using NUnit.Framework;

namespace Sirena.Tests
{
    [TestFixture]
    public class SirenaClientTests
    {
        [Test]
        public void ConnectTest()
        {
            var clientSettings = new SirenaClientSettings()
            {
                UserId = 922,
                Host = "193.104.87.251",
                Port = 34323,
                PublicRsaKey = new RSAParameters()

            };

            SirenaClient client = new SirenaClient(clientSettings);

            client.Connect();

            Assert.IsTrue(client.IsConnected);
        }
    }
}
