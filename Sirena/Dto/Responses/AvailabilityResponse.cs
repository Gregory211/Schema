using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirena.Helpers;

namespace Sirena
{
    /// <summary>
    /// Represents the Sirena availability response.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class AvailabilityResponse : DtoResponse
    {
        /// <summary>
        /// Gets the answer object.
        /// </summary>
        [DataMember]
        [XmlElement("answer")]
        public AvailabilityAnswer Answer { get; set; }
    }
    [DataContract, Serializable]
    public sealed class AvailabilityAnswer
    {
        /// <summary>
        /// Gets the pult name.
        /// </summary>
        [DataMember]
        [XmlAttribute("pult")]
        public String Pult { get; set; }

        /// <summary>
        /// Gets the message id.
        /// </summary>
        [DataMember]
        [XmlAttribute("msgid")]
        public String MessageId { get; set; }

        /// <summary>
        /// Gets the time when response was processed.
        /// </summary>
        [XmlIgnore]
        public DateTime? Time { get; set; }

        /// <summary>
        /// USE Time instead.
        /// </summary>
        [DataMember]
        [XmlAttribute("time")]
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
        /// Gets the answer body.
        /// </summary>
        [DataMember]
        [XmlElement("availability")]
        public AvailabilityAnswerBody Body { get; set; }
    }
    [DataContract, Serializable]
    public sealed class AvailabilityAnswerBody
    {
        /// <summary>
        /// Gets the departure city.
        /// </summary>
        [DataMember]
        [XmlAttribute("departure")]
        public String Departure { get; set; }

        /// <summary>
        /// Gets the arrival city.
        /// </summary>
        [DataMember]
        [XmlAttribute("arrival")]
        public String Arrival { get; set; }

        /// <summary>
        /// Gets the flight class.
        /// </summary>
        [DataMember]
        [XmlAttribute("class")]
        public String Class { get; set; }

        /// <summary>
        /// Gets the flight joint type.
        /// </summary>
        [XmlIgnore]
        public JointType? JointType { get; set; }

