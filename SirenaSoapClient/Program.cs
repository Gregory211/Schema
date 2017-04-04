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
using System.Xml.Serialization;
using System.IO;

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
                        Data = "country"
                       
                    }
                }
            };
            #endregion
            var xmlView1 = SerializationHelper.Serialize(pricingRequest);
            Console.WriteLine(xmlView1);

            var segment = new List<BookingRequestSegment>();

            segment.Add(new BookingRequestSegment()
            {
                Airplane = "TU5",
                Arrival = "SVX",
                Class = "Y",
                Company = "UT",
                Departure = "DME",
                Num = "784",
                ProxyDate = "23.03.17"
            });

            segment.Add(new BookingRequestSegment()
            {
                Airplane = "TU5",
                Arrival = "OVB",
                Class = "Y",
                Company = "YQ",
                Departure = "SVX",
                Num = "541",
                ProxyDate = "23.03.17"
            });

            segment.Add(new BookingRequestSegment()
            {
                Airplane = "TU5",
                Arrival = "VVO",
                Class = "Y",
                Company = "7R",
                Departure = "OVB",
                Num = "145",
                ProxyDate = "23.03.17"
            });

            var passengers = new List<BookingRequestPassenger>();
            var contacts = new List<BookingContact>();

            contacts.Add(new BookingContact() { ContactType = ContactType.Email, ProxyContactType = "email", Value = "P.M@BFMELE.COM" });

            passengers.Add(new BookingRequestPassenger()
            {
                Category = "ADT",
                Contacts = contacts,
                Doc = "N5026629",
                DocCode = "OTHER",
                Name = "TestName",
                Surname = "TestSurname",
                ProxyBirthDate = "19.02.96",
                ProxySexType = "male",
                Phones = new BookingContact[] { new BookingContact() {  ContactType = ContactType.Work, ProxyContactType = "work", Value = "+74996088905" } }
            });

            var request = new BookingRequest()
            {
                Query = new BookingQuery()
                {
                    Params = new BookingQueryParams()
                    {
                        Contacts = new BookingRequestContacts()
                        {
                            Phone = new BookingContact()
                            {
                                ContactType = ContactType.Agency,
                                ProxyContactType = nameof(ContactType.Agency).ToLower(),
                                Comment = "credentials",
                                Value = "+74996088905"
                            }
                        },
                        
                        Passengers = passengers,
                        Segments = segment,

                        AnswerParams = new BookingAnswerParams()
                        {
                            ShowTts = true
                        }
                    }
                }
            };

            try
            {
                while (true)
                {
                    //var resp = sirenaGateServiceClient.SendBookingRequest(request);


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
