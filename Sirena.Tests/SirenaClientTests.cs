using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using Sirena.Dto.Requests;
using Sirena.Dto.Responses;
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
        //[Test]
        public async Task FareremarkRequest_Example_14()
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
                                //Value = "Some content as an exact copy from the previous pricing or booking answer"
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

            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            Assert.NotNull(xmlResult);
        }
        #endregion    
        #region PricingRequest
        [Test]
        public async Task PricingRequest_Example()
        {
            #region req 1
            var request = new PricingRequest()
            {
                Query = new PricingQuery()
                {
                    Params = new PricingQueryParamas()
                    {
                        Segments = new[]
                        {
                            new PricingRequestSegment
                            {
                                Company = "UT",
                                Departure = "MOW",
                                Arrival = "LED",
                                ProxyDate = DateTime.Now.AddDays(7).ToString("dd.MM.yy"),
                                Direct = false
                            },
                            new PricingRequestSegment
                            {
                                Company = "UT",
                                Departure = "LED",
                                Arrival = "MOW",
                                ProxyDate = DateTime.Now.AddDays(9).ToString("dd.MM.yy"),
                                Direct = false
                            }
                        },
                        Passengers = new[]
                        {
                            new PricingRequestPassenger
                            {
                                Code = "ADT",
                                Count = "2"
                            },
                        },
                        AnswerParams = new PricingAnswerParams
                        {
                            Lang = "en",
                            ShowFlightTime = true,
                            ShowAvailable = true,
                            ShowIoMatching = true,
                            ShowVariantTotal = true,
                        },
                        RequestParams = new PricingRequestParams
                        {
                            FormPay = new FormPay()
                            {
                                Value = "IN"
                            }
                        }
                    }
                }
            };
            #endregion
            #region req 2
            var request2 = new PricingRequest()
            {
                Query = new PricingQuery()
                {
                    Params = new PricingQueryParamas()
                    {
                        Segments = new[]
                        {
                            new PricingRequestSegment
                            {
                                Company = "UT",
                                Departure = "MOW",
                                Arrival = "LED",
                                ProxyDate = DateTime.Now.AddDays(7).ToString("dd.MM.yy"),
                                Direct = false
                            },
                            new PricingRequestSegment
                            {
                                Company = "UT",
                                Departure = "LED",
                                Arrival = "MOW",
                                ProxyDate = DateTime.Now.AddDays(9).ToString("dd.MM.yy"),
                                Direct = true
                            }
                        },
                        Passengers = new[]
                        {
                            new PricingRequestPassenger
                            {
                                Code = "ADT",
                                Count = "2"
                            },
                        },
                        AnswerParams = new PricingAnswerParams
                        {
                            //Lang = "en",
                            ShowFlightTime = true,
                            ShowAvailable = true,
                            ShowIoMatching = true,
                            ShowVariantTotal = true,
                            ShowBaseClass = true
                        },
                        RequestParams = new PricingRequestParams
                        {
                            FormPay = new FormPay()
                            {
                                Value = "IN"
                            }
                        }
                    }
                }
            };
            #endregion


            var xml = SerializationHelper.Serialize(request2);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            Assert.NotNull(xmlResult);
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
                                Phones = new []
                                {
                                    new BookingContact { ContactType = ContactType.Mobile, Comment = "ЗВОНИТЬ ПОСЛЕ 19:00", Value = "79101234567" },
                                    new BookingContact { ContactType = ContactType.Work,   Value = "74957654321" }
                                }
                            }
                        },
                        Contacts = new BookingRequestContacts()
                        {
                            Phone = new BookingContact()
                            {
                                ContactType = ContactType.Agency,
                                Comment = "ДОП. ОФИС #15",
                                Value = "74991234567"
                            }
                        },
                        AnswerParams = new BookingAnswerParams()
                        {
                            ShowTts = true,
                            ShowBaseClass = true
                        }
                    }
                }
            };


            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            Assert.NotNull(xmlResult);

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
        #region SellingQueryRequest
        [Test]
        public async Task SellingQueryRequest_Example()
        {
            var request = new SellingQueryRequest()
            {
                Query = new SellingQuery
                {
                    Params = new SellingQueryParamas()
                    {
                        ReguestSegment = new SellingQueryReguestSegment()
                        {
                            Company = "UT",
                            FlightNumber = "203",
                            Departure = "MOW",
                            Arrival = "LED",
                            ProxyDate = DateTime.Now.AddDays(5).ToString("dd.MM.yyyy"),
                            SubClasses = new List<string> { "Y" }
                        },
                        ReguestPassenger = new SellingQueryReguestPassenger()
                        {
                            Family = "ИВАНОВ",
                            Name = "ВАСИЛИЙ ПЕТРОВИЧ",
                            Age = "01.06.78",
                            Sex = SellingQueryPassengerSex.Male,
                            Nationality = "РФ",
                            Category = "ААА",
                            DocCode = "ПС",
                            Doc = "1234561234"
                        },
                        Customer = new List<Phone>
                        {
                            new Phone
                            {
                                ContactType = ContactType.Mobile,
                                Value = "84957250900"
                            }
                        },
                        Paydoc = new PayDoc()
                        {
                            Formpay = "ПК",
                            Type = "VI"
                        }
                    }
                }
            };

            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            Assert.NotNull(xmlResult);
        }

        [Test]
        public void SellingQueryRawRequest_DeserializeResponse_Should_Be_Ok()
        {
            var response = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<sirena>\n  <answer pult=\"ОНГВЕБ\" msgid=\"1106032448\" time=\"19:40:39 08.06.2016\" instance=\"ГРУ\">\n    <selling_query regnum=\"199ДКТ\" agency=\"01ОНГ\">\n      <pnr>\n        <passengers>\n          <passenger id=\"12\" lead_pass=\"true\">\n            <name>ВАСИЛИЙ ПЕТРОВИЧ</name>\n            <surname>ИВАНОВ</surname>\n            <sex>male</sex>\n            <birthdate>01.06.1978</birthdate>\n            <age>38</age>\n            <doccode>ПС</doccode>\n            <doc>1234561234</doc>\n            <pspexpire>31.12.2049</pspexpire>\n            <category rbm=\"0\">ААА</category>\n            <doc_country>РФ</doc_country>\n            <nationality>РФ</nationality>\n            <residence>РФ</residence>\n          </passenger>\n        </passengers>\n        <segments>\n          <segment id=\"12\">\n            <company>UT</company>\n            <flight>203</flight>\n            <subclass>Y</subclass>\n            <class>Y</class>\n            <baseclass>Y</baseclass>\n            <seatcount>1</seatcount>\n            <airplane>735</airplane>\n            <departure>\n              <city>МОВ</city>\n              <airport>ВНК</airport>\n              <time>11:05</time>\n              <date>13.06.2016</date>\n            </departure>\n            <arrival>\n              <city>СПТ</city>\n              <airport>ПЛК</airport>\n              <time>12:25</time>\n              <date>13.06.2016</date>\n            </arrival>\n            <status text=\"HK\">confirmed</status>\n            <flightTime>1:20</flightTime>\n          </segment>\n        </segments>\n        <prices tick_ser=\"ЭБМ\" fop=\"ПК\">\n          <price segment-id=\"12\" passenger-id=\"12\" code=\"ААА\" count=\"1\" currency=\"РУБ\" ticket=\"0000000000\" baggage=\"20К\" fc=\"1,12,12,12\" accode=\"298\" validating_company=\"UT\">\n            <vat fare=\"418.18\" zz=\"28.22\"/>\n            <fare remark=\"0151\" fare_expdate=\"2016-06-13 11:05\">\n              <value currency=\"РУБ\">3000.00</value>\n              <code base_code=\"YOW\">YOW</code>\n            </fare>\n            <taxes>\n              <tax owner=\"aircompany\">\n                <code>SA</code>\n                <value currency=\"РУБ\">300.00</value>\n              </tax>\n              <tax owner=\"aircompany\">\n                <code>YQ</code>\n                <value currency=\"РУБ\">1300.00</value>\n              </tax>\n              <tax owner=\"aircompany\">\n                <code>ZZ</code>\n                <value currency=\"РУБ\">185.00</value>\n              </tax>\n              <tax owner=\"agency\">\n                <code>АГ</code>\n                <value currency=\"РУБ\">300.00</value>\n              </tax>\n              <tax owner=\"agency\">\n                <code>ДД</code>\n                <value currency=\"РУБ\">100.00</value>\n              </tax>\n              <tax owner=\"aircompany\">\n                <code>RI</code>\n                <value currency=\"РУБ\">1008.00</value>\n              </tax>\n            </taxes>\n            <payment_info>\n              <payment fop=\"ПК\" num=\"VI\" curr=\"РУБ\">6193.00</payment>\n            </payment_info>\n            <total>6193.00</total>\n          </price>\n          <variant_total currency=\"РУБ\">6193.00</variant_total>\n        </prices>\n        <regnum>199ДКТ</regnum>\n        <version>1</version>\n        <utc_timelimit>08:06 11.06.2016</utc_timelimit>\n      </pnr>\n      <contacts>\n        <contact cont_id=\"1\" loc_id=\"1\" type=\"agency\">74951231234</contact>\n        <contact cont_id=\"2\" loc_id=\"1\" type=\"mobile\">84957250900</contact>\n        <customer>\n          <firstname></firstname>\n          <lastname></lastname>\n        </customer>\n      </contacts>\n      <timeout utc_deadline=\"17:40 08.06.2016\">60</timeout>\n      <cost curr=\"РУБ\">6193.00</cost>\n    </selling_query>\n  </answer>\n</sirena>\n";

            var xml = SerializationHelper.Deserialize<SellingQueryResponse>(response);

            Assert.NotNull(xml);
            Assert.NotNull(xml.Answer.Body.Pnr);
            Assert.NotNull(xml.Answer.Body.Contacts);
            Assert.NotNull(xml.Answer.Body.Timeout);
            Assert.NotNull(xml.Answer.Body.Cost);
        }
        #endregion
        #region PaymentExtAuthQueryRequest

        //[Test]
        public async Task PaymentExtQueryRequest_Example()
        {
            var request = new PaymentExtAuthQueryRequest
            {
                ConfirmQuery = new PaymentExtAuthQuery
                {
                    Params = new PaymentExtAuthQueryParams
                    {
                        Regnum = "0Ж8ФВ9",
                        Surname = "ИВАНОВ",
                        Action = "query",
                        PayDoc = new PaymentExtAuthQueryPayDoc()
                        {
                            Formpay = "ПК",
                            Type = "VI",
                        },
                        RequestParams = new PaymentExtAuthQueryRequestParams { TickSer = "ЭБМ" }
                    }
                }
            };

            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.ConfirmAnswer.Body);
            Assert.Null(result.ConfirmAnswer.Body.Error);

            Assert.NotNull(xmlResult);
        }

        #endregion
        #region PaymentExtAuthConfirmRequest
        //   [Test]
        public async Task PaymentExtConfirmRequest_Example()
        {
            var request = new PaymentExtAuthConfirmRequest
            {
                ConfirmQuery = new PaymentExtAuthConfirmQuery
                {
                    ConfirmParams = new PaymentExtAuthConfirmParams
                    {
                        Regnum = "123456",
                        Surname = "ИВАНОВ",
                        Action = "confirm",
                        PayDoc = new PaymentExtAuthPayDoc()
                        {
                            Formpay = "ПК",
                            Type = "VI",
                            Num = "4405*7915",
                            ExpDate = "01.02.10",
                            Holder = "Mr. Holder",
                            AuthCode = "654321"
                        },
                        Cost = new Cost { Curr = "РУБ", Value = "5000.00" },
                        AnswerParams = new PaymentExtAuthAnswerParams { ReturnReceipt = true }
                    }
                }
            };

            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.ConfirmAnswer.Body);
            Assert.Null(result.ConfirmAnswer.Body.Error);

            Assert.NotNull(xmlResult);
        }
        #endregion
        #region GetInitReceiptsRequest
        //[Test]
        public async Task GetItinReceiptsRequest_Example()
        {
            var request = new GetInitReceiptsRequest
            {
                Query = new GetInitReceiptsQuery
                {
                    Params = new GetInitReceiptsParamas
                    {
                        Regnum = "00001Х",
                        Surname = "ИВАНОВ"
                    }
                }
            };

            var xml = SerializationHelper.Serialize(request);

            var result = await _client.SendRequestAsync(request);
            var xmlResult = await _client.SendRequestAsync(xml);

            Assert.NotNull(result.Answer.Body);
            Assert.Null(result.Answer.Body.Error);

            Assert.NotNull(xmlResult);
        }
        #endregion
    }
}
