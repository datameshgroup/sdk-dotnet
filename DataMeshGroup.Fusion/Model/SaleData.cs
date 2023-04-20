using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data associated to the Sale System, with a particular value during the processing of the payment by the POI, including the cards acquisition.
    /// </summary>
    public class SaleData
    {
        /// <summary>
        /// Identification of the Cashier or Operator.
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// Language of the Cashier or Operator. ISO 639-1 format.
        /// </summary>
        public string OperatorLanguage { get; set; }

        /// <summary>
        /// Shift number. Identifies the shift that drives the Sale Terminal during the session
        /// </summary>
        public string ShiftNumber { get; set; }

        /// <summary>
        /// Language of the card holder. ISO 639-1 format.
        /// </summary>
        public string CustomerLanguage { get; set; }

        /// <summary>
        /// Unique identification of a Sale transaction. To identify the transaction on the Sale Terminal, e.g. ticket number.
        /// </summary>
        public TransactionIdentification SaleTransactionID { get; set; }

        /// <summary>
        /// Globally unique identification for a sequence of related POI transactions
        /// </summary>
        public string SaleReferenceID { get; set; }

        /// <summary>
        /// Information related to the software and hardware feature of the Sale Terminal.
        /// </summary>
        public SaleTerminalData SaleTerminalData { get; set; }

        /// <summary>
        /// Type of token replacing the PAN of a payment card to identify the payment mean of the customer. In a Payment or CardAcquisition request, if a token is requested.
        /// </summary>
        public TokenRequestedType? TokenRequestedType { get; set; }

        /// <summary>
        /// Additional and optional identification of a customer order. Set if the payment is related to an open customer order.
        /// </summary>
        public string CustomerOrderID { get; set; }

        /// <summary>
        /// Indicates if a list of customer orders must be sent in response message.
        /// </summary>
        public List<CustomerOrderReq> CustomerOrderReq { get; set; }

        /// <summary>
        /// Sale information intended for the POI. The POI System receives this information which is meaningful only for the Sale System. The POI stores this information with the transaction.
        /// </summary>
        public string SaleToPOIData { get; set; }

        /// <summary>
        /// Sale information intended for the Acquirer. The POI System receives this information and sends it to the Acquirer without any change.
        /// </summary>
        public string SaleToAcquirerData { get; set; }

        /// <summary>
        /// Sale information intended for the Issuer. The POI System receives this information and sends it to the Acquirer for the Issuer without any change.
        /// </summary>
        public SaleToIssuerData SaleToIssuerData { get; set; }

        /// <summary>
        /// Merchant performing the transaction.
        /// </summary>
        public SponsoredMerchant SponsoredMerchant { get; set; }
    }
}
