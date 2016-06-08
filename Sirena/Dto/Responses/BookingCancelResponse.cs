using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirena.Dto.Responses
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class BookingCancelResponse : DtoResponse
    {
        public BookingCancelResponse() : base() {}

        [DataMember]
        [XmlElement("answer")]
        public BookingCancelAnswer Answer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingCancelAnswer
    {
        /// <summary>
        /// Gets the pult name.
        /// </summary>
        [DataMember]
        [XmlAttribute("pult")]
        public string Pult { get; set; }

        /// <summary>
        /// Gets the message id.
        /// </summary>
        [DataMember]
        [XmlAttribute("msgid")]
        public string MessageId { get; set; }

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
        public string ProxyTime
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

        [DataMember]
        [XmlElement("booking-cancel")]
        public BookingCancelAnswerBody BookingCancel { get; set; }
    }

    [DataContract, Serializable]
    public sealed class BookingCancelAnswerBody
    {
        [DataMember]
        [XmlElement("ok")]
        public string Ok { get; set; }
    }
}
