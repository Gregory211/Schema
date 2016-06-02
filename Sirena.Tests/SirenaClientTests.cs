using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sirena.Tests
{
    [TestFixture]
    public class SirenaClientTests
    {
        private SirenaClient _client;

        [TestFixtureSetUp]
        public void Init()
        {
            var clientSettings = new SirenaClientSettings()
            {
                UserId = 922,
                Host = "193.106.94.28",
                Port = 8888,
                PublicRsaKey = new RSAParameters()

            };

            _client = new SirenaClient(clientSettings);

            _client.Connect();
        }

        [Test]
        public async Task ConnectTest()
        {                     
            Assert.IsTrue(_client.IsConnected);

            var result = await _client.SendRequestAsync(new AvailabilityRequest()
            {
                Query =
                    new AvailabilityQuery()
                    {
                        Params =
                            new AvailabilityQueryParamas()
                            {
                                Departure = "МОВ",
                                Arrival = "XБP",
                                AnswerParams = new AvailabilityAnswerParams() { ShowFlightTime = true }
                            }
                    }
            });

            Assert.NotNull(result);
        }
    }
}
