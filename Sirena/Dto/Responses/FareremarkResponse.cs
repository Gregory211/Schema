using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//OK
namespace Sirena
{
    [DataContract, Serializable()]
    [XmlRoot("sirena")]
    public sealed class FareremarkResponse : DtoResponse
    {
        [DataMember]
        [XmlElement("answer")]
        public FareremarkAnswer Answer { get; set; }
    }

    [DataContract, Serializable()]
    public sealed class FareremarkAnswer
    {
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
        [XmlElement("fareremark")]
        public FareremarkAnswerBody Body { get; set; }
    }

    [DataContract, Serializable()]
    public sealed class FareremarkAnswerBody
    {
        [DataMember]
        [XmlElement("remark")]
        public FareremarkRemark Remark { get; set; }

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

    [DataContract, Serializable()]
    public sealed class FareremarkRemark
    {
        [XmlIgnore]
        public Boolean NewFare { get; set; }

        [DataMember]
        [XmlAttribute("new_fare")]
        public String ProxyNewFare
        {
            get
            {
                return NewFare.ToString().ToLower();
            }
            set
            {
                NewFare = (String.IsNullOrEmpty(value)) ? false : Boolean.Parse(value);
            }
        }

        [DataMember]
        [XmlText]
        public String Text { get; set; }
    }
}
