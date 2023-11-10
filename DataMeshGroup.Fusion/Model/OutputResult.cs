namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Information related to the result the output (display, print, input).
    /// 
    /// In the message response, it contains the result of the output, if required in the message request.
    /// </summary>
    public class OutputResult
    {
        /// <summary>
        /// Mirrored from request
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// Mirrored from request
        /// </summary>
        public InfoQualify InfoQualify { get; set; }

        public Response Response { get; set; }
    }
}
