using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        /// <summary>
        /// Пример 2. Запрос наличия мест на направлении Москва-Хабаровск
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AvailabilityRequest_Example_2()
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
                                Arrival = "СПТ",
                                AnswerParams = new AvailabilityAnswerParams { ShowFlightTime = true },
                                ProxyDate = DateTime.Now.ToString("dd.MM.yy")
                            }
                    }
            };

            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);
        }

        /// <summary>
        /// Пример 10. Запрос наличия мест на направлении
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AvailabilityRequest_Example_10()
        {
            var request = new AvailabilityRequest()
            {
                Query = new AvailabilityQuery()
                {
                    Params = new AvailabilityQueryParamas()
                    {
                        Departure = "МОВ",
                        Arrival = "СПТ",
                        SubClasses = new List<string>()
                                {
                                    "Э",
                                    "К",
                                    "Л",
                                    "М",
                                    "Н",
                                    "Я"
                                },
                        RequestParams = new AvailabilityRequestParams() { JointType = JointType.All },
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
        /// <summary>
        /// Пример 8. Запрос расписания на направлении
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ScheduleRequest_Example_8()
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
                                Arrival = "СПТ",
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
        #region FaresRequest
        /// <summary>
        /// Пример 12. Запрос справки по тарифам
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task FaresRequest_Example_12()
        {
            var request = new FaresRequest()
            {
                Query = new FaresQuery()
                {
                    Params = new FaresQueryParams()
                    {
                        Departure = "МОВ",
                        Arrival = "СПТ",
                        Company = "ПЛ",
                        SubClasses = new List<string> { "Т", "М" },
                        PassengerCategory = "ААА"
                    }
                }
            };

            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);
        }
        #endregion
        #region FareremarkRequests
        /// <summary>
        /// Пример 14. Запрос текста УПТ
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task fareremarkRequest_Example_14()
        {
            var request = new FareremarkRequest()
            {
                Query = new FareremarkQuery()
                {
                    Params = new FareremarkQueryParams()
                    {
                        Company = "SU",
                        Code = "2000",
                        AnswerParams = new FareremarkAnswerParams()
                        {
                            Language = "ru"
                        },
                        RequestParams = new FareremarkRequestParams()
                        {
                            Upt = new FaresUpt()
                            {
                                CustomElements = new XElement[]
                                {
                                    new XElement("idar1") {Value = "31346266"},
                                    new XElement("addon_ida") {Value = "-1"},
                                    new XElement("ntrip") {Value = "0"},
                                    new XElement("nvr") {Value = "-1"},
                                    new XElement("code_upt") {Value = "3004"},
                                    new XElement("tariff") {Value = "0"},
                                    new XElement("deliv_type") {Value = "4"},
                                    new XElement("main_awk") {Value = "31"},
                                    new XElement("cat") {Value = "-1"},
                                    new XElement("vcat"),
                                    new XElement("city1"){Value = "88"},
                                    new XElement("city2") {Value = "26"},
                                    new XElement("dport") {Value = "88"},
                                    new XElement("aport") {Value = "26"},
                                    new XElement("base_fare") {Value = "UWBOW   "},
                                    new XElement("iit") {Value = "3"},
                                    new XElement("owrt") {Value = "OW"},
                                    new XElement("ddate") {Value = "20160215134317"},
                                    new XElement("fdate") {Value = "20160221190000"},
                                }
                            }
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
