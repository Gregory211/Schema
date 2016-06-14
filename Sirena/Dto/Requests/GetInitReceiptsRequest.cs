using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirena.Dto.Requests
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class GetInitReceiptsRequest : DtoRequest
    {
        public GetInitReceiptsRequest() : base(){}

        [DataMember]
        [XmlElement("query")]
        public GetInitReceiptsQuery Query { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetInitReceiptsQuery
    {
        public GetInitReceiptsQuery() : base(){}

        [DataMember]
        [XmlElement("get_itin_receipts")]
        public GetInitReceiptsParamas Params { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetInitReceiptsParamas
    {
        public GetInitReceiptsParamas() : base(){}

        [DataMember]
        [XmlElement("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlElement("surname")]
        public string Surname { get; set; }
    }
}
