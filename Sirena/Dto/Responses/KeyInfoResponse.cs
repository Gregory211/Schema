using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//OK
namespace Sirena
{
    [DataContract, Serializable]
    [XmlRoot("sirena")]
    public sealed class KeyInfoResponse : DtoResponse
    {
        [DataMember][XmlElement("answer")]
        public KeyAnswer Answer { get; set; }
    }

    [DataContract, Serializable]
    public sealed class KeyAnswer
    {
        [DataMember][XmlAttribute("pult")]
        public String Pult { get; set; }

        [DataMember][XmlElement("key_info")]
        public KeyAnswerBody Body { get; set; }
    }

    [DataContract, Serializable]
    public sealed class KeyAnswerBody
    {
        [DataMember][XmlElement("key_manager")]
        public KeyManager KeyManager { get; set; }

        [DataMember][XmlElement("error")]
        public Error Error { get; set; }

        /// <summary>
        /// Gets the additional info about the response.
        /// </summary>
        [DataMember][XmlElement("info")]
        public Info Info { get; set; }
    }

    [DataContract, Serializable]
    public sealed class KeyManager
    {
        [DataMember][XmlElement("key")]
        public KeyDigest KeyDigest { get; set; }

        [DataMember][XmlElement("expiration")]
        public String Expiration { get; set; }

        [DataMember][XmlElement("unconfirmed")]
        public String Unconfirmed { get; set; }

        [DataMember][XmlElement("server_public_key")]
        public String ServerPublicKey { get; set; }
    }

    [DataContract, Serializable]
    public sealed class KeyDigest
    {
        [DataMember][XmlAttribute("state")]
        public KeyState KeyState { get; set; }

        [DataMember]
        [XmlText]
        public String Text { get; set; }
    }

    [DataContract, Serializable]
    public enum KeyState
    {
        [EnumMember]
        [XmlEnum("current")]
        Current,

        [EnumMember]
        [XmlEnum("future")]
        Future,
    }
}
