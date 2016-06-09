using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirena.Dto.Responses
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class PaymentExtAuthConfirmResponse : DtoResponse
    {
        public PaymentExtAuthConfirmResponse() : base() {}

        [DataMember]
        [XmlElement("answer")]
        public PaymentExtAuthConfirmAnswer ConfirmAnswer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthConfirmAnswer
    {
        public PaymentExtAuthConfirmAnswer() {}

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
        public PaymentExtAuthConfirmAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class PaymentExtAuthConfirmAnswerBody
    {
        public PaymentExtAuthConfirmAnswerBody(){}

        [DataMember]
        [XmlElement("receipts")]
        public Receipts Receipts { get; set; }

        [DataMember]
        [XmlElement("regnum")]
        public string Regnum { get; set; }

        [DataMember]
        [XmlElement("timelimit")]
        public string Timelimit { get; set; }

        [DataMember]
        [XmlElement("nseats")]
        public string Nseats { get; set; }

        [DataMember]
        [XmlElement("agn")]
        public string Agn { get; set; }

        [DataMember]
        [XmlElement("ppr")]
        public string Ppr { get; set; }

        [DataMember]
        [XmlElement("nseg")]
        public string Nseg { get; set; }

        [DataMember]
        [XmlElement("tickinfo")]
        public Tickinfo Tickinfo { get; set; }

        [DataMember]
        [XmlElement("ok")]
        public string Ok { get; set; }

        [DataMember]
        [XmlElement("error")]
        public Error Error { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Receipts
    {
        public Receipts() { }

        [DataMember]
        [XmlAttribute("cr_time")]
        public string CrTime { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }

    [DataContract, Serializable]
    public sealed class Tickinfo
    {
        public Tickinfo() { }

        [DataMember]
        [XmlAttribute("seg_id")]
        public string SegId { get; set; }

        [DataMember]
        [XmlAttribute("pass_id")]
        public string PassId { get; set; }

        [DataMember]
        [XmlAttribute("ticknum")]
        public string Ticknum { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }
}
