using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sirena.Helpers;

namespace Sirena.Dto.Requests
{
    /// <summary>
    /// Покупка перевозки 
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class SellingQueryRequest : DtoRequest
    {
        public SellingQueryRequest() : base() { }

        [DataMember]
        [XmlElement("query")]
        public SellingQuery Query { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingQuery
    {
        public SellingQuery() { }

        [DataMember]
        [XmlElement("selling_query")]
        public SellingQueryParamas Params { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SellingQueryParamas
    {
        public SellingQueryParamas() { }

        /// <summary>
        /// Gets or sets the segment params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("segment")]
        public SellingQueryReguestSegment ReguestSegment { get; set; }

        /// <summary>
        /// Gets or set the passenger params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("passenger")]
        public SellingQueryReguestPassenger ReguestPassenger { get; set; }

        [DataMember]
        [XmlArray("customer"), XmlArrayItem("phone")]
        public List<Phone> Customer { get; set; }

        [DataMember]
        [XmlElement("paydoc")]
        public PayDoc Paydoc { get; set; }
    }

    /// <summary>
    /// Таблица 113. Параметры элемента segment
    /// </summary>
    [DataContract, Serializable]
    public sealed class SellingQueryReguestSegment
    {
        public SellingQueryReguestSegment()
        {
        }

        [DataMember]
        [XmlElement("departure")]
        public string Departure { get; set; }

        [DataMember]
        [XmlElement("arrival")]
        public string Arrival { get; set; }

        [XmlIgnore]
        public DateTime? Date { get; set; }

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
                    Date = DateTime.ParseExact(value, "dd.MM.yyyy", null);
            }
        }

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
        /// Gets or sets the subclass.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("subclass")]
        public List<string> SubClasses { get; set; }

    }

    [DataContract, Serializable]
    public sealed class SellingQueryReguestPassenger
    {
        public SellingQueryReguestPassenger()
        {
        }

        [DataMember]
        [XmlElement("family")]
        public string Family { get; set; }

        [DataMember]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// dd.MM.yy
        /// </summary>
        [XmlElement("age")]
        public string Age { get; set; }

        [XmlElement("sex")]
        public SellingQueryPassengerSex Sex { get; set; }

        [DataMember]
        [XmlElement("nationality")]
        public string Nationality { get; set; }

        [DataMember]
        [XmlElement("category")]
        public string Category { get; set; }

        [DataMember]
        [XmlElement("doccode")]
        public string DocCode { get; set; }

        [DataMember]
        [XmlElement("doc")]
        public string Doc { get; set; }
    }

    [DataContract]
    public enum SellingQueryPassengerSex
    {
        [EnumMember]
        [XmlEnum("female")]
        Female,

        [EnumMember]
        [XmlEnum("male")]
        Male,
    }

    [DataContract, Serializable]
    public sealed class Phone
    {
        public Phone() { }
        /// <summary>
        /// Gets or sets the 
        ///  type.
        /// </summary>
        [XmlIgnore]
        public ContactType ContactType { get; set; }

        /// <summary>
        /// USE ContactType instead.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the contact comment.
        /// </summary>
        [XmlAttribute("comment")]
        public String Comment { get; set; }

        /// <summary>
        /// Gets or sets the contact value such as an email or phone.
        /// </summary>
        [XmlText]
        public String Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PayDoc
    {
        public PayDoc() { }

        [DataMember]
        [XmlElement("formpay")]
        public string Formpay { get; set; }

        [DataMember]
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
