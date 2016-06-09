using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sirena
{
    /// <summary>
    /// Represents the Sirena fares response.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class FaresResponse : DtoResponse
    {
        public FaresResponse() { }
        [DataMember][XmlElement("answer")]
        public FaresAnswer Answer { get; set; }
    }
    [DataContract, Serializable]
    public sealed class FaresAnswer
    {
        public FaresAnswer() { }
        /// <summary>
        /// Gets the pult name.
        /// </summary>
        [DataMember][XmlAttribute("pult")]
        public String Pult { get; set; }

        /// <summary>
        /// Gets the message id.
        /// </summary>
        [DataMember][XmlAttribute("msgid")]
        public String MessageId { get; set; }

        /// <summary>
        /// Gets the time when response was processed.
        /// </summary>
        [XmlIgnore]
        public DateTime? Time { get; set; }

        /// <summary>
        /// USE Time instead.
        /// </summary>
        [DataMember][XmlAttribute("time")]
        public String ProxyTime
        {
            get
            {
                return (Time.HasValue) ? Time.Value.ToString("HH:mm:ss dd.MM.yyyy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Time = DateTime.ParseExact(value, "HH:mm:ss dd.MM.yyyy", null);
                }
            }
        }

        /// <summary>
        /// Gets the fares.
        /// </summary>
        [DataMember][XmlElement("fares")]
        public FaresAnswerBody Body { get; set; }
    }
    [DataContract, Serializable]
    public sealed class FaresAnswerBody
    {
        public FaresAnswerBody() { }
        /// <summary>
        /// Gets the departure city.
        /// </summary>
        [DataMember][XmlAttribute("departure")]
        public String Departure { get; set; }

        /// <summary>
        /// Gets the arrival city.
        /// </summary>
        [DataMember][XmlAttribute("arrival")]
        public String Arrival { get; set; }

        /// <summary>
        /// Gets the departure time.
        /// </summary>
        [XmlIgnore]
        public DateTime? DepartureTime { get; set; }

        /// <summary>
        /// USE DepartureTime instead.
        /// </summary>
        [DataMember][XmlAttribute("deptdate")]
        public String ProxyDepartureTime
        {
            get
            {
                return (DepartureTime.HasValue) ? DepartureTime.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    DepartureTime = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }

        /// <summary>
        /// Gets the booking date.
        /// </summary>
        [XmlIgnore]
        public DateTime? BookDate { get; set; }

        /// <summary>
        /// USE BookDate instead.
        /// </summary>
        [DataMember][XmlAttribute("bookdate")]
        public String ProxyBookDate
        {
            get
            {
                return (BookDate.HasValue) ? BookDate.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    BookDate = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }

        /// <summary>
        /// Gets the company name.
        /// </summary>
        [DataMember][XmlAttribute("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets the passenger category.
        /// </summary>
        [DataMember][XmlAttribute("passenger")]
        public String PassengerCategory { get; set; }

        /// <summary>
        /// Gets the fares.
        /// </summary>
        [DataMember][XmlElement("fare")]
        public Fare[] Fares { get; set; }

        /// <summary>
        /// Gets the response error.
        /// </summary>
        [DataMember][XmlElement("error")]
        public Error Error { get; set; }

        /// <summary>
        /// Gets the additional info about the response.
        /// </summary>
        [DataMember][XmlElement("info")]
        public Info Info { get; set; }
    }
    /// <summary>
    /// Таблица 24. Структура элемента Ответ на запрос Справка по тарифам (fares)
    /// </summary>
    [DataContract, Serializable]
    public sealed class Fare
    {
        public Fare() { }
        /// <summary>
        /// Gets the fares name.
        /// </summary>
        [DataMember][XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets the fare subclasses.
        /// </summary>
        [DataMember][XmlElement("subclass")]
        public String[] SubClasses { get; set; }

        /// <summary>
        /// Gets the flight direction.
        /// </summary>
        /// <remarks>
        /// Possible values in Cyrillic:
        /// Т - there
        /// Х - there and back
        /// М - route
        /// И - interline
        /// К - round
        /// С - throughout
        /// В - round throughout
        /// </remarks>
        [DataMember][XmlElement("direction")]
        public String Direction { get; set; }

        /// <summary>
        /// Gets the flight cost in the specified currencies.
        /// </summary>
        [DataMember][XmlElement("rate")]
        public FaresCurrencyRate[] CurrencyRates { get; set; }

        /// <summary>
        /// Gets the company name.
        /// </summary>
        [DataMember][XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets the flight number.
        /// </summary>
        [DataMember][XmlElement("num")]
        public String FlightNumber { get; set; }

        /// <summary>
        /// Gets the UPT code.
        /// </summary>
        [DataMember][XmlElement("remark")]
        public String Remark { get; set; }

        /// <summary>
        /// Gets the passenger categories.
        /// </summary>
        [DataMember][XmlElement("category")]
        public String[] PassengerCategories { get; set; }

        /// <summary>
        /// Gets the code for the route fare.
        /// </summary>
        [DataMember][XmlElement("route")]
        public String RouteCode { get; set; }

        /// <summary>
        /// Gets the fare expiration date.
        /// </summary>
        [XmlIgnore]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// USE ExpirationDate instead.
        /// </summary>
        [DataMember][XmlElement("validto")]
        public String ProxyExpirationDate 
        {
            get
            {
                return (ExpirationDate.HasValue) ? ExpirationDate.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    ExpirationDate = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }

        /// <summary>
        /// Gets the minimum stay.
        /// </summary>
        [DataMember][XmlElement("minstay")]
        public String MinimumStay { get; set; }

        /// <summary>
        /// Gets the maximum stay.
        /// </summary>
        [DataMember][XmlElement("maxstay")]
        public String MaximumStay { get; set; }

        /// <summary>
        /// Gets the UPT parameters for getting the additional fare info.
        /// </summary>
        [DataMember][XmlElement("upt")]
        public FaresUpt Upt { get; set; } 
    }
    [DataContract, Serializable]
    public sealed class FaresCurrencyRate
    {
        public FaresCurrencyRate() { }
        /// <summary>
        /// Gets the currency name in Cyrillic.
        /// </summary>
        [DataMember][XmlAttribute("currency")]
        public String Currency { get; set; }

        /// <summary>
        /// Gets the currency value.
        /// </summary>
        [XmlIgnore]
        public Double Value
        {
            get
            {
                return Double.Parse(ProxyValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            set
            {
                var nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                ProxyValue = value.ToString("0.00", nfi);
            }
        }

        /// <summary>
        /// USE Value instead.
        /// </summary>
        [XmlText]
        public String ProxyValue { get; set; }
    }
    [DataContract, Serializable]
    public sealed class FaresUpt
    {
        public FaresUpt() { }
        [DataMember]
        [XmlAnyElement]
        public XElement[] CustomElements { get; set; }
    }
}
