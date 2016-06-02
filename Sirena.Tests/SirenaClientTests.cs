using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;
using Sirena.Helpers;

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
        public void ConnectTest()
        {
            Assert.IsTrue(_client.IsConnected);
        }

        [Test]
        public async Task AvailabilityRequest()
        {
            var request = new AvailabilityRequest()
            {
                Query =
                    new AvailabilityQuery()
                    {
                        Params =
                            new AvailabilityQueryParamas()
                            {
                                Departure = "МОВ",
                                Arrival = "XБP",
                                AnswerParams = new AvailabilityAnswerParams { ShowFlightTime = true }
                            }
                    }
            };


            var result = _client.SendRequestAsync(request).Result;

            //var rawRequest = SerializationHelper.Serialize(request);
            //var result = await _client.SendRequestAsync(rawRequest);

            Assert.NotNull(result);
        }
    }
}
