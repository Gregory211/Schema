using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// OK
namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class FareremarkRequest : DtoRequest
    {
        public FareremarkRequest() { }
        [DataMember]
        [XmlElement("query")]
        public FareremarkQuery Query { get; set; }
    }
    [DataContract, Serializable]
    public sealed class FareremarkQuery
    {
        public FareremarkQuery() { }
        [DataMember]
        [XmlElement("fareremark")]
        public FareremarkQueryParams Params { get; set; }
    }

    /// <summary>
    /// Таблица 25. Параметры запроса fareremark Условия применения тарифов 
    /// </summary>
    [DataContract, Serializable]
    public sealed class FareremarkQueryParams
    {
        public FareremarkQueryParams() { }
        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <remarks>
        /// The string must contain 3 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("company")]
        public String Company { get; set; }

        /// <summary>
        /// Gets or sets the fare name gotten 
        /// gotten from a fare response.
        /// </summary>
        /// <remarks>
        /// The string must contain 5 letters.
        /// </remarks>
        [DataMember]
        [XmlElement("code")]
        public String Code { get; set; }

        [DataMember]
        [XmlElement("request_params")]
        public FareremarkRequestParams RequestParams { get; set; }

        [DataMember]
        [XmlElement("answer_params")]
        public FareremarkAnswerParams AnswerParams { get; set; }
    }

    /// <summary>
    /// Таблица 26. Параметры секции request_params
    /// </summary>
    [DataContract, Serializable]
    public sealed class FareremarkRequestParams
    {
        public FareremarkRequestParams() { }
        /// <summary>
        /// Gets or sets the sixteenth category for UTP.
        /// </summary>
        /// <remarks>
        /// Uses only for new UTPs.
        /// </remarks>
        [DataMember]
        [XmlElement("cat_16")]
        public string CatSixteen { get; set; }

        /// <summary>
        /// Gets or sets the UPT parameters gotten from a fare response.
        /// </summary>
        [DataMember]
        [XmlElement("upt")]
        public FaresUpt Upt { get; set; }
    }

    [DataContract, Serializable]
    public sealed class FareremarkAnswerParams
    {
        public FareremarkAnswerParams() { }
        [DataMember]
        [XmlElement("lang")]
        public String Language { get; set; }
    }
}
