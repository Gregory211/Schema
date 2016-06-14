using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sirena.Dto.Responses
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public class GetItinReceiptsResponse : DtoResponse
    {
        public GetItinReceiptsResponse() : base() { }
        [DataMember]
        [XmlElement("answer")]
        public GetItinReceiptsAnswer Answer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetItinReceiptsAnswer
    {
        public GetItinReceiptsAnswer() : base() { }

        [DataMember]
        [XmlAttribute("pult")]
        public String Pult { get; set; }

        [DataMember]
        [XmlAttribute("msgid")]
        public String MessageId { get; set; }

        [XmlIgnore]
        public DateTime? Time { get; set; }

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

        /// <summary>
        /// Gets the answer body.
        /// </summary>
        [DataMember]
        [XmlElement("get_itin_receipts")]
        public GetItinReceiptsAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class GetItinReceiptsAnswerBody
    {
        public GetItinReceiptsAnswerBody() { }
       
        [DataMember]
        [XmlElement("error")]
        public Error Error { get; set; }

        [XmlIgnore]
        public DateTime? Date { get; set; }

        [DataMember]
        [XmlAttribute("cr_time")]
        public string ProxyDate
        {
            get
            {
                return (Date.HasValue) ? Date.Value.ToString("HH:mm:ss dd.MM.yy") : null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Date = DateTime.ParseExact(value, "HH:mm:ss dd.MM.yy", null);
                }
            }
        }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }
}
