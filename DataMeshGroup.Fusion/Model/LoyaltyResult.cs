namespace DataMeshGroup.Fusion.Model
{
    public class LoyaltyResult
    {
        public LoyaltyAccount LoyaltyAccount { get; set; }
        public string CurrentBalance { get; set; }
        public LoyaltyAmount LoyaltyAmount { get; set; }
        public LoyaltyAcquirerData LoyaltyAcquirerData { get; set; }
        public Rebates Rebates { get; set; }
    }
}
