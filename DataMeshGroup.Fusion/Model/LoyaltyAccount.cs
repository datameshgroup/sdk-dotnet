namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data related to a loyalty account processed in the transaction.
    /// </summary>
    public class LoyaltyAccount
    {

        /// <summary>
        /// Identification of a Loyalty account.
        /// </summary>
        public LoyaltyAccountID LoyaltyAccountID { get; set; }

        /// <summary>
        /// Identification of a Loyalty brand
        /// </summary>
        public LoyaltyBrand LoyaltyBrand { get; set; }
    }

}
