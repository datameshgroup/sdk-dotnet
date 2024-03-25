using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Conveys Information related to the Payment transaction processed by the POI System
    /// </summary>
    public class PaymentResponse: MessagePayload
    {
        public PaymentResponse() :
            base(MessageClass.Service, MessageCategory.Payment, MessageType.Response)
        {
        }

        /// <summary>
        /// Result of a message request processing.
        /// If Result is Success, ErrorCondition is absent or not used in the processing of the message. 
        /// In the other cases, the ErrorCondition has to be present and can refine the processing of the message response.
        /// AdditionalResponse gives more information about the success or the failure of the message request processing
        /// </summary>
        public Response Response { get; set; }

        /// <summary>
        /// Data related to the Sale System.
        /// Data associated to the Sale System, with a particular value during the processing of the payment by the POI, including the cards acquisition.
        /// </summary>
        public SaleData SaleData { get; set; }

        /// <summary>
        /// Data related to the POI System.
        /// </summary>
        public POIData POIData { get; set; }

        /// <summary>
        /// Data related to the result of a processed payment transaction.
        /// </summary>
        public PaymentResult PaymentResult { get; set; }

        /// <summary>
        /// Data related to the result of a processed loyalty transaction.
        /// </summary>
        public List<LoyaltyResult> LoyaltyResult { get; set; }

        /// <summary>
        /// Customer or Merchant payment receipt.
        /// </summary>
        public List<PaymentReceipt> PaymentReceipt { get; set; }

        /// <summary>
        /// Customer order attached to a customer, recorded in the POI system.
        /// Allows the management of customer orders by the POI, for instance in a multi-channel or a click and collect sale transaction.
        /// </summary>
        public CustomerOrder CustomerOrder { get; set; }

        /// <summary>
        /// Present if ErrorCondition is "PaymentRestriction". Consists of a list of product codes corresponding to products that are purchasable with the given card. Items that exist in the basket but do not belong to this list corresponds to restricted items.
        /// </summary>
        public List<string> AllowedProductCode { get; set; }

        /// <summary>
        /// Get a plain text version of the specified receipt type
        /// </summary>
        /// <returns>
        /// A plain text version of the receipt specified, or null if no such receipt exists
        /// </returns>
        public string GetReceiptAsPlainText(DocumentQualifier documentQualifier = DocumentQualifier.SaleReceipt)
        {
            PaymentReceipt paymentReceipt = PaymentReceipt?.FirstOrDefault(r => r.DocumentQualifier == documentQualifier)
                ?? PaymentReceipt?.FirstOrDefault(r => r.DocumentQualifier == DocumentQualifier.CustomerReceipt);

            return paymentReceipt?.OutputContent?.GetContentAsPlainText();
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
