using System;
using System.Runtime.Serialization;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Exception which occurs when errors are encountered on the underlying network layer
    /// </summary>
    [Serializable]
    public class FusionException : Exception
    {
        /// <summary>
        /// Set to 'true' if the calling app needs to enter error recovery as a result of this exception
        /// </summary>
        public bool ErrorRecoveryRequired { get; set; }

        /// <summary>
        /// Text description of the error. Can be used to set AbortRequest.AbortReason during error recovery.
        /// </summary>
        public virtual string ErrorReason => "Other Exception";

        public FusionException()
        {
        }

        public FusionException(string message) : base(message)
        {
        }

        public FusionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FusionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}