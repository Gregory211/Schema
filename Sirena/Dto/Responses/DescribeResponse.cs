using System;
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
    public class DescribeResponse : DtoResponse
    {
        public DescribeResponse() : base() { }

        /// <summary>
        /// Gets the answer object.
        /// </summary>
        [DataMember]
        [XmlElement("answer")]
        public DescribeAnswer Answer { get; set; }

    }

    [DataContract, Serializable]
    public sealed class DescribeAnswer
    {
        public DescribeAnswer() { }

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
        [XmlElement("describe")]
        public DescribeAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class DescribeAnswerBody
    {
        public DescribeAnswerBody() { }

        [DataMember]
        [XmlAttribute("results")]
        public string Results { get; set; }

        [DataMember]
        [XmlElement("code")]
        public Code[] Code { get; set; }

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

        [DataMember]
        [XmlElement("name")]
        public Name Name { get; set; }
    }

    public sealed class Code
    {
        [DataMember]
        [XmlAttribute("lang")]
        public string Lang { get; set; }
    }

    public sealed class Name
    {
        [DataMember]
        [XmlAttribute("lang")]
        public string Lang { get; set; }
    }

    

}
