﻿using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirena
{
    [DataContract, Serializable]
    public sealed class Error
    {
        public Error() { }

        [DataMember]
        [XmlAttribute("code")]
        public Int32 Code { get; set; }

        [DataMember]
        [XmlText]
        public String Text { get; set; }

        public override String ToString()
        {
            return String.Format("Error code: {0}, Error message: {1}", Code, Text);
        }
    }
}
