using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SirenaSoapClient.SirenaGateServiceReference;
using SirenaSoapClient.SirenaGateServiceReferenceTwo;
using Sirena.Helpers;
using System.Xml;
using System.Xml.Linq;
using Sirena.Dto.Requests;
using Sirena;

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
                                Arrival = "LED",
                                AnswerParams = new AvailabilityAnswerParams { ShowFlightTime = true },
                                Connections = "only ",
                                ProxyDate = DateTime.Now.ToString("dd.MM.yy")
                            }
                    }
            };
            #endregion
            #region PricingRequest
            var pricingRequest = new DescribeRequest()
            {
                Query = new DescribeQuery()
                {
                    Params = new DescribeQueryParamas()
                    {
                        
                        RequestParams = new DescribeQueryParamas.DescribeRequestParams()
                        {
                            ShowRealCodes = true,
                            ShowAll = true
                            
                        },
                        Data = "airport"
                       
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
                    var response = sirenaGateServiceClient.SendDescribeRequest(pricingRequest);
                    
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
