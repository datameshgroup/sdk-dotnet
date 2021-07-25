using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentTransaction
    {
        public AmountsReq AmountsReq { get; set; }
        
        public OriginalPOITransaction OriginalPOITransaction { get; set; }
        
        public TransactionConditions TransactionConditions { get; set; }
        
        public List<SaleItem> SaleItem { get; set; }
    }
}
