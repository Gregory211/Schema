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

            var request = new AvailabilityRequest()
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
                                ProxyTimeFrom                              =  ""
                            }
                    }
            };

            var xmlView1 = SerializationHelper.Serialize(request);
            Console.WriteLine(xmlView1);

            try
            {
                Console.Write("Send request -> ");
                var response = sirenaGateServiceClient.SendAvailabilityRequest(request);
                Console.WriteLine("Success");
                Console.WriteLine("Response: ");
                var xmlView2 = SerializationHelper.Serialize(response);
                Console.WriteLine(xmlView2);
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
