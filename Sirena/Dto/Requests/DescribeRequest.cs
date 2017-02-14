using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sirena.Dto.Requests
{
    /// <summary>
    /// Поиск вариантов перевозки и оценка их стоимости PricingRequest
    /// </summary>
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class DescribeRequest : DtoRequest
    {
        public DescribeRequest() : base() { }
        /// <summary>
        /// Gets or sets the query data.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("query")]
        public DescribeQuery Query { get; set; }
    }

    /// <summary>
    /// Represents the query data that used in
    /// the Sirena pricing request.
    /// </summary>
    [DataContract, Serializable]
    public sealed class DescribeQuery
    {
        public DescribeQuery() { }
        /// <summary>
        /// Gets or sets the query params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("describe")]
        public DescribeQueryParamas Params { get; set; }
    }

    /// <summary>
    /// Таблица 28. Параметры запроса pricing
    /// Наличие мест (availability) 
    /// Запрос используется для получения наличия мест на направлении.
    /// </summary>
    [DataContract, Serializable]
    public sealed class DescribeQueryParamas
    {
        public DescribeQueryParamas() { }

        /// <summary>
        /// Gets or sets the segment params.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [DataMember]
        [XmlElement("data")]
        public string Data { get; set; }

        [DataMember]
        [XmlElement("code")]
        public string Code { get; set; }

        [DataMember]
        [XmlElement("request_params")]
        public DescribeRequestParams RequestParams { get; set; }

        [DataContract, Serializable]
        public sealed class DescribeRequestParams
        {

            [DataMember]
            [XmlElement("show_all")]
            public bool ShowAll { get; set; }
        }
        
    }
 
}
