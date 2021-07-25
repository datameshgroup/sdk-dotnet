namespace DataMeshGroup.Fusion.Model
{
    public class PaymentReceipt
    {
        public string DocumentQualifier { get; set; }
        public bool? IntegratedPrintFlag { get; set; }
        public bool? RequiredSignatureFlag { get; set; }
        public OutputContent OutputContent { get; set; }
    }
}
