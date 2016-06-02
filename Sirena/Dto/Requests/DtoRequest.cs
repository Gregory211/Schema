using System;
using System.Runtime.Serialization;

namespace Sirena
{
    /// <summary>
    /// The base class for all dto request classes.
    /// </summary>
    [DataContract, Serializable]
    public abstract class DtoRequest
    {
        /// <summary>
        /// Initializes an instance of the DtoRequest class.
        /// </summary>
        public DtoRequest() { }
    }
}
