using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class TransactionConditions
    {
        public List<string> AllowedPaymentBrand { get; set; }
        public List<string> AcquirerID { get; set; }
        public bool? DebitPreferredFlag { get; set; }
        //public string AllowedLoyaltyBrand { get; set; }
        public string LoyaltyHandling { get; set; }
        public string CustomerLanguage { get; set; }
        public bool? ForceOnlineFlag { get; set; }
        public List<EntryMode> ForceEntryMode { get; set; }
        public string MerchantCategoryCode { get; set; }
    }
}
