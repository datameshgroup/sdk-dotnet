namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// State of a host. Indicates the reachability of the host by the POI Terminal.
    /// </summary>
    public class HostStatus
    {
        /// <summary>
        /// The acquirer ID of the host
        /// </summary>
        public string AcquirerID { get; set; }
        
        /// <summary>
        /// Indicates if the host is reachable
        /// </summary>
        public bool IsReachableFlag { get; set; }
    }
}
