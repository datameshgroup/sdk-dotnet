using System;
using System.Runtime.Serialization;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Exception which occurs when errors are encountered on the underlying network layer
    /// </summary>
    [Serializable]
    public class TimeoutException : FusionException
    {
        public TimeoutException()
        {
        }

        public TimeoutException(string message) : base(message)
        {
        }

        public TimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TimeoutException(string message, bool errorRecoveryRequired) : base(message)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        public TimeoutException(string message, bool errorRecoveryRequired, Exception innerException) : base(message, innerException)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        protected TimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}