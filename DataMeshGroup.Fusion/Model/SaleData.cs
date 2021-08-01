using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleData
    {
        public string OperatorID { get; set; }
        public string OperatorLanguage { get; set; } = "en";
        public string ShiftNumber { get; set; }
        public string CustomerLanguage { get; set; } = "en";
        public TransactionIdentification SaleTransactionID { get; set; }
        public string SaleReferenceID { get; set; }
        public SaleTerminalData SaleTerminalData { get; set; }
        public TokenRequestedType? TokenRequestedType { get; set; }
        public string CustomerOrderID { get; set; }
        public List<CustomerOrderReq> CustomerOrderReq { get; set; }
        public string SaleToPOIData { get; set; }
        public string SaleToAcquirerData { get; set; }
        //public SaleToIssuerData SaleToIssuerData { get; set; }
        //public SponsoredMerchant SponsoredMerchant { get; set; }
    }
}
