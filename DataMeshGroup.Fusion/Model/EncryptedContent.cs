namespace DataMeshGroup.Fusion.Model
{
    public class EncryptedContent
    {
        public string ContentType { get; set; }
        public ContentEncryptionAlgorithm ContentEncryptionAlgorithm { get; set; }
        public string EncryptedData { get; set; }
    }
}
