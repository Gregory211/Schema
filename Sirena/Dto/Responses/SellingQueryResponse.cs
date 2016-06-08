using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sirena.Dto.Requests;
using Sirena.Helpers;

namespace Sirena.Dto.Responses
{

    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class SellingQueryResponse : DtoResponse
    {

        public SellingQueryResponse() : base() { }

        [DataMember]
        [XmlElement("answer")]
        public SellingQueryAnswer Answer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingQueryAnswer
    {
        public SellingQueryAnswer()
        {
        }

        [DataMember]
        [XmlAttribute("pult")]
        public string Pult { get; set; }

        [DataMember]
        [XmlAttribute("msgid")]
        public string MessageId { get; set; }

        [XmlIgnore]
        public DateTime? Time { get; set; }

        [DataMember]
        [XmlAttribute("time")]
        public string ProxyTime
        {
            get { return (Time.HasValue) ? Time.Value.ToString("HH:mm:ss dd.MM.yyyy") : null; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Time = DateTime.ParseExact(value, "HH:mm:ss dd.MM.yyyy", null);
                }
            }
        }

        [DataMember]
        [XmlAttribute("instance")]
        public string Instance { get; set; }

        [DataMember]
        [XmlElement("selling_query")]
        public Body Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Body
    {
        public Body(){}

        [DataMember]
        [XmlAttribute("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlAttribute("agency")]
        public string Agency { get; set; }


        [DataMember]
        [XmlElement("pnr")]
        public Pnr Pnr { get; set; }

        [DataMember]
        [XmlElement("contacts")]
        public Contacts Contacts { get; set; }

        [DataMember]
        [XmlElement("timeout")]
        public Timeout Timeout { get; set; }

        [DataMember]
        [XmlElement("cost")]
        public Cost Cost { get; set; }


        [DataMember]
        [XmlElement("error")]
        public Error Error { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Pnr
    {
        public Pnr() { }

        [DataMember]
        [XmlArray("passengers"), XmlArrayItem("passenger")]
        public List<SellingQueryResponsePassanger> Passenger = new List<SellingQueryResponsePassanger>();

        [DataMember]
        [XmlArray("segments"), XmlArrayItem("segment")]
        public List<SellingQueryResponseSegment> Segments = new List<SellingQueryResponseSegment>();

        [DataMember][XmlElement("prices")]
        public SellingPrices Prices { get; set; }

        [DataMember][XmlElement("regnum")]
        public string Regnum { get; set; }

        [DataMember][XmlElement("version")]
        public string Version { get; set; }

        [DataMember][XmlElement("utc_timelimit")]
        public string UtcTimelimit { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingQueryResponsePassanger
    {
        public SellingQueryResponsePassanger()
        {
        }

        [DataMember]
        [XmlAttribute("id")]
        public string Id { get; set; }

        [DataMember]
        [XmlAttribute("lead_pass")]
        public bool LeadPass { get; set; }

        [DataMember]
        [XmlElement("name")]
        public string Name { get; set; }

        [DataMember]
        [XmlElement("surname")]
        public string Surname { get; set; }

        [XmlElement("sex")]
        public SellingQueryPassengerSex Sex { get; set; }

        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        [XmlElement("birthdate")]
        public string Birthdate { get; set; }

        [XmlElement("age")]
        public int Age { get; set; }

        [DataMember]
        [XmlElement("doccode")]
        public string DocCode { get; set; }

        [DataMember]
        [XmlElement("doc")]
        public string Doc { get; set; }

        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        [XmlElement("pspexpire")]
        public string Pspexpire { get; set; }

        [DataMember]
        [XmlElement("category")]
        public Category Category { get; set; }

        [DataMember]
        [XmlElement("doc_country")]
        public string DocCountry { get; set; }

        [DataMember]
        [XmlElement("nationality")]
        public string Nationality { get; set; }

        [DataMember]
        [XmlElement("residence")]
        public string Residence { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Category
    {
        public Category()
        {
        }

        [DataMember]
        [XmlAttribute("rbm")]
        public string Rbm { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingQueryResponseSegment
    {
        public SellingQueryResponseSegment()
        {
        }

        [DataMember]
        [XmlAttribute("id")]
        public string Id { get; set; }

        [DataMember]
        [XmlElement("company")]
        public string Company { get; set; }

        [DataMember]
        [XmlElement("flight")]
        public string FlightNumber { get; set; }

        [DataMember]
        [XmlElement("subclass")]
        public string SubClass { get; set; }

        [DataMember]
        [XmlElement("class")]
        public string Class { get; set; }

        [DataMember]
        [XmlElement("baseclass")]
        public string BaseClass { get; set; }

        [DataMember]
        [XmlElement("seatcount")]
        public int Seatcount { get; set; }

        [DataMember]
        [XmlElement("airplane")]
        public string Airplane { get; set; }

        [DataMember]
        [XmlElement("departure")]
        public DepartureArrivalInfo Departure { get; set; }

        [DataMember]
        [XmlElement("arrival")]
        public DepartureArrivalInfo Arrival { get; set; }

        [DataMember]
        [XmlElement("status")]
        public Status Status { get; set; }

        /// <summary>
        /// HH:mm
        /// </summary>
        [DataMember]
        [XmlElement("flightTime")]
        public string FlightTime { get; set; }

    }

    [DataContract, Serializable]
    public sealed class DepartureArrivalInfo
    {
        public DepartureArrivalInfo() { }

        [DataMember]
        [XmlElement("city")]
        public string City { get; set; }

        [DataMember]
        [XmlElement("airport")]
        public string Airport { get; set; }

        /// <summary>
        /// HH:mm
        /// </summary>
        [DataMember]
        [XmlElement("time")]
        public string Time { get; set; }

        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        [DataMember]
        [XmlElement("date")]
        public string Date { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Status
    {
        public Status() { }

        [DataMember]
        [XmlAttribute("text")]
        public string Text { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingPrices
    {
        public SellingPrices() { }

        [DataMember][XmlAttribute("tick_ser")]
        public string TickSer { get; set; }

        [DataMember][XmlAttribute("fop")]
        public string Fop { get; set; }

        [DataMember]
        [XmlElement("price")]
        public Price[] PricesList { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Contacts
    {
        public Contacts() { }

        [DataMember]
        [XmlArrayItem("contact")]
        public List<Contact> ContactsList = new List<Contact>();

        [DataMember]
        [XmlElement("customer")]
        public Customer Customer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Contact
    {
        public Contact() { }
        /// <summary>
        /// Gets or sets the 
        ///  type.
        /// </summary>
        [XmlIgnore]
        public ContactType ContactType { get; set; }

        [XmlAttribute("type")]
        public String ProxyContactType
        {
            get
            {
                return EnumHelper.GetXmlEnumName(ContactType);
            }
            set
            {
                ContactType = (ContactType)EnumHelper.ParseXmlEnumName(typeof(ContactType), value.ToString());
            }
        }

        [XmlAttribute("loc_id")]
        public string LocId { get; set; }

        [XmlAttribute("cont_id")]
        public string ContId { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public class Timeout
    {
        public Timeout() { }

        /// <summary>
        /// HH:mm dd.MM.yyyy
        /// </summary>
        [DataMember]
        [XmlAttribute("utc_deadline")]
        public string UtcDeadline { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public class Cost
    {
        public Cost() { }

        [DataMember]
        [XmlAttribute("curr")]
        public string Curr { get; set; }

        [DataMember]
        [XmlText]
        public String Value { get; set; }
    }

    [DataContract, Serializable]
    public class Customer
    {
        public Customer() { }

        [DataMember]
        [XmlElement("firstname")]
        public string Firstname { get; set; }

        [DataMember]
        [XmlElement("lastname")]
        public string Lastname { get; set; }
    }
}
