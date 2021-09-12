using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class CardAcquisitionTransaction
    {
        public List<PaymentBrand> AllowedPaymentBrand { get; set; }
        public ForceEntryMode ForceEntryMode { get; set; }
    }

}
