using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//OK
namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class ScheduleResponse : DtoResponse
    {
        public ScheduleResponse() { }
        /// <summary>
        /// Gets the response answer.
        /// </summary>
        [DataMember][XmlElement("answer")]
        public ScheduleAsnwer Answer { get; set; }
    }
    [DataContract, Serializable]
    public sealed class ScheduleAsnwer
    {
        public ScheduleAsnwer() { }
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
        /// Gets the answer body.
        /// </summary>
        [DataMember][XmlElement("schedule")]
        public ScheduleAnswerBody Body { get; set; }
    }

    /// <summary>
    /// Ответ состоит из элементов flight и flights. 
    /// Маршруты, состоящие из одного сегмента, передаются в элементе flight. 
    /// Стыковочные маршруты передаются в элементе flights, cодержащем элементы flight. 
    /// </summary>

    [DataContract, Serializable]
    public sealed class ScheduleAnswerBody
    {
        public ScheduleAnswerBody() { }
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
        [DataMember][XmlAttribute("date")]
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
                    var date = default(DateTime);
                    if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                    {
                        Date = date;
                        return;
                    }
                    if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                    {
                        Date = date;
                        return;
                    }
                }
            }
        }

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

        /// <summary>
        /// Gets the single segment flights.
        /// </summary>
        [DataMember][XmlElement("flight")]
        public ScheduleFlight[] Flights { get; set; }

        /// <summary>
        /// Gets the multi segment flights.
        /// </summary>
        [DataMember][XmlElement("flights")]
        public ScheduleSegmentedFlight[] MultiSegmentFlights { get; set; }
    }
    [DataContract, Serializable]
    public sealed class ScheduleSegmentedFlight
    {
        public ScheduleSegmentedFlight() { }
        /// <summary>
        /// Gets the flight segments.
        /// </summary>
        [DataMember][XmlElement("flight")]
        public ScheduleFlight[] FlightSegments { get; set; }
    }

    /// <summary>
    /// Таблица 15. Структура элемента flight
    /// </summary>
    [DataContract, Serializable]
    public sealed class ScheduleFlight
    {
        public ScheduleFlight() { }
        /// <summary>
        /// Gets the air company name.
        /// </summary>
        [DataMember][XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Код оперирующего перевозчика для рейсов код-шер
        /// </summary>
        [DataMember][XmlElement("operating_company")]
        public String OperatingCompany { get; set; }

        /// <summary>
        /// Gets flight number.
        /// </summary>
        [DataMember][XmlElement("num")]
        public String FlightNumber { get; set; }

        /// <summary>
        /// Gets the departure city.
        /// </summary>
        [DataMember][XmlElement("origin")]
        public String Original { get; set; }

        /// <summary>
        /// Gets the departure city terminal.
        /// </summary>
        [DataMember][XmlElement("orig_term")]
        public String OriginalTerminal { get; set; }

        /// <summary>
        /// Gets the destination city.
        /// </summary>
        [DataMember][XmlElement("destination")]
        public String Destination { get; set; }

        /// <summary>
        /// Gets the destination city terminal.
        /// </summary>
        [DataMember][XmlElement("dest_term")]
        public String DestinationTerminal { get; set; }

        /// <summary>
        /// Gets the departure time.
        /// </summary>
        [DataMember][XmlElement("depttime")]
        public ScheduleFlightTime DepartureTime { get; set; }

        /// <summary>
        /// Gets the arrival time.
        /// </summary>
        [DataMember][XmlElement("arrvtime")]
        public ScheduleFlightTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets the period info for the schedule.
        /// </summary>
        [DataMember][XmlElement("period")]
        public ScheduleFlightPeriod Period { get; set; }

        /// <summary>
        /// Gets the airplane code.
        /// </summary>
        [DataMember][XmlElement("airplane")]
        public String Airplane { get; set; }

        /// <summary>
        /// Gets the flight summary.
        /// </summary>
        [DataMember][XmlElement("classes")]
        public ScheduleFlightClassInfo ClassInfo { get; set; }

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
        [DataMember][XmlElement("flightTime")]
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
        [DataMember][XmlElement("et_possible")]
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
    public sealed class ScheduleFlightPeriod
    {
        public ScheduleFlightPeriod() { }
        /// <summary>
        /// Gets the period begin date.
        /// </summary>
        [XmlIgnore]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// USE BeginDate instead.
        /// </summary>
        [DataMember][XmlAttribute("begin")]
        public String ProxyBeginDate
        {
            get
            {
                return BeginDate.ToString("dd.MM.yy");
            }
            set
            {
                var date = default(DateTime);
                if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                {
                    BeginDate = date;
                    return;
                }
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                {
                    BeginDate = date;
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the period end date.
        /// </summary>
        [XmlIgnore]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// USE EndDate instead.
        /// </summary>
        [DataMember][XmlAttribute("end")]
        public String ProxyEndDate
        {
            get
            {
                return EndDate.ToString("dd.MM.yy");
            }
            set
            {
                var date = default(DateTime);
                if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                {
                    EndDate = date;
                    return;
                }
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                {
                    EndDate = date;
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the week days.
        /// </summary>
        [DataMember][XmlAttribute("days")]
        public String Days { get; set; }
    }
    [DataContract, Serializable]
    public sealed class ScheduleFlightClassInfo
    {
        public ScheduleFlightClassInfo() { }
        /// <summary>
        /// Gets the economy class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [DataMember][XmlAttribute("econom")]
        public Int32 EconomyAvailable { get; set; }

        /// <summary>
        /// Gets the business class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [DataMember][XmlAttribute("business")]
        public Int32 BussinessAvailable { get; set; }

        /// <summary>
        /// Gets the first class availablity.
        /// </summary>
        /// <remarks>
        /// 1 is available.
        /// </remarks>
        [DataMember][XmlAttribute("first")]
        public Int32 FirstAvailable { get; set; }
    }
    [DataContract, Serializable]
    public sealed class ScheduleFlightTime
    {
        public ScheduleFlightTime() { }
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
        [DataMember][XmlAttribute("dayshift")]
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

}
