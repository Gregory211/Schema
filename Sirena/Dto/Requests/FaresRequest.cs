using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// OK
namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class FaresRequest : DtoRequest
    {
        [DataMember][XmlElement("query")]
        public FaresQuery Query { get; set;}
    }
    [DataContract, Serializable]
    public sealed class FaresQuery
    {
        [DataMember][XmlElement("fares")]
        public FaresQueryParams Params { get; set; }
    }
    /// <summary>
    /// Таблица 22. Параметры запроса Справка по тарифам (fares)
    /// </summary>
    [DataContract, Serializable]
    public sealed class FaresQueryParams
    {
        /// <summary>
        /// Gets or sets the departure city.
        /// </summary>
        [DataMember][XmlElement("departure")]
        public String Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival city.
        /// </summary>
        [DataMember][XmlElement("arrival")]
        public String Arrival { get; set; }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        [XmlIgnore]
        public DateTime? DepartureTime { get; set; }

        /// <summary>
        /// USE DepartureTime instead.
        /// </summary>
        [DataMember][XmlElement("deptdate")]
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
        /// Gets or sets the booking date.
        /// </summary>
        [XmlIgnore]
        public DateTime? BookDate { get; set; }

        /// <summary>
        /// USE BookDate instead.
        /// </summary>
        [DataMember][XmlElement("bookdate")]
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
        /// Gets or sets the company name.
        /// </summary>
        [DataMember][XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        [DataMember][XmlElement("flight")]
        public String FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the flight subclasses.
        /// </summary>
        [DataMember][XmlElement("subclass")]
        public List<String> SubClasses { get; set; }

        /// <summary>
        /// Gets or sets the flight base class.
        /// </summary>
        [DataMember][XmlElement("baseclass")]
        public String BaseClass { get; set; }

        /// <summary>
        /// Gets or sets the passenger category.
        /// </summary>
        [DataMember][XmlElement("passenger")]
        public String PassengerCategory { get; set; }

        /// <summary>
        /// Gets or sets the request params.
        /// </summary>
        [DataMember][XmlElement("request_params")]
        public FaresRequestParams RequestParams { get; set; }
    }
    /// <summary>
    /// Таблица 23. Параметры секции request_params
    /// </summary>
    [DataContract, Serializable]
    public sealed class FaresRequestParams
    {
        /// <summary>
        /// Gets or sets the ticket series.
        /// </summary>
        [DataMember][XmlElement("tick_ser")]
        public String TicketSeries { get; set; }

        /// <summary>
        /// Gets or sets the flag to show special fares.
        /// </summary>
        [DataMember][XmlElement("tripflag")]
        public Boolean TripFlag { get; set; }
    }
}
