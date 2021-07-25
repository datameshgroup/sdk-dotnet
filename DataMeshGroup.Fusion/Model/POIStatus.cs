namespace DataMeshGroup.Fusion.Model
{
    public class POIStatus
    {
        public string GlobalStatus { get; set; }
        public bool? SecurityOKFlag { get; set; }
        public bool? PEDOKFlag { get; set; }
        public bool? CardReaderOKFlag { get; set; }
        public string PrinterStatus { get; set; }
        public bool? CommunicationOKFlag { get; set; }
        public CashHandlingDevice CashHandlingDevice { get; set; }
        public bool? FraudPreventionFlag { get; set; }
    }
}
