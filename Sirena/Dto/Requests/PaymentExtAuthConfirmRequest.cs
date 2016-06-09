using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Sirena.Dto.Responses;

namespace Sirena.Dto.Requests
{

    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class PaymentExtAuthConfirmRequest : DtoRequest
    {
        public PaymentExtAuthConfirmRequest() : base() { }

        [DataMember]
        [XmlElement("query")]
        public PaymentExtAuthConfirmQuery ConfirmQuery { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthConfirmQuery
    {
        public PaymentExtAuthConfirmQuery() { }

        [DataMember]
        [XmlElement("payment-ext-auth")]
        public PaymentExtAuthConfirmParams ConfirmParams { get; set; }
    }

    [DataContract, Serializable]
    public sealed  class PaymentExtAuthConfirmParams
    {
        public PaymentExtAuthConfirmParams() : base() { }

        [DataMember]
        [XmlElement("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlElement("surname")]
        public string Surname { get; set; }

        [DataMember]
        [XmlElement("action")]
        public string Action { get; set; }


        [DataMember]
        [XmlElement("paydoc")]
        public PaymentExtAuthPayDoc PayDoc { get; set; }

        [DataMember]
        [XmlElement("cost ")]
        public Cost Cost { get; set; }

        [DataMember]
        [XmlElement("return_receipt ")]
        public PaymentExtAuthAnswerParams AnswerParams { get; set; }

        [DataMember]
        [XmlElement("request_params ")]
        public PaymentExtAuthRequestParams RequestParams { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthPayDoc
    {
        public PaymentExtAuthPayDoc() { }

        [DataMember]
        [XmlElement("formpay")]
        public string Formpay { get; set; }

        [DataMember]
        [XmlElement("type")]
        public string Type { get; set; }

        [DataMember]
        [XmlElement("num")]
        public string Num { get; set; }

        [DataMember]
        [XmlElement("exp_date")]
        public string ExpDate { get; set; }

        [DataMember]
        [XmlElement("holder")]
        public string Holder { get; set; }

        [DataMember]
        [XmlElement("auth_code")]
        public string AuthCode { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthAnswerParams
    {
        public PaymentExtAuthAnswerParams(){}

        [DataMember]
        [XmlElement("return_receipt")]
        public bool ReturnReceipt { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthRequestParams
    {
        public PaymentExtAuthRequestParams() { }

        [DataMember]
        [XmlElement("tick_ser")]
        public bool TickSer { get; set; }
    }
}
