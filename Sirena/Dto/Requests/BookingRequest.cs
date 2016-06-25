using System;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirena.Helpers;

// OK
namespace Sirena
{
    /// <summary>
    /// Represents the Sirena booking request.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class BookingRequest : DtoRequest
    {
        public BookingRequest(): base() { }
        /// <summary>
        /// Gets or sets the query data.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("query")]
        public BookingQuery Query { get; set; }
    }

    /// <summary>
    /// Represents the query data that used in
    /// the Sirena booking request.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingQuery
    {
        public BookingQuery() { }
        /// <summary>
        /// Gets or sets the query params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("booking")]
        public BookingQueryParams Params { get; set; }
    }

    /// <summary>
    /// Represents the query params that used in
    /// the Sirena booking request.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingQueryParams
    {
        public BookingQueryParams() { }

        [DataMember]
        [XmlElement("segment")]
        public List<BookingRequestSegment> Segments { get; set; }

        [DataMember]
        [XmlElement("passenger")]
        public List<BookingRequestPassenger> Passengers { get; set; }

        [DataMember]
        [XmlElement("contacts")]
        public BookingRequestContacts Contacts { get; set; }

        [DataMember]
        [XmlElement("answer_params")]
        public BookingAnswerParams AnswerParams { get; set; }

        [DataMember]
        [XmlElement("request_params")]
        public BookingRequestParams RequestParams { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingAnswerParams
    {
        public BookingAnswerParams() { }
        /// <summary>
        /// Gets or sets the flag to show the airplane type.
        /// </summary>
        [DataMember]
        [XmlElement("show_tts")]
        public Boolean ShowTts { get; set; }

        /// <summary>
        /// Gets or sets the flag to show the Upt info.
        /// </summary>
        [DataMember]
        [XmlElement("show_upt_rec")]
        public Boolean ShowUptRecord { get; set; }

        /// <summary>
        /// Gets or sets the flag to show remarks about passengers.
        /// </summary>
        [DataMember]
        [XmlElement("add_remarks")]
        public Boolean AddRemarks { get; set; }

        [DataMember]
        [XmlElement("show_baseclass")]
        public Boolean ShowBaseClass { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingRequestParams
    {
        public BookingRequestParams() { }
        /// <summary>
        /// Gets or sets the language used in the response.
        /// </summary>
        [DataMember]
        [XmlElement("lang")]
        public String Language { get; set; }

        /// <summary>
        /// Gets or sets the ticket series to estimate the fare.
        /// </summary>
        [DataMember]
        [XmlElement("tick_ser")]
        public String TicketSeries { get; set; }

        /// <summary>
        /// Gets or sets the parcel agency
        /// </summary>
        /// <remarks>
        /// The value should contain 5 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("parcel_agency")]
        public String ParcelAgency { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingRequestSegment
    {
        public BookingRequestSegment() { }
        /// <summary>
        /// Gets or sets the segment id.
        /// </summary>
        [XmlIgnore]
        public Int32? Id { get; set; }

        /// <summary>
        /// USE Id instead.
        /// </summary>
        [DataMember]
        [XmlAttribute("id")]
        public String ProxyId
        {
            get
            {
                return (Id.HasValue) ? Id.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Id = Int32.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the aviacompany.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string contain 5 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("num")]
        public string Num { get; set; }

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
        /// Required.
        /// The date format is DD.MM.YY.
        /// </remarks>
        [XmlIgnore]
        public DateTime Date { get; set; }

        /// <summary>
        /// USE Date instead.
        /// </summary>
        [DataMember]
        [XmlElement("date")]
        public string ProxyDate
        {
            get
            {
                return Date.ToString("dd.MM.yy");
            }
            set
            {
                Date = DateTime.ParseExact(value, "dd.MM.yy", null);
            }
        }

        /// <summary>
        /// Gets or sets the airplane.
        /// </summary>
        [DataMember]
        [XmlElement("airplane")]
        public string Airplane { get; set; }

        /// <summary>
        /// Gets or sets the flight class.
        /// </summary>
        /// <remarks>
        /// Required.
        /// The string must contain 1 letter.
        /// </remarks>
        [DataMember]
        [XmlElement("subclass")]
        public string SubClass { get; set; }

        [DataMember]
        [XmlElement("class")]
        public string Class { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingRequestPassenger
    {
        public BookingRequestPassenger() { }
        /// <summary>
        /// Gets the passenger id.
        /// </summary>
        [XmlIgnore]
        public Int32? Id { get; set; }

        /// <summary>
        /// USE Id instead.
        /// </summary>
        [DataMember]
        [XmlAttribute("id")]
        public String ProxyId
        {
            get
            {
                return (Id.HasValue) ? Id.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Id = Int32.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets the flag that indicated the passenger is a leader.
        /// </summary>
        [XmlIgnore]
        public Boolean? IsLeader { get; set; }

        /// <summary>
        /// USE IsLeader instead.
        /// </summary>
        [DataMember]
        [XmlAttribute("lead_pass")]
        public String ProxyIsLeader
        {
            get
            {
                return (IsLeader.HasValue) ? IsLeader.Value.ToString().ToLower() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    IsLeader = Boolean.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets the passenger surname.
        /// </summary>
        [DataMember]
        [XmlElement("surname")]
        public String Surname { get; set; }

        /// <summary>
        /// Gets the passenger name.
        /// </summary>
        [DataMember][XmlElement("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets the passenger category.
        /// </summary>
        [DataMember][XmlElement("category")]
        public String Category { get; set; }

        /// <summary>
        /// Gets or sets the passenger sex.
        /// </summary>
        [XmlIgnore]
        public BookingPassengerSex Sex { get; set; }

        /// <summary>
        /// USE Sex instead.
        /// </summary>

        [DataMember][XmlElement("sex")]
        public String ProxySexType
        {
            get
            {
                return EnumHelper.GetXmlEnumName(Sex);
            }
            set
            {
                Sex = (BookingPassengerSex)EnumHelper.ParseXmlEnumName(typeof(BookingPassengerSex), value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the passenger birthdate.
        /// </summary>
        [XmlIgnore]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// USE BirthDate instead.
        /// </summary>
        [DataMember]
        [XmlElement("age")]
        public string ProxyBirthDate
        {
            get
            {
                return BirthDate.ToString("dd.MM.yy");
            }
            set
            {
                DateTime date;
                if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                {
                    BirthDate = date;
                    return;
                }
                if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                {
                    BirthDate = date;
                }
            }
        }

        /// <summary>
        /// Gets or sets the document type.
        /// </summary>
        /// <remarks>
        /// Possible values:
        /// "SR" - birth certificated.
        /// "PS" - russian passport.
        /// "NP" - foreign passport.
        /// </remarks>
        [DataMember]
        [XmlElement("doccode")]
        public string DocCode { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        [DataMember]
        [XmlElement("doc")]
        public string Doc { get; set; }

        /// <summary>
        /// Gets or sets the document expiration date.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is DD.MM.YY.
        /// </remarks>
        [XmlIgnore]
        public DateTime? DocumentExpirationDate { get; set; }

        /// <summary>
        /// USE DocumentExpirationDate instead.
        /// </summary>
        [DataMember]
        [XmlElement("pspexpire")]
        public String ProxyDocumentExpirationDate
        {
            get
            {
                return DocumentExpirationDate.HasValue ? DocumentExpirationDate.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    DocumentExpirationDate = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }

        /// <summary>
        /// Gets or sets the passenger nationality.
        /// </summary>
        [DataMember]
        [XmlElement("nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the discount document type.
        /// </summary>
        [DataMember][XmlElement("doccode_disc")]
        public String DiscountDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the discount document number.
        /// </summary>
        [DataMember][XmlElement("doc_disc")]
        public String DiscountDocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the discount document expiration date.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// The date format is DD.MM.YY.
        /// </remarks>
        [XmlIgnore]
        public DateTime? DiscountDocumentExpirationDate { get; set; }

        /// <summary>
        /// USE DiscountDocumentExpirationDate instead.
        /// </summary>
        [DataMember][XmlElement("pspexpire_disc")]
        public String ProxyDiscountDocumentExpirationDate
        {
            get
            {
                return DiscountDocumentExpirationDate.HasValue ? DiscountDocumentExpirationDate.Value.ToString("dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    DiscountDocumentExpirationDate = DateTime.ParseExact(value, "dd.MM.yy", null);
                }
            }
        }

        /// <summary>
        /// Gets or sets the passenger phones.
        /// </summary>
        [DataMember][XmlElement("phone")]
        public BookingContact[] Phones { get; set; }

        /// <summary>
        /// Gets or sets the passenger contacts.
        /// </summary>
        [DataMember][XmlElement("contact")]
        public List<BookingContact> Contacts { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingRequestContacts
    {
        public BookingRequestContacts() { }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [DataMember][XmlElement("phone")]
        public BookingContact Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [DataMember][XmlElement("email")]
        public string Email { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingContact
    {
        public BookingContact() { }

        [DataMember]
        [XmlIgnore]
        public ContactType ContactType { get; set; }

        [DataMember]
        [XmlAttribute("type")]
        public string ProxyContactType
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

        [DataMember]
        [XmlAttribute("comment")]
        public string Comment { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// ReguestPassenger genders.
    /// </summary>
    [DataContract]
    public enum BookingPassengerSex
    {
        [EnumMember]
        [XmlEnum("female")]
        Female,

        [EnumMember]
        [XmlEnum("male")]
        Male,
    }

    /// <summary>
    /// Contains contact types.
    /// </summary>
    [DataContract]
    public enum ContactType
    {
        [EnumMember]
        [XmlEnum("agency")]
        Agency,

        [EnumMember]
        [XmlEnum("mobile")]
        Mobile,

        [EnumMember]
        [XmlEnum("home")]
        Home,

        [EnumMember]
        [XmlEnum("work")]
        Work,

        [EnumMember]
        [XmlEnum("fax")]
        Fax,

        [EnumMember]
        [XmlEnum("hotel")]
        Hotel,

        [EnumMember]
        [XmlEnum("email")]
        Email,
    }
}
