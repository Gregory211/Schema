using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirenaSoapClient.SirenaGateServiceReference;
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
            var pricingRequest = new FareremarkRequest()
            {
                Query = new FareremarkQuery()
                {
                    Params = new FareremarkQueryParams()
                    {
                        Company = "SU",
                        Code = "C",
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
                                    new XElement("idar1") {Value = "64245"},
                                    new XElement("addon_ida") {Value = "-1"},
                                    new XElement("ntrip") {Value = "0"},
                                    new XElement("nvr") {Value = "-1"},
                                    new XElement("code_upt") {Value = "2000"},
                                    new XElement("tariff") {Value = "0"},
                                    new XElement("deliv_type") {Value = "4"},
                                    new XElement("main_awk") {Value = "119"},
                                    new XElement("cat") {Value = "-1"},
                                    new XElement("vcat"),
                                    new XElement("city1"){Value = "28"},
                                    new XElement("city2") {Value = "26"},
                                    new XElement("dport") {Value = "28"},
                                    new XElement("aport") {Value = "26"},
                                    new XElement("base_fare") {Value = "C"},
                                    new XElement("iit") {Value = "3"},
                                    new XElement("owrt") {Value = "OW"},
                                    new XElement("ddate") {Value = "20161226160431"},
                                    new XElement("fdate") {Value = "20170104200000"},
                                }
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
                    var response = sirenaGateServiceClient.SendFareremarkRequest(pricingRequest);
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
