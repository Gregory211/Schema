using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Sirena.Helpers;

namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class BookingResponse : DtoResponse
    {
        [DataMember][XmlElement("answer")]
        public BookingAnswer Answer { get; set; }
    }

    /// <summary>
    /// Represents the booking response answer.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingAnswer
    {
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

        [DataMember][XmlElement("booking")]
        public BookingAnswerBody Body { get; set; }
    }
    [DataContract, Serializable]
    public sealed class BookingAnswerBody
    {
        [DataMember][XmlAttribute("regnum")]
        public String RegistrationNumber { get; set; }

        [DataMember][XmlAttribute("agency")]
        public String Agency { get; set; }

        [DataMember][XmlElement("pnr")]
        public BookingPassengerNameRecord PassengerNameRecord { get; set; }

        [DataMember][XmlElement("contacts")]
        public BookingResponseContacts Contacts { get; set; }

        [XmlIgnore]
        public Boolean? LatinRegistration { get; set; }

        [DataMember][XmlElement("latin_registration")]
        public String ProxyLatinRegistration
        {
            get
            {
                return (LatinRegistration.HasValue) ? LatinRegistration.Value.ToString().ToLower() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    LatinRegistration = Boolean.Parse(value);
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
    }
    [DataContract, Serializable]
    public sealed class BookingPassengerNameRecord
    {
        [DataMember][XmlElement("regnum")]
        public String RegistrationNumber { get; set; }

        [XmlIgnore]
        public DateTime TimeLimit { get; set; }

        [DataMember][XmlElement("timelimit")]
        public String ProxyTimeLimit
        {
            get
            {
                return TimeLimit.ToString("dd.MM.yy HH:mm");
            }
            set
            {
                TimeLimit = DateTime.ParseExact(value, "dd.MM.yy HH:mm", null);
            }
        }

        [XmlIgnore]
        public DateTime UtcTimeLimit { get; set; }

        [DataMember][XmlElement("utc_timelimit")]
        public String ProxyUtcTimeLimit
        {
            get
            {
                return UtcTimeLimit.ToString("HH:mm dd.MM.yyyy");
            }
            set
            {
                UtcTimeLimit = DateTime.ParseExact(value, "HH:mm dd.MM.yyyy", null);
            }
        }

        [XmlArray("passengers")]
        [XmlArrayItem("passenger")]
        public BookingResponsePassenger[] Passengers { get; set; }

        [XmlArray("segments")]
        [XmlArrayItem("segment")]
        public BookingResponseSegment[] Segments { get; set; }

        [DataMember][XmlElement("prices")]
        public BookingPrices Prices { get; set; }
    }
    [DataContract, Serializable]
    public sealed class BookingResponseContacts
    {
        [DataMember][XmlElement("email")]
        public String[] Emails { get; set; }

        [DataMember][XmlElement("contact")]
        public BookingContact[] ContactItems { get; set; }
    }
    [DataContract, Serializable]
    public sealed class BookingResponsePassenger
    {
        [XmlIgnore]
        public Int32? Id { get; set; }

        [DataMember][XmlAttribute("id")]
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

        [XmlIgnore]
        public Boolean? IsLeader { get; set; }

        [DataMember][XmlAttribute("lead_pass")]
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

        [DataMember][XmlElement("surname")]
        public String Surname { get; set; }

        [DataMember][XmlElement("name")]
        public String Name { get; set; }

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
        /// Gets the passenger birth date.
        /// </summary>
        [XmlIgnore]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// USE BirthDate instead.
        /// </summary>
        [DataMember][XmlElement("birthdate")]
        public String ProxyBirthDate
        {
            get
            {
                return BirthDate.ToString("dd.MM.yy");
            }
            set
            {
                var date = default(DateTime);
                if (DateTime.TryParseExact(value, "dd.MM.yy", null, DateTimeStyles.None, out date))
                {
                    BirthDate = date;
                    return;
                }
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                {
                    BirthDate = date;
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the passenger age.
        /// </summary>
        [XmlIgnore]
        public Int32? Age { get; set; }

        /// <summary>
        /// USE Age instead.
        /// </summary>
        [DataMember][XmlElement("age")]
        public String ProxyAge
        {
            get
            {
                return (Age.HasValue) ? Age.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Age = Int32.Parse(value);
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
        [DataMember][XmlElement("doccode")]
        public String DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        [DataMember][XmlElement("doc")]
        public String DocumentNumber { get; set; }

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
        [DataMember][XmlElement("pspexpire")]
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
        /// Gets the passenger nationality.
        /// </summary>
        [DataMember][XmlElement("nationality")]
        public String Nationality { get; set; }

        /// <summary>
        /// Gets the discount document type.
        /// </summary>
        [DataMember][XmlElement("doccode_disc")]
        public String DiscountDocumentType { get; set; }

        /// <summary>
        /// Gets the discount document number.
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
        /// Gets the passenger contacts.
        /// </summary>
        [DataMember][XmlElement("contacts")]
        public BookingResponsePassengerContacts Contacts { get; set; }
    }
    [DataContract, Serializable]
    public sealed class BookingResponsePassengerContacts
    {
        [DataMember][XmlElement("phone")]
        public BookingContact[] Phones { get; set; }

        [DataMember][XmlElement("contact")]
        public BookingContact[] ContactItems { get; set; }

        [DataMember][XmlElement("email")]
        public String Email { get; set; }
    }

    /// <summary>
    /// Represents the booking segment.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingResponseSegment
    {
        [XmlIgnore]
        public Int32? Id { get; set; }

        [DataMember][XmlAttribute("id")]
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

        [DataMember][XmlElement("company")]
        public String Company { get; set; }

        [DataMember][XmlElement("flight")]
        public String FlightNumber { get; set; }

        [DataMember][XmlElement("airplane")]
        public String Airplane { get; set; }

        [DataMember][XmlElement("class")]
        public String Class { get; set; }

        [DataMember][XmlElement("subclass")]
        public String SubClass { get; set; }

        [DataMember][XmlElement("seatcount")]
        public Int32 SeatCount { get; set; }

        [DataMember][XmlElement("departure")]
        public BookingResponseSegmentPoint Departure { get; set; }

        [DataMember][XmlElement("arrival")]
        public BookingResponseSegmentPoint Arrival { get; set; }

        /// <summary>
        /// Gets the segment status.
        /// </summary>
        [XmlIgnore]
        public BookingSegmentStatus Status { get; set; }

        /// <summary>
        /// USE Status instead.
        /// </summary>
        [DataMember][XmlElement("status")]
        public String ProxyStatus
        {
            get
            {
                return EnumHelper.GetXmlEnumName(Status);
            }
            set
            {
                Status = (BookingSegmentStatus)EnumHelper.ParseXmlEnumName(typeof(BookingSegmentStatus), value);
            }
        }

    }

    /// <summary>
    /// Represents the booking segment point.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingResponseSegmentPoint
    {
        [DataMember][XmlElement("city")]
        public String City { get; set; }

        [DataMember][XmlElement("airport")]
        public String Airport { get; set; }

        [DataMember][XmlElement("terminal")]
        public String Terminal { get; set; }

        [XmlIgnore]
        public DateTime Date { get; set; }

        [DataMember][XmlElement("date")]
        public String DateProxy
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

        [XmlIgnore]
        public TimeSpan Time { get; set; }

        [DataMember][XmlElement("time")]
        public String ProxyTime
        {
            get
            {
                return Time.ToString("hh':'mm");
            }
            set
            {
                Time = TimeSpan.ParseExact(value, "hh':'mm", null);
            }
        }
    }

    /// <summary>
    /// Represents the booking price collection.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingPrices
    {
        [DataMember][XmlAttribute("tick_ser")]
        public String TicketSeries { get; set; }

        [DataMember][XmlAttribute("fop")]
        public String Fop { get; set; }

        [DataMember][XmlElement("price")]
        public BookingPrice[] Prices { get; set; }

        [DataMember][XmlElement("variant_total")]
        public BookingCurrencyValue VariantTotal { get; set; }
    }

    /// <summary>
    /// Represents the booking price info.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingPrice
    {
        [XmlIgnore]
        public Int32? SegmentId { get; set; }

        [DataMember][XmlAttribute("segment-id")]
        public String ProxySegmentId
        {
            get
            {
                return (SegmentId.HasValue) ? SegmentId.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    SegmentId = Int32.Parse(value);
                }
            }
        }

        [XmlIgnore]
        public Int32? PassengerId { get; set; }

        [DataMember][XmlAttribute("passenger-id")]
        public String ProxyPassengerId
        {
            get
            {
                return (PassengerId.HasValue) ? PassengerId.Value.ToString() : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    PassengerId = Int32.Parse(value);
                }
            }
        }

        [DataMember][XmlAttribute("accode")]
        public String Accode { get; set; }

        [DataMember][XmlElement("fare")]
        public BookingPriceFare Fare { get; set; }

        [XmlArray("taxes")]
        [XmlArrayItem("tax")]
        public BookingPriceTax[] Taxes { get; set; }
    }

    /// <summary>
    /// Represents the fare info.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingPriceFare
    {
        /// <summary>
        /// Gets the fare expiration date.
        /// </summary>
        [XmlIgnore]
        public DateTime? FareExpirationDate { get; set; }
        
        /// <summary>
        /// USE FareExpirationDate instead.
        /// </summary>
        [DataMember][XmlAttribute("fare_expdate")]
        public String ProxyFareExpirationDate
        {
            get
            {
                return (FareExpirationDate.HasValue) ? FareExpirationDate.Value.ToString("yyyy-MM-dd hh:mm") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    FareExpirationDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", null);
                }
            }
        }

        /// <summary>
        /// Gets the fare code info.
        /// </summary>
        [DataMember][XmlElement("code")]
        public BookingPriceCode Code { get; set; }

        /// <summary>
        /// Gets the currency value.
        /// </summary>
        [DataMember][XmlElement("value")]
        public BookingCurrencyValue CurrencyValue { get; set; }
    }

    /// <summary>
    /// Represents the tax info.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingPriceTax
    {
        /// <summary>
        /// Gets the tax owner.
        /// </summary>
        [XmlIgnore]
        public BookingTaxOwner? Owner { get; set; }

        /// <summary>
        /// USE Owner instead.
        /// </summary>
        [DataMember][XmlAttribute("owner")]
        public String ProxyOwnerType
        {
            get
            {
                return (Owner.HasValue) ? EnumHelper.GetXmlEnumName(Owner.Value) : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Owner = (BookingTaxOwner)EnumHelper.ParseXmlEnumName(typeof(BookingTaxOwner), value.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the tax code info.
        /// </summary>
        [DataMember][XmlElement("code")]
        public BookingPriceCode Code { get; set; }

        /// <summary>
        /// Gets the tax currency value.
        /// </summary>
        [DataMember][XmlElement("value")]
        public BookingCurrencyValue CurrencyValue { get; set; }
    }

    /// <summary>
    /// Represents the currency value.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingCurrencyValue
    {
        /// <summary>
        /// Gets the currency type.
        /// </summary>
        [DataMember][XmlAttribute("currency")]
        public String Currency { get; set; }

        /// <summary>
        /// Gets the currency value.
        /// </summary>
        [XmlText]
        public Single Value { get; set; }
    }

    /// <summary>
    /// Represents the code infomation about taxes and fares.
    /// </summary>
    [DataContract, Serializable]
    public sealed class BookingPriceCode
    {
        /// <summary>
        /// Gets the base code.
        /// </summary>
        [DataMember][XmlAttribute("base_code")]
        public String BaseCode { get; set; }

        /// <summary>
        /// Gets the code value.
        /// </summary>
        [XmlText]
        public String Value { get; set; }
    }

    /// <summary>
    /// Contains tax owners.
    /// </summary>
    [DataContract, Serializable]
    public enum BookingTaxOwner
    {
        /// <summary>
        /// Tax owner is aircompany.
        /// </summary>
        [DataMember][XmlEnum("aircompany")]
        Aircompany,

        /// <summary>
        /// Tax owner is agency.
        /// </summary>
        [DataMember][XmlEnum("agency")]
        Agency,

        /// <summary>
        /// Tax owner is neutral.
        /// </summary>
        [DataMember][XmlEnum("neutral")]
        Neutral
    }

    /// <summary>
    /// Contains booking segment statuses.
    /// </summary>
    [DataContract, Serializable]
    public enum BookingSegmentStatus
    {
        /// <summary>
        /// The segment is in a waitlist.
        /// </summary>
        [DataMember][XmlEnum("waitlist")]
        Waitlist,

        /// <summary>
        /// The segment is refused.
        /// </summary>
        [DataMember][XmlEnum("refused")]
        Refused,

        /// <summary>
        /// The segment is confirmed.
        /// </summary>
        [DataMember][XmlEnum("confirmed")]
        Confirmed,

        /// <summary>
        /// The segment is uncofirmed.
        /// </summary>
        [DataMember][XmlEnum("uncofirmed")]
        Uncofirmed,
    }
}
