using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirenaSoapClient.SirenaGateServiceReference;
using Sirena.Helpers;

namespace SirenaSoapClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SirenaGateServiceClient sirenaGateServiceClient = new SirenaGateServiceClient();

            #region AvailabilityRequest
            var availabilityRequest = new AvailabilityRequest()
            {
                Query =
                    new AvailabilityQuery()
                    {

                        Params =
                            new AvailabilityQueryParamas()
                            {
                                Departure = "MOW",
                                Arrival = "RIX",
                                AnswerParams = new AvailabilityAnswerParams { ShowFlightTime = true },
                                Connections = "only ",
                                ProxyDate = DateTime.Now.ToString("dd.MM.yy")
                            }
                    }
            };
            #endregion
            #region PricingRequest
            var pricingRequest = new PricingRequest()
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
            var xmlView1 = SerializationHelper.Serialize(pricingRequest);
            Console.WriteLine(xmlView1);

            try
            {
                while (true)
                {
                    Console.Write("Send request -> ");
                    //var response = sirenaGateServiceClient.SendAvailabilityRequest(availabilityRequest);
                    var response = sirenaGateServiceClient.SendPricingRequest(pricingRequest);
                    Console.WriteLine("Success");
                    Console.WriteLine("Response: ");
                    var xmlView2 = SerializationHelper.Serialize(response);
                    Console.WriteLine(xmlView2);

                    Console.Write("'exit' command for quit or press Enter for continiue: ");
                    var exit = Console.ReadLine();
                    if (exit != null && exit.Equals("exit"))
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error");
                Console.WriteLine(exc);
            }

            Console.Read();
        }
    }
}
