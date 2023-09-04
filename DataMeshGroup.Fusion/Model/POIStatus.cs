namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// State of a POI Terminal.
    /// Indicate the availability of the POI Terminal components.The data element is absent if the component is not part of the POI Terminal.
    /// </summary>
    public class POIStatus
    {
        public GlobalStatus GlobalStatus { get; set; }
        public bool? SecurityOKFlag { get; set; }
        public bool? PEDOKFlag { get; set; }
        public bool? CardReaderOKFlag { get; set; }
        public PrinterStatus PrinterStatus { get; set; }
        public bool? CommunicationOKFlag { get; set; }
        public CashHandlingDevice CashHandlingDevice { get; set; }
        public bool? FraudPreventionFlag { get; set; }
    }
}
