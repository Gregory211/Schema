using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;
using Sirena.Helpers;

namespace Sirena.Tests
{
    /// <summary>
    /// Интеграционные тесты для шлюза Sirena Travel
    /// </summary>
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
                Port = 8080,
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

        #region AvailabilityRequest
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
                                Arrival = "ХБР",
                                AnswerParams = new AvailabilityAnswerParams { ShowFlightTime = true },
                                Connections = "only ",
                                ProxyDate = DateTime.Now.ToString("dd.MM.yy")
                            }
                    }
            };

            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);
        }

        #endregion

        #region ScheduleRequest
        [Test]
        public async Task ScheduleRequest()
        {
            var request = new ScheduleRequest()
            {
                Query =
                    new ScheduleQuery()
                    {
                        Params =
                            new ScheduleQueryParams()
                            {
                                Departure = "МОВ",
                                Arrival = "ХБР",
                                ProxyDate = DateTime.Now.ToString("dd.MM.yy"),
                                AnswerParams = new ScheduleAnswerParams()
                                {
                                    FullDate = true,
                                    ShowEt = true,
                                    MarkCityPort = true,
                                    ShowFlightTime = true
                                }
                            }
                    }
            };

            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);
        }
        #endregion
    }
}
