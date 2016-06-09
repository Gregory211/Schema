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
    public sealed class PaymentExtAuthQueryResponse : DtoResponse
    {
        public PaymentExtAuthQueryResponse() : base() { }

        [DataMember]
        [XmlElement("answer")]
        public PaymentExtAuthQueryAnswer ConfirmAnswer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQueryAnswer
    {
        public PaymentExtAuthQueryAnswer() { }

        [DataMember]
        [XmlAttribute("pult")]
        public string Pult { get; set; }

        [DataMember]
        [XmlAttribute("msgid")]
        public string MessageId { get; set; }

        [XmlIgnore]
        public DateTime? Time { get; set; }

        [DataMember]
        [XmlAttribute("time")]
        public string ProxyTime
        {
            get { return (Time.HasValue) ? Time.Value.ToString("HH:mm:ss dd.MM.yyyy") : null; }
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
        [XmlElement("payment-ext-auth")]
        public PaymentExtAuthQueryAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthQueryAnswerBody
    {
        public PaymentExtAuthQueryAnswerBody() { }

        [DataMember]
        [XmlAttribute("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlAttribute("action")]
        public string Action { get; set; }

        [DataMember]
        [XmlAttribute("mode")]
        public string Mode { get; set; }

        [DataMember]
        [XmlElement("tick_ser")]
        public string TickSer { get; set; }

        [DataMember]
        [XmlElement("timeout")]
        public Timeout Timeout { get; set; }

        [DataMember]
        [XmlElement("cost")]
        public Cost Cost { get; set; }


        [DataMember]
        [XmlElement("error")]
        public Error Error { get; set; }
    }
}
