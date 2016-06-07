using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using Sirena.Dto.Requests;

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
        //[Test]
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
        #region PricingRequest
        [Test]
        public async Task PricingRequest_Example()
        {
            var request = new PricingRequest()
            {
                Query = new PricingQuery()
                {
                    Params = new PricingQueryParamas()
                    {
                        Segment = new PricingRequestSegment()
                        {
                            Departure = "МОВ",
                            Arrival = "СПТ",
                            ProxyDate = DateTime.Now.ToString("dd.MM.yy")
                        },
                        Passenger = new PricingRequestPassenger()
                        {
                            Code = "ААА",
                            Count = 1
                        }
                    }
                }
            };

            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);
        }
        #endregion
        #region BookingRequest

        public async Task<BookingAnswerBody> BookingRequest()
        {
            var request = new BookingRequest
            {
                Query = new BookingQuery
                {

                    Params = new BookingQueryParams
                    {
                        Segments = new List<BookingRequestSegment>()
                        {
                            new BookingRequestSegment
                            {
                                Company  = "UT", Num = "203", Departure = "МОВ", Arrival = "СПТ", ProxyDate = DateTime.Now.AddDays(10).ToString("dd.MM.yy"), Class = "Y"
                            }
                        },
                        Passengers = new List<BookingRequestPassenger>()
                        {
                            new BookingRequestPassenger()
                            {
                                Surname = "ИВАНОВ",
                                Name = "ВАСИЛИЙ ПЕТРОВИЧ",
                                ProxyBirthDate = new DateTime(1988,5,25).ToString("dd.MM.yy"),
                                Category = "ААА",
                                ProxySexType = "male",
                                DocCode = "ПС",
                                Doc = "1234561234",
                                Nationality = "РФ",
                                Phones = new List<BookingContact>()
                                {
                                    new BookingContact { ContactType = BookingContactType.Mobile, Comment = "ЗВОНИТЬ ПОСЛЕ 19:00", Value = "79101234567" },
                                    new BookingContact { ContactType =  BookingContactType.Work, Value = "74957654321" }
                                }
                            }
                        },
                        Contacts = new BookingRequestContacts()
                        {
                            Phone = new BookingContact()
                            {
                                ContactType = BookingContactType.Agency,
                                Comment = "ДОП. ОФИС #15",
                                Value = "74991234567"
                            }
                        },
                        AnswerParams = new BookingAnswerParams()
                        {
                            ShowTts = true
                        }
                    }
                }
            };


            var result = await _client.SendRequestAsync(request);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            return result.Answer.Body;
        }
        [Test]
        public async Task BookingRequest_Example()
        {           
            var result = await BookingRequest();

            Assert.NotNull(result);
            Assert.Null(result.Error);
        }
        #endregion
        #region BookingCancelRequest
        [Test]
        public async Task BookingCancelRequest_Example()
        {
            var bookingBody = BookingRequest().Result;
            var bookingResponsePassenger = bookingBody.PassengerNameRecord.Passengers.FirstOrDefault();

            if (bookingResponsePassenger != null)
            {
                var request = new BookingCancelRequest
                {
                    Query = new BookingCancelQuery()
                    {
                        Params = new BookingCancelQueryParams()
                        {
                            Surname = bookingResponsePassenger.Surname,
                            Regnum = bookingBody.Regnum
                        }
                    }

                };

                var result = await _client.SendRequestAsync(request);

                Assert.NotNull(result.Answer.BookingCancel);
            }
        }
        #endregion
    }
}
