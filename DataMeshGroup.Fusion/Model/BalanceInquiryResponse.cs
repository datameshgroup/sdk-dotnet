using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class BalanceInquiryResponse : MessagePayload
    {
        public BalanceInquiryResponse() :
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Response)
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
        /// Data related to the result of a Balance Inquiry request
        /// </summary>
        public PaymentAccountStatus PaymentAccountStatus { get; set; }

        /// <summary>
        /// Data related to the result of a loyalty Balance Inquiry
        /// </summary>
        public LoyaltyAccountStatus LoyaltyAccountStatus { get; set; }

        /// <summary>
        /// Customer or Merchant receipt.
        /// </summary>
        public List<PaymentReceipt> PaymentReceipt { get; set; }

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

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }

    }
}
