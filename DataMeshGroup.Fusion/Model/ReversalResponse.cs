using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Conveys Information related to the Payment transaction processed by the POI System
    /// </summary>
    public class ReversalResponse : MessagePayload
    {
        public ReversalResponse() :
            base(MessageClass.Service, MessageCategory.Reversal, MessageType.Response)
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
        /// Data related to the POI System.
        /// </summary>
        public POIData POIData { get; set; }

        /// <summary>
        /// Mirrored from the request
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ReversedAmount { get; set; }

        /// <summary>
        /// Customer or Merchant payment receipt.
        /// </summary>
        // public List<PaymentReceipt> PaymentReceipt { get; set; }

        /// <summary>
        /// Get a plain text version of the specified receipt type
        /// </summary>
        /// <returns>
        /// A plain text version of the receipt specified, or null if no such receipt exists
        /// </returns>
        //public string GetReceiptAsPlainText(DocumentQualifier documentQualifier = DocumentQualifier.SaleReceipt)
        //{
        //    return PaymentReceipt?.FirstOrDefault(r => r.DocumentQualifier == documentQualifier)?.OutputContent?.GetContentAsPlainText();
        //}

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
