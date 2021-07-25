namespace DataMeshGroup.Fusion.Model
{
    public class SecurityTrailer
    {
        public string ContentType { get; set; }
        public AuthenticatedData AuthenticatedData { get; set; }
    }

    public class AuthenticatedData
    {
        public string Version { get; set; }
        public Recipient Recipient { get; set; }
    }

    public class Recipient
    {
        public KEK KEK { get; set; }
        public MACAlgorithm MACAlgorithm { get; set; }
        public EncapsulatedContent EncapsulatedContent { get; set; }
        public string MAC { get; set; }
    }

    public class KEK
    {
        public string Version { get; set; }
        public KEKIdentifier KEKIdentifier { get; set; }
        public KeyEncryptionAlgorithm KeyEncryptionAlgorithm { get; set; }
        public string EncryptedKey { get; set; }
    }

    public class KEKIdentifier
    {
        public string KeyIdentifier { get; set; }
        public string KeyVersion { get; set; }
    }

    public class MACAlgorithm
    {
        public string Algorithm { get; set; }
    }

    public class EncapsulatedContent
    {
        public string ContentType { get; set; }
    }

    public class KeyEncryptionAlgorithm
    {
        public string Algorithm { get; set; }
    }
}