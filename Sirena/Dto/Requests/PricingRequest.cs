using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sirena.Dto.Requests
{
    /// <summary>
    /// Поиск вариантов перевозки и оценка их стоимости PricingRequest
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class PricingRequest : DtoRequest
    {
        public PricingRequest(): base() { }
        /// <summary>
        /// Gets or sets the query data.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("query")]
        public PricingQuery Query { get; set; }
    }

    /// <summary>
    /// Represents the query data that used in
    /// the Sirena pricing request.
    /// </summary>
    [DataContract, Serializable]
    public sealed class PricingQuery
    {
        public PricingQuery() { }
        /// <summary>
        /// Gets or sets the query params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("pricing")]
        public PricingQueryParamas Params { get; set; }
    }

    /// <summary>
    /// Таблица 28. Параметры запроса pricing
    /// Наличие мест (availability) 
    /// Запрос используется для получения наличия мест на направлении.
    /// </summary>
    [DataContract, Serializable]
    public sealed class PricingQueryParamas
    {
        public PricingQueryParamas() { }

        /// <summary>
        /// Gets or sets the segment params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("segment")]
        public PricingRequestSegment Segment { get; set; }

        /// <summary>
        /// Gets or set the passenger params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("passenger")]
        public PricingRequestPassenger Passenger { get; set; }
    }

    /// <summary>
    /// Таблица 29. Параметры элемента segment
    /// </summary>
    [DataContract, Serializable]
    public sealed class PricingRequestSegment
    {
        public PricingRequestSegment()
        {
        }

        /// <summary>
        /// Gets or sets the departure city or airport.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("departure")]
        public string Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival city or airport.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("arrival")]
        public string Arrival { get; set; }

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
        [DataMember]
        [XmlElement("date")]
        public string ProxyDate
        {
            get
            {
                return (Date.HasValue) ? Date.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (value != null)
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
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string contain 5 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("flight")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the base class.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("baseclass")]
        public string BaseClass { get; set; }

        /// <summary>
        /// Gets or sets the subclass.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("subclass")]
        public List<string> SubClasses { get; set; }

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
        public string Connections { get; set; } = "only";


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
                if (value != null)
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

        // <desire> Список рейсов, которые будут рассматриваться при оценке. Все остальные рассматриваться не будут 
        // <ignore> Список рейсов, исключаемых из рассмотрения 
    }

    /// <summary>
    /// Таблица 30. Параметры элемента passenger
    /// </summary>
    [DataContract, Serializable]
    public sealed class PricingRequestPassenger
    {
        public PricingRequestPassenger()
        {
        }

        /// <summary>
        /// Код категории пассажира
        /// </summary>
        [DataMember]
        [XmlElement("code")]
        public string Code { get; set; }

        /// <summary>
        /// Количество пассажиров категории
        /// </summary>
        [DataMember]
        [XmlElement("count")]
        public int Count { get; set; }

        /// <summary>
        /// Возраст пассажиров категории
        /// </summary>
        [XmlElement("age")]
        public int Age { get; set; }
    }
}
