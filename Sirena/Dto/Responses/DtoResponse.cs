using System;
using System.Runtime.Serialization;

namespace Sirena
{
    /// <summary>
    /// The base class for all dto response classes.
    /// </summary>
    [DataContract, Serializable]
    public abstract class DtoResponse
    {
        /// <summary>
        /// Initializes an instance of the DtoResponse class.
        /// </summary>
        public DtoResponse() { }
    }
}
