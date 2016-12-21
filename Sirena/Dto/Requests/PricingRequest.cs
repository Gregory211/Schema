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
        public PricingRequestSegment[] Segments { get; set; }

        /// <summary>
        /// Gets or set the passenger params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("passenger")]
        public PricingRequestPassenger[] Passengers { get; set; }

        [DataMember]
        [XmlElement("answer_params")]
        public PricingAnswerParams AnswerParams { get; set; }

        [DataMember]
        [XmlElement("request_params")]
        public PricingRequestParams RequestParams { get; set; }
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

        [DataMember]
        [XmlElement("class")]
        public string Class { get; set; }

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
        public string Count { get; set; }

        /// <summary>
        /// Возраст пассажиров категории
        /// </summary>
        [DataMember]
        [XmlElement("age")]
        public string Age { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PricingAnswerParams
    {
        public PricingAnswerParams() : base()
        {

        }

        [DataMember]
        [XmlAttribute("lang")]
        public string Lang { get; set; }

        [DataMember]
        [XmlElement("show_flighttime")]
        public bool ShowFlightTime { get; set; }

        [DataMember]
        [XmlElement("show_io_matching")]
        public bool ShowIoMatching { get; set; }

        [DataMember]
        [XmlElement("show_varianttotal")]
        public bool ShowVariantTotal { get; set; }

        [DataMember]
        [XmlElement("show_available")]
        public bool ShowAvailable { get; set; }

        [DataMember]
        [XmlElement("show_baseclass")]
        public bool ShowBaseClass { get; set; }

        [DataMember]
        [XmlElement("show_reg_latin")]
        public bool ShowRegLatin { get; set; }

        [DataMember]
        [XmlElement("show_upt_rec")]
        public bool ShowUptRec { get; set; }

        [DataMember]
        [XmlElement("show_fareexpdate")]
        public bool ShowFareExpDate { get; set; }

        [DataMember]
        [XmlElement("show_et")]
        public bool ShowEt { get; set; }

        [DataMember]
        [XmlElement("show_n_blanks")]
        public bool ShowNBlanks { get; set; }

        [DataMember]
        [XmlElement("regroup")]
        public bool ReGroup { get; set; }

        [DataMember]
        [XmlElement("split_companies")]
        public bool SplitCompanies { get; set; }

        [DataMember]
        [XmlElement("reference_style_codes")]
        public bool ReferenceStyleCodes { get; set; }

        [DataMember]
        [XmlElement("mark_cityport")]
        public bool MarkCityPort { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PricingRequestParams
    {
        public PricingRequestParams() : base(){}

        [DataMember]
        [XmlElement("min_results")]
        public string MinResults { get; set; }

        [DataMember]
        [XmlElement("max_results")]
        public string MaxResults { get; set; }

        [DataMember]
        [XmlElement("timeout")]
        public string Timeout { get; set; }

        [DataMember]
        [XmlArray("comb_rules")][XmlArrayItem("rule")]
        public PricingCombRule[] CombRules { get; set; }

        [DataMember]
        [XmlElement("formpay")]
        public FormPay FormPay { get; set; }

        [DataMember]
        [XmlElement("et_if_possible")]
        public string EtIfPossible { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PricingCombRule
    {
        public PricingCombRule() : base() { }

        [DataMember]
        [XmlAttribute("comb")]
        public string Comb { get; set; }

        [DataMember]
        [XmlElement("acomp")]
        public string Acomp { get; set; }
    }

    [DataContract, Serializable]
    public sealed class FormPay
    {
        public FormPay() { }

        [DataMember]
        [XmlAttribute("type")]
        public string Type { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }
}
