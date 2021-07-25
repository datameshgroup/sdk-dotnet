namespace DataMeshGroup.Fusion.Model
{
    public class OutputContent
    {
        public OutputFormat OutputFormat { get; set; } = OutputFormat.XHTML;
        //public PredefinedContent PredefinedContent { get; set; }
        //public OutputText OutputText { get; set; }

        /// <summary>
        /// The payment receipt in XHTML format BASE64 encoded
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Base64JsonConverter))]
        public string OutputXHTML { get; set; }
        //public OutputBarcode OutputBarcode { get; set; }
    }
}
