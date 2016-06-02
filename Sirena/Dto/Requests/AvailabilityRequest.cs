using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// OK
namespace Sirena
{
    /// <summary>
    /// Represents the Sirena availability request.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]    
    public sealed class AvailabilityRequest : DtoRequest
    {
        /// <summary>
        /// Gets or sets the query data.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("query")]
        public AvailabilityQuery Query { get; set; }
    }

    /// <summary>
    /// Represents the query data that used in
    /// the Sirena availability request.
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityQuery
    {
        /// <summary>
        /// Gets or sets the query params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("availability")]
        public AvailabilityQueryParamas Params { get; set; }
    }

    /// <summary>
    /// Таблица 16. Параметры
    /// Наличие мест (availability) 
    /// Запрос используется для получения наличия мест на направлении.
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityQueryParamas
    {
        /// <summary>
        /// Gets or sets the departure city or airport.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("departure")]
        public String Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival city or airport.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("arrival")]
        public String Arrival { get; set; }

        /// <summary>
        /// Gets or sets the departure date.
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
        /// Gets or sets the aviacompany.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string contain 5 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("flight")]
        public String FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the base class.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("baseclass")]
        public String BaseClass { get; set; }

        /// <summary>
        /// Gets or sets the subclass.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("subclass")]
        public List<String> SubClasses { get; set; }

        /// <summary>
        /// Признак вывода только прямых рейсов
        /// </summary>
        [DataMember]
        [XmlElement("direct")]
        public bool Direct { get; set; }

        /// <summary>
        /// Правило отображения стыковочных рейсов
        /// </summary>
        [DataMember]
        [XmlElement("connections")]
        public string Connections { get; set; }


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
        public string ProxyTimeFrom
        {
            get
            {
                return (TimeFrom.HasValue) ? TimeFrom.Value.ToString("hhmm") : null;
            }
            set
            {
                if(value != null)
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
        public string ProxyTimeTill
        {
            get
            {
                return (TimeTill.HasValue) ? TimeTill.Value.ToString("hhmm") : null;
            }
            set
            {
                if (value != null)
                    TimeTill = TimeSpan.ParseExact(value, "hhmm", null);
            }
        }

        /// <summary>
        /// Gets or sets the request params.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        [DataMember]
        [XmlElement("request_params")]
        public AvailabilityRequestParams RequestParams { get; set; }

        /// <summary>
        /// Gets or set the answer params.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        [DataMember]
        [XmlElement("answer_params")]
        public AvailabilityAnswerParams AnswerParams { get; set; }
    }

    /// <summary>
    /// Таблица 17. Параметры ответа на запрос наличия мест
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityAnswerParams
    {
        /// <summary>
        /// Gets or sets the marker to show the flight time info.
        /// </summary>
        [DataMember]
        [XmlElement("show_flighttime")]
        public Boolean ShowFlightTime { get; set; }

        /// <summary>
        /// Gets or sets the marker tos show the base class for all flight subclasses.
        /// </summary>
        [DataMember]
        [XmlElement("show_baseclass")]
        public Boolean ShowBaseClass { get; set; }

        /// <summary>
        /// Gets or sets the date of when getting available places.
        /// </summary>
        [DataMember]
        [XmlElement("return_date")]
        public Boolean ReturnDate { get; set; }

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

    /// <summary>
    /// Таблица 18. Параметры секции request_params
    /// </summary>
    [DataContract, Serializable]
    public sealed class AvailabilityRequestParams
    {
        /// <summary>
        /// Gets or sets the joint type for a flight.
        /// </summary>
        [DataMember]
        [XmlElement("joint_type")]
        public JointType JointType { get; set; }

        /// <summary>
        /// Gets or sets restriction in a Tch selling session.
        /// </summary>
        [DataMember]
        [XmlElement("check_tch_restrictions")]
        public Boolean CheckTchRestrictions { get; set; }

        /// <summary>
        /// Признак обязательного учета ограничений на продажу по картотекам ДАР/ДАГ
        /// </summary>
        [DataMember]
        [XmlElement("use_dag")]
        public Boolean UseDag { get; set; }

        /// <summary>
        /// Признак обязательного учета ограничений на продажу по картотеке ИАК
        /// </summary>
        [DataMember]
        [XmlElement("use_iak")]
        public Boolean UseIak { get; set; }
    }

    /// <summary>
    /// Таблица 19. Описание элемента joint_type
    /// </summary>
    [DataContract, Serializable]
    public enum JointType
    {
        /// <summary>
        /// All joints.
        /// </summary>
        [EnumMember]
        [XmlEnum("jtAll")]
        All,

        /// <summary>
        /// No joints.
        /// </summary>
        [EnumMember]
        [XmlEnum("jtNone")]
        None,

        /// <summary>
        /// All joints for the current avia company.
        /// </summary>
        [EnumMember]
        [XmlEnum("jtAwk")]
        Awk,

        /// <summary>
        /// All joints according to the M2 contract.
        /// </summary>
        [EnumMember]
        [XmlEnum("jtM2")]
        M2,

        /// <summary>
        /// Joints by direct(interline) contracts.
        /// </summary>
        [EnumMember]
        [XmlEnum("jtInterline")]
        Interline,
    }
}
