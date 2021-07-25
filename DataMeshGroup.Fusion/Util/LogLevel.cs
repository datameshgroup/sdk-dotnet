using System;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Indicates the severity of a log message.Log Levels are ordered in increasing severity.So Debug is more severe than Trace, etc.
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// Log level for very low severity diagnostic messages.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Log level for low severity diagnostic messages.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Log level for informational diagnostic messages.
        /// </summary>
        Information = 2,

        /// <summary>
        /// Log level for diagnostic messages that indicate a non-fatal problem.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Log level for diagnostic messages that indicate a failure in the current operation.
        /// </summary>
        Error = 4,

        /// <summary>
        /// Log level for diagnostic messages that indicate a failure that will terminate the entire application.
        /// </summary>
        Critical = 5,

        /// <summary>
        /// The highest possible log level.Used when configuring logging to indicate that no log messages should be emitted.
        /// </summary>
        None = 6
    }
}
