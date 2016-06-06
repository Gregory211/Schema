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
                        Segment = new PricingRequestSegment()
                        {
                            Departure = "МОВ",
                            Arrival = "СПТ",
                            ProxyDate = DateTime.Now.AddDays(4).ToString("dd.MM.yy")
                        },
                        Passenger = new PricingRequestPassenger()
                        {
                            Code = "ААА",
                            Count = 1
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
