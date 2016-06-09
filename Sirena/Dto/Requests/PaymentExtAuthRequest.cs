using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sirena.Dto.Responses;

namespace Sirena.Dto.Requests
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class PaymentExtAuthQueryRequest : DtoRequest
    {
        public PaymentExtAuthQueryRequest() : base() { }

        [DataMember]
        [XmlElement("query")]
        public PaymentExtAuthQuery ConfirmQuery { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQuery
    {
        public PaymentExtAuthQuery() { }

        [DataMember]
        [XmlElement("payment-ext-auth")]
        public PaymentExtAuthQueryParams Params { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQueryParams
    {
        public PaymentExtAuthQueryParams() : base() { }

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
        public PaymentExtAuthQueryPayDoc PayDoc { get; set; }

        [DataMember]
        [XmlElement("request_params ")]
        public PaymentExtAuthQueryRequestParams RequestParams { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQueryPayDoc
    {
        public PaymentExtAuthQueryPayDoc() { }

        [DataMember]
        [XmlElement("formpay")]
        public string Formpay { get; set; }

        [DataMember]
        [XmlElement("type")]
        public string Type { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQueryRequestParams
    {
        public PaymentExtAuthQueryRequestParams() { }

        [DataMember]
        [XmlElement("tick_ser")]
        public string TickSer { get; set; }

        [DataMember]
        [XmlElement("payment_timeout")]
        public string PaymentTimeout { get; set; }
    }
}
