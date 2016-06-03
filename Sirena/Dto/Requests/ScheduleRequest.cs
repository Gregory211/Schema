using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// OK
namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class ScheduleRequest : DtoRequest
    {
        public ScheduleRequest() { }
        [DataMember]
        [XmlElement("query")]
        public ScheduleQuery Query { get; set; }
    }

    [DataContract, Serializable]
    public sealed class ScheduleQuery
    {
        public ScheduleQuery() { }
        [DataMember]
        [XmlElement("schedule")]
        public ScheduleQueryParams Params { get; set; }
    }

    /// <summary>
    /// Расписание(schedule)
    /// Запрос используется для получения расписания на дату или период дат.
    /// Таблица 12. Параметры запроса schedule
    /// </summary>
    [DataContract, Serializable]
    public sealed class ScheduleQueryParams
    {
        public ScheduleQueryParams() { }
        /// <summary>
        /// Код города или порта отправления
        /// </summary>
        [DataMember]
        [XmlElement("departure")]
        public String Departure { get; set; }

        [DataMember]
        [XmlElement("arrival")]
        public String Arrival { get; set; }

        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets or sets the departure or period start date.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is DD.MM.YY.
        /// </remarks>
        [XmlIgnore]
        public DateTime? Date { get; set; }

        /// <summary>
        /// USE Date instead.
        /// </summary>
        [DataMember]
        [XmlElement("date")]
        public String ProxyDate
        {
            get
            {
                return (Date.HasValue) ? Date.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                Date = DateTime.ParseExact(value, "dd.MM.yy", null);
            }
        }

        /// <summary>
        /// Gets or sets the period finish date.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is DD.MM.YY.
        /// </remarks>
        [XmlIgnore]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// USE Date instead.
        /// </summary>
        [DataMember]
        [XmlElement("date2")]
        public String ProxyEndDate
        {
            get
            {
                return (EndDate.HasValue) ? EndDate.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                EndDate = DateTime.ParseExact(value, "dd.MM.yy", null);
            }
        }

        /// <summary>
        /// Gets or sets the minimal departure time.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is HH24MI.
        /// </remarks>
        [XmlIgnore]
        public TimeSpan? TimeFrom { get; set; }

        /// <summary>
        /// USE TimeFrom instead.
        /// </summary>
        [DataMember]
        [XmlElement("time_from")]
        public String ProxyTimeFrom
        {
            get
            {
                return (TimeFrom.HasValue) ? TimeFrom.Value.ToString("hhmm") : null;
            }
            set
            {
                TimeFrom = TimeSpan.ParseExact(value, "hhmm", null);
            }
        }

        /// <summary>
        /// Gets or sets the maximal departure time.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is HH24MI.
        /// </remarks>
        [XmlIgnore]
        public TimeSpan? TimeTill { get; set; }

        /// <summary>
        /// USE TimeTill instead.
        /// </summary>
        [DataMember]
        [XmlElement("time_till")]
        public String ProxyTimeTill
        {
            get
            {
                return (TimeTill.HasValue) ? TimeTill.Value.ToString("hhmm") : null;
            }
            set
            {
                TimeTill = TimeSpan.ParseExact(value, "hhmm", null);
            }
        }

        /// <summary>
        /// Gets or sets the flag to search only direct flights.
        /// </summary>
        [DataMember]
        [XmlElement("direct")]
        public Boolean OnlyDirect { get; set; }

        [DataMember]
        [XmlElement("request_params")]
        public ScheduleRequestParams RequestParams { get; set; }

        [DataMember]
        [XmlElement("answer_params")]
        public ScheduleAnswerParams AnswerParams { get; set; }
    }

    /// <summary>
    /// Таблица 13. Дополнительные параметры для запроса расписания
    /// </summary>
    [DataContract, Serializable]
    public sealed class ScheduleRequestParams
    {
        public ScheduleRequestParams() { }
        /// <summary>
        /// Gets or sets the flag to show connected flights
        /// for a single company.
        /// </summary>
        [DataMember]
        [XmlElement("only_m2_joints")]
        public Boolean OnlyM2Joints { get; set; }
    }

    /// <summary>
    /// Таблица 14. Параметры ответа на запрос расписания
    /// </summary>
    [DataContract, Serializable]
    public sealed class ScheduleAnswerParams
    {
        public ScheduleAnswerParams() { }
        /// <summary>
        /// Gets or sets the marker to show the flight time info.
        /// </summary>
        [DataMember]
        [XmlElement("show_flighttime")]
        public Boolean ShowFlightTime { get; set; }

        /// <summary>
        /// Gets or sets the full date (dd.MM.yyyy) format in answers.
        /// </summary>
        [DataMember]
        [XmlElement("full_date")]
        public Boolean FullDate { get; set; }

        /// <summary>
        /// Gets or sets the marker to show a city or an airport in the response.
        /// </summary>
        [DataMember]
        [XmlElement("mark_cityport")]
        public Boolean MarkCityPort { get; set; }

        /// <summary>
        /// Gets or sets the availability for issueing an e-ticket.
        /// </summary>
        [DataMember]
        [XmlElement("show_et")]
        public Boolean ShowEt { get; set; }
    }
}
