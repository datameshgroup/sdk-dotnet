namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Identification of a Loyalty account.
    /// </summary>
    public class LoyaltyAccountID
    {
        /// <summary>
        /// Entry mode of the loyalty account information
        /// </summary>
        public EntryMode EntryMode { get; set; }

        /// <summary>
        /// Type of account identification. Informs the Sale System the type of the account or card identification.
        /// </summary>
        public IdentificationType IdentificationType { get; set; }

        /// <summary>
        /// Support of the loyalty account identification. Allows knowing where and how you have found the loyalty account identification.
        /// </summary>
        public IdentificationSupport IdentificationSupport { get; set; }


        /// <summary>
        /// Loyalty account identification. Contains the identification of the loyalty account conforming to the IdentificationType.
        /// </summary>
        public string LoyaltyID { get; set; }
    }
}
