using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentResponse: MessagePayload
    {
        public PaymentResponse() :
            base(MessageClass.Service, MessageCategory.Payment, MessageType.Response)
        {
        }

        public Response Response { get; set; }
        public SaleData SaleData { get; set; }
        public POIData POIData { get; set; }
        public PaymentResult PaymentResult { get; set; }
        //public LoyaltyResult LoyaltyResult { get; set; }
        
        public List<PaymentReceipt> PaymentReceipt { get; set; }
        
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
            return PaymentReceipt?.FirstOrDefault(r => r.DocumentQualifier == documentQualifier)?.OutputContent?.GetContentAsPlainText();
        }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
