namespace DataMeshGroup.Fusion.Model
{
    public class PrintOutput
    {
        /// <summary>
        /// Qualification of the document to print to the Cashier or the Customer.
        /// </summary>
        public DocumentQualifier DocumentQualifier { get; set; }

        /// <summary>
        /// Indicates the <see cref="PrintResponse"/> type required for this PrintRequest
        /// Default is NotRequired
        /// </summary>
        public ResponseMode ResponseMode { get; set; }

        /// <summary>
        /// Indicates if the receipt should be included in the sale result. Default == false
        /// </summary>
        public bool? IntegratedPrintFlag { get; set; }

        /// <summary>
        /// True if a signature is required for the receipt. Default == false
        /// </summary>
        public bool? RequiredSignatureFlag { get; set; }

        /// <summary>
        /// Content of the receipt
        /// </summary>
        public OutputContent OutputContent { get; set; }
    }
}
