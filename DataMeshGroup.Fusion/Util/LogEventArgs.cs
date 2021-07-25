using System;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Log Event object. Sent when a logging event occurs
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs()
        {
            Data = "";
            LogLevel = LogLevel.None;
            Exception = null;
            CreatedDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Defines the type of log event
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// The UTC DateTime the log event was generated
        /// </summary>
        public DateTime CreatedDateTime { get; private set; }

        /// <summary>
        /// The content which has been logged
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// May be populated when when LogLevel is Error or Critical
        /// </summary>
        public Exception Exception { get; set; }
    }
}
