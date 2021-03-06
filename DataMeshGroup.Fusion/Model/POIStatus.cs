namespace DataMeshGroup.Fusion.Model
{
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
