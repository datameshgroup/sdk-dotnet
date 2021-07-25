using System;
using System.Runtime.Serialization;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Exception which occurs when errors are encountered on the underlying network layer
    /// </summary>
    [Serializable]
    public class NetworkException : FusionException
    {
        public NetworkException()
        {
        }

        public NetworkException(string message) : base(message)
        {
        }

        public NetworkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NetworkException(string message, bool errorRecoveryRequired) : base(message)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        public NetworkException(string message, bool errorRecoveryRequired, Exception innerException) : base(message, innerException)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        protected NetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}