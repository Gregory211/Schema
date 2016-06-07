using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirena.Dto.Requests
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class BookingCancelRequest : DtoRequest
    {
        public BookingCancelRequest(): base() { }

        [DataMember]
        [XmlElement("query")]
        public BookingCancelQuery Query { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingCancelQuery
    {
        public BookingCancelQuery() { }

        [DataMember]
        [XmlElement("booking-cancel")]
        public BookingCancelQueryParams Params { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingCancelQueryParams
    {
        public BookingCancelQueryParams() { }

        [DataMember]
        [XmlElement("surname")]
        public string Surname { get; set; }

        [DataMember]
        [XmlElement("regnum")]
        public string Regnum { get; set; }
    }
}
