using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sirena.Dto.Requests
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class GetInitReceiptsDataRequest : DtoRequest
    {
        public GetInitReceiptsDataRequest() : base() { }

        [DataMember]
        [XmlElement("query")]
        public GetInitReceiptsDataQuery Query { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetInitReceiptsDataQuery
    {
        public GetInitReceiptsDataQuery() : base() { }

        [DataMember]
        [XmlElement("get_itin_receipts_data")]
        public GetInitReceiptsDataParamas Params { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetInitReceiptsDataParamas
    {
        public GetInitReceiptsDataParamas() : base() { }

        [DataMember]
        [XmlElement("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlElement("surname")]
        public string Surname { get; set; }

        [DataMember]
        [XmlElement("standard_data")]
        public bool StandartData { get; set; }
    }
}
