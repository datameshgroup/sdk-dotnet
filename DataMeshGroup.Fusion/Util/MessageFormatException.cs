﻿using System;
using System.Runtime.Serialization;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Exception which occurs when Fusion is unable to pack/unpack a request 
    /// </summary>
    [Serializable]
    public class MessageFormatException : FusionException
    {
        public string InvalidMessage { get; set; }

        public MessageFormatException()
        {
        }

        public MessageFormatException(string message) : base(message)
        {
        }

        public MessageFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MessageFormatException(string message, bool errorRecoveryRequired) : base(message)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        public MessageFormatException(string message, bool errorRecoveryRequired, Exception innerException) : base(message, innerException)
        {
            ErrorRecoveryRequired = errorRecoveryRequired;
        }

        protected MessageFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}