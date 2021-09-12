namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Information related to the software of the Sale System which manages the Sale to POI protocol.
    /// </summary>
    public class SaleSoftware
    {

        public SaleSoftware()
        {

        }

        /// <summary>
        /// Constructor will params for the required fields for SaleSoftware
        /// </summary>
        public SaleSoftware(string providerIdentification, string applicationName, string softwareVersion, string certificationCode)
        {
            ProviderIdentification = providerIdentification;
            ApplicationName = applicationName;
            SoftwareVersion = softwareVersion;
            CertificationCode = certificationCode;
        }

        /// <summary>
        /// Identification of the provider of the software or POI component
        /// </summary>
        public string ProviderIdentification { get; set; }
        
        /// <summary>
        /// Identifies the name of the sale system
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// Identifies the version of the sale system
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// Certification code of the software which manages the Sale to POI protocol.
        /// </summary>
        public string CertificationCode { get; set; } 

        public string ComponentDescription { get; set; }

        /// <summary>
        /// Sent in the Login Request to describe the type of component.
        /// </summary>
        public ComponentType ComponentType { get; set; }
    }

}