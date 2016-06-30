﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirena.Helpers;

namespace Sirena.Dto.Responses
{
    /// <summary>
    /// Represents the Sirena availability response.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class PricingResponse : DtoResponse
    {
        public PricingResponse(): base() { }

        /// <summary>
        /// Gets the answer object.
        /// </summary>
        [DataMember]
        [XmlElement("answer")]
        public PricingAnswer Answer { get; set; }

    }

    [DataContract, Serializable]
    public sealed class PricingAnswer
    {
        public PricingAnswer() { }

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

        [DataMember]
        [XmlAttribute("instance")]
        public string Instance { get; set; }

        /// <summary>
        /// Gets the answer body.
        /// </summary>
        [DataMember]
        [XmlElement("pricing")]
        public PricingAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PricingAnswerBody
    {
        public PricingAnswerBody() { }

        [DataMember]
        [XmlAttribute("results")]
        public string Results { get; set; }
      
        [DataMember]
        [XmlElement("variant")]
        public PricingVariant[] Variant { get; set; }

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
    }

    [DataContract, Serializable]
    public sealed class PricingVariant
    {
        [DataMember]
        [XmlAttribute("fop")]
        public string Fop { get; set; }

        [DataMember]
        [XmlAttribute("card_type")]
        public string CardType { get; set; }

        [DataMember]
        [XmlAttribute("seance")]
        public string Seance { get; set; }

        [DataMember]
        [XmlElement("flight")]
        public PricingFlight[] Flights { get; set; }

        [DataMember]
        [XmlElement("segmentTransferTime")]
        public SegmentTransferTime SegmentTransferTime { get; set; }

        [DataMember]
        [XmlElement("variant_total")]
        public VariantTotal VariantTotal { get; set; }
    }

    [DataContract, Serializable]
    public sealed class SegmentTransferTime
    {
        [DataMember]
        [XmlAttribute("iSegmentNum")]
        public string iSegmentNum { get; set; }

        [DataMember]
        [XmlAttribute("iSegmentOrig")]
        public string iSegmentOrig { get; set; }

        [DataMember]
        [XmlAttribute("iSegmentDest")]
        public string iSegmentDest { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class VariantTotal
    {
        [DataMember]
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PricingFlight
    {
        public PricingFlight(){}

        [DataMember]
        [XmlAttribute("iSegmentNum")]
        public string iSegmentNum { get; set; }

        [DataMember]
        [XmlAttribute("iSegmentOrig")]
        public string iSegmentOrig { get; set; }

        [DataMember]
        [XmlAttribute("iSegmentDest")]
        public string iSegmentDest { get; set; }

        [DataMember]
        [XmlAttribute("oSegmentPartNum")]
        public string oSegmentPartNum { get; set; }

        [DataMember]
        [XmlAttribute("oSegmentPartQuantity")]
        public string oSegmentPartQuantity { get; set; }

        /// <summary>
        /// Gets the air company name.
        /// </summary>
        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets flight number.
        /// </summary>
        [DataMember]
        [XmlElement("num")]
        public string Num { get; set; }

        /// <summary>
        /// Gets the departure city.
        /// </summary>
        [DataMember]
        [XmlElement("origin")]
        public Origin Original { get; set; }


        /// <summary>
        /// Gets the destination city.
        /// </summary>
        [DataMember]
        [XmlElement("destination")]
        public Destination Destination { get; set; }

        /// <summary>
        /// Gets the departure time.
        /// </summary>
        [DataMember]
        [XmlElement("deptdate")]
        public DeptDate DeptDate { get; set; }

        /// <summary>
        /// Gets the arrival time.
        /// </summary>
        [DataMember]
        [XmlElement("arrvdate")]
        public ArrvDate ArrvDate { get; set; }

        /// <summary>
        /// HH:mm
        /// </summary>
        [DataMember]
        [XmlElement("depttime")]
        public string DeptTime { get; set; }

        /// <summary>
        /// HH:mm
        /// </summary>
        [DataMember]
        [XmlElement("arrvtime")]
        public string ArrvTime { get; set; }

        [DataMember]
        [XmlElement("class")]
        public string Class { get; set; }

        [DataMember]
        [XmlElement("subclass")]
        public string SubClass { get; set; }

        /// <summary>
        /// Gets the airplane code.
        /// </summary>
        [DataMember]
        [XmlElement("airplane")]
        public string Airplane { get; set; }

        [DataMember]
        [XmlElement("price")]
        public Price[] Price { get; set; }

    }

    [DataContract, Serializable]
    public sealed class Origin
    {
        [DataMember]
        [XmlAttribute("terminal")]
        public string Terminal { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Destination
    {
        [DataMember]
        [XmlAttribute("terminal")]
        public string Terminal { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class DeptDate
    {
        [DataMember]
        [XmlAttribute("delay")]
        public string Delay { get; set; }

        /// <summary>
        /// dd.MM.yy
        /// </summary>
        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class ArrvDate
    {
        [DataMember]
        [XmlAttribute("delay")]
        public string Delay { get; set; }

        /// <summary>
        /// dd.MM.yy
        /// </summary>
        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Price
    {
        /****** Аттрибуты****************************/
        [DataMember]
        [XmlAttribute("passenger-id")]
        public string PassengerId { get; set; }

        [DataMember]
        [XmlAttribute("code")]
        public string Code { get; set; }

        [DataMember]
        [XmlAttribute("count")]
        public string Count { get; set; }

        [DataMember]
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        [DataMember]
        [XmlAttribute("ticket")]
        public string Ticket { get; set; }

        [DataMember]
        [XmlAttribute("fc")]
        public string Fc { get; set; }

        [DataMember]
        [XmlAttribute("accode")]
        public string Accode { get; set; }

        [DataMember]
        [XmlAttribute("validating_company")]
        public string ValidatingCompany { get; set; }

        [DataMember]
        [XmlAttribute("fop")]
        public string Fop { get; set; }

        [DataMember]
        [XmlAttribute("orig_code")]
        public string OrigCode { get; set; }

        /******Элементы****************************/
        [DataMember]
        [XmlElement("vat")]
        public Vat Vat { get; set; }

        [DataMember]
        [XmlElement("fare ")]
        public Fare Fare { get; set; }

        [DataMember]
        [XmlElement("tax ")]
        public Tax Tax { get; set; }

        [DataMember]
        [XmlElement("total")]
        public string Total { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Vat
    {
        [DataMember]
        [XmlAttribute("fare")]
        public string Fare { get; set; }

        [DataMember]
        [XmlAttribute("zz")]
        public string Zz { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Fare
    {
        [DataMember]
        [XmlAttribute("remark")]
        public string Remark { get; set; }

        [DataMember]
        [XmlAttribute("fare_expdate")]
        public string FareExpdate { get; set; }

        [DataMember]
        [XmlAttribute("code")]
        public string Code { get; set; }

        [DataMember]
        [XmlAttribute("base_code")]
        public string BaseCode { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Tax
    {
        [DataMember]
        [XmlAttribute("code")]
        public string Code { get; set; }

        [DataMember]
        [XmlAttribute("owner")]
        public string Owner { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }
}