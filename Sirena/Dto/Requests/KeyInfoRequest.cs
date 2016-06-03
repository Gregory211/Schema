using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//OK
namespace Sirena
{
    /// <summary>
    /// Represents the Sirena key info request.
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class KeyInfoRequest : DtoRequest
    {
        
        [DataMember][XmlElement("query")]
        public KeyInfoQuery Query { get; set; }

        public KeyInfoRequest() 
        {
            Query = new KeyInfoQuery();
        }
    }
    [DataContract, Serializable]
    public sealed class KeyInfoQuery
    {
        [DataMember][XmlElement("key_info")]
        public KeyInfoQueryParams Params { get; set; }

        public KeyInfoQuery()
        {
            Params = new KeyInfoQueryParams();
        }
    }
    [DataContract, Serializable]
    public sealed class KeyInfoQueryParams
    {
        [DataMember][XmlElement("answer_params")]
        public KeyInfoAnswerParams AnswerParams { get; set; }

        public KeyInfoQueryParams()
        {
            AnswerParams = new KeyInfoAnswerParams();
        }
    }
    [DataContract, Serializable]
    public sealed class KeyInfoAnswerParams
    {
        [DataMember][XmlElement("lang")]
        public String LanguageParams { get; set; }

        public KeyInfoAnswerParams()
        {
            LanguageParams = "eng";
        }
    }
}