        /// <summary>
        /// USE JointType instead.
        /// </summary>
        [XmlAttribute("joint_type")]
        public String ProxyJointType
        {
            get
            {
                return (JointType.HasValue) ? EnumHelper.GetXmlEnumName(JointType.Value) : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    JointType = (JointType)EnumHelper.ParseXmlEnumName(typeof(JointType), value.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the multi segment flights.
        /// </summary>
        [DataMember]
        [XmlElement("flights")]
        public List<AvailabilitySegmentedFlight> MultiSegmentFlights { get; set; }

        /// <summary>
        /// Gets the single segment flights.
        /// </summary>
        [DataMember]
        [XmlElement("flight")]
        public AvailabilityFlight[] Flights { get; set; }

        /// <summary>
        /// Gets the response error.
        /// </summary>
        [DataMember]
        [XmlElement("error")]
        public Error Error { get; set; }

        /// <summary>
        /// Gets the additional info about the response.
        /// </summary>
        [DataMember]
        [XmlElement("info")]
        public Info Info { get; set; }

        /// <summary>
        /// Gets the date of the request departure time.
        /// </summary>
        /// <remarks>
        /// The date format is dd.MM.yyyy.
        /// </remarks>
        [XmlIgnore]
        public DateTime? Date { get; set; }

        /// <summary>
        /// USE Date instead.
        /// </summary>
        [DataMember]
        [XmlAttribute("date")]
        public String ProxyDate
        {
            get
            {
                return (Date.HasValue) ? Date.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Date = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }
    }
    [DataContract, Serializable]
    public sealed class AvailabilitySegmentedFlight
    {
        /// <summary>
        /// Gets the flight segments.
        /// </summary>
        [DataMember]
        [XmlElement("flight")]
        public List<AvailabilityFlight> FlightSegments { get; set; }
    }

    /// <summary>
    /// Таблица 20. Структура элемента flight
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityFlight
    {
        /// <summary>
        /// Gets the air company name.
        /// </summary>
        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Код оперирующего перевозчика для рейсов код-шер
        /// </summary>
        [DataMember]
        [XmlElement("operating_company")]
        public String OperatingCompany { get; set; }

        /// <summary>
        /// Gets flight number.
        /// </summary>
        [DataMember]
        [XmlElement("num")]
        public String FlightNumber { get; set; }

        /// <summary>
        /// Gets the departure city.
        /// </summary>
        [DataMember]
        [XmlElement("origin")]
        public String Original { get; set; }

        /// <summary>
        /// Gets the departure city terminal.
        /// </summary>
        [DataMember]
        [XmlElement("orig_term")]
        public String OriginalTerminal { get; set; }

        /// <summary>
        /// Gets the destination city.
        /// </summary>
        [DataMember]
        [XmlElement("destination")]
        public String Destination { get; set; }

        /// <summary>
        /// Gets the destination city terminal.
        /// </summary>
        [DataMember]
        [XmlElement("dest_term")]
        public String DestinationTerminal { get; set; }

        /// <summary>
        /// Gets the departure time.
        /// </summary>
        [DataMember]
        [XmlElement("depttime")]
        public AvailabilityFlightTime DepartureTime { get; set; }

        /// <summary>
        /// Gets the arrival time.
        /// </summary>
        [DataMember]
        [XmlElement("arrvtime")]
        public AvailabilityFlightTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets the airplane code.
        /// </summary>
        [DataMember]
        [XmlElement("airplane")]
        public String Airplane { get; set; }

        /// <summary>
        /// Gets intermediate stops count during the flight.
        /// </summary>
        [DataMember]
        [XmlIgnore]
        public Int32? IntermediateStops { get; set; }

        /// <summary>
        /// USE IntermediateStops instead.
        /// </summary>
        [DataMember]
        [XmlElement("ilc")]
        public String ProxyIntermediateStops
        {
            get
            {
                return (IntermediateStops.HasValue) ? IntermediateStops.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    IntermediateStops = Int32.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets the flight delay
        /// </summary>
        /// <remarks>
        /// 0 - during a day
        /// else days count.
        /// </remarks>
        [XmlIgnore]
        public Int32? Delay { get; set; }

        /// <summary>
        /// USE Delay instead.
        /// </summary>
        [DataMember]
        [XmlElement("delay")]
        public String ProxyDelay
        {
            get
            {
                return (Delay.HasValue) ? Delay.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Delay = Int32.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets the flight status.
        /// </summary>
        /// <remarks>
        /// The second letter is cyrillic.
        /// "P" or "П" - in the port.
        /// "D" or "У" - flown.
        /// "R" or "Р" - registration closed.
        /// "C" or "О" - canceled.
        /// "M" or "Ц" - ЦОУ????
        /// "G" or "Г" - closed by depth.
        /// </remarks>
        [DataMember]
        [XmlElement("status")]
        public String Status { get; set; }

        /// <summary>
        /// Gets the flight subclasses.
        /// </summary>
        [DataMember]
        [XmlElement("subclass")]
        public List<AvailabilityFlightSubClass> SubClasses { get; set; }

        /// <summary>
        /// Gets the flight summary.
        /// </summary>
        [DataMember]
        [XmlElement("summary")]
        public AvailabilityFlightClassInfo ClassInfo { get; set; }

        /// <summary>
        /// Gets the flight time.
        /// </summary>
        /// <remarks>
        /// Only hours and minutes will be used.
        /// </remarks>
        [XmlIgnore]
        public TimeSpan? FlightTime { get; set; }

        /// <summary>
        /// USE FlightTime instead.
        /// </summary>
        [DataMember]
        [XmlElement("flightTime")]
        public String ProxyFlightTime
        {
            get
            {
                return (FlightTime.HasValue) ? FlightTime.Value.ToString("hh':'mm") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    FlightTime = TimeSpan.ParseExact(value, "g", null);
                }
            }
        }

        /// <summary>
        /// Gets the flag indicates e-ticketing is possible.
        /// </summary>
        [XmlIgnore]
        public Boolean? IsEticketPossible { get; set; }

        /// <summary>
        /// USE IsEticketPossible instead.
        /// </summary>
        [DataMember]
        [XmlElement("et_possible")]
        public String ProxyIsETicketPossible
        {
            get
            {
                return (IsEticketPossible.HasValue) ? IsEticketPossible.ToString().ToLower() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    IsEticketPossible = Boolean.Parse(value);
                }
            }
        }
    }
    [DataContract, Serializable]
    public sealed class AvailabilityFlightTime
    {
        /// <summary>
        /// Gets the shift of the initial departure date.
        /// </summary>
        /// <remarks>
        /// Can be negative.
        /// </remarks>
        [XmlIgnore]
        public Int32? DayShift { get; set; }

        /// <summary>
        /// USE DayShift instead.
        /// </summary>
        [XmlAttribute("dayshift")]
        public String ProxyDayShift
        {
            get
            {
                return (DayShift.HasValue) ? DayShift.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    DayShift = Int32.Parse(value);
                }
            }
        }

        /// <summary>
        /// USE Time instead.
        /// </summary>
        [XmlText]
        public String ProxyTime
        {
            get
            {
                return (Time.HasValue) ? Time.Value.ToString("hh':'mm") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Time = TimeSpan.ParseExact(value, "g", null);
                }
            }
        }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <remarks>
        /// Only hours and minutes will be used.
        /// </remarks>
        [XmlIgnore]
        public TimeSpan? Time { get; set; }
    }
    [DataContract, Serializable]
    public sealed class AvailabilityFlightClassInfo
    {
        /// <summary>
        /// Gets the economy class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [XmlAttribute("econom")]
        public Int32 EconomyAvailable { get; set; }

        /// <summary>
        /// Gets the business class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [XmlAttribute("business")]
        public Int32 BussinessAvailable { get; set; }

        /// <summary>
        /// Gets the first class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [XmlAttribute("first")]
        public Int32 FirstAvailable { get; set; }
    }

    /// <summary>
    /// Таблица 21. Структура элемента subclass
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityFlightSubClass
    {
        /// <summary>
        /// Gets the subclass name.
        /// </summary>
        [XmlText]
        public String Text { get; set; }

        /// <summary>
        /// Gets the base class name.
        /// </summary>
        [XmlAttribute("baseclass")]
        public String BaseClass { get; set; }

        /// <summary>
        /// Gets the seats count.
        /// </summary>
        /// <remarks>
        /// if negative:
        /// -1 - subclass closed
        /// -2 - seat by request
        /// -3 - ability to put in the wait list.
        /// </remarks>
        [XmlAttribute("count")]
        public Int32 Count { get; set; }
    }
}
