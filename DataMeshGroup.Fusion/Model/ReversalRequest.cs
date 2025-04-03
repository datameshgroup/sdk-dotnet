using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class ReversalRequest : MessagePayload
    {
        /// <summary>
        /// Create a ReversalRequest with default parameters
        /// </summary>
        public ReversalRequest() :
            base(MessageClass.Service, MessageCategory.Reversal, MessageType.Request)
        {
        }

        /// <summary>
        /// Constructs a <see cref="ReversalRequest"/> with required parameters
        /// </summary>
        /// <param name="reversalReason"></param>
        /// <param name="poiTransactionID"></param>
        public ReversalRequest(ReversalReason reversalReason, TransactionIdentification poiTransactionID) :
            base(MessageClass.Service, MessageCategory.Payment, MessageType.Request)
        {
            this.ReversalReason = reversalReason;
            this.OriginalPOITransaction = new OriginalPOITransaction() { POITransactionID = poiTransactionID };
        }
        
        /// <summary>
        /// Optional sale data 
        /// </summary>
        public SaleData SaleData { get; set; }

        /// <summary>
        /// Optional payment data
        /// </summary>
        public PaymentData PaymentData { get; set; }

        /// <summary>
        /// Identifies the transaction to be reversed
        /// </summary>
        public OriginalPOITransaction OriginalPOITransaction { get; set; }
        
        /// <summary>
        /// Text reason of why the transaction is being reversed. e.g. "Signature Declined"
        /// </summary>
        public ReversalReason ReversalReason { get; set; }

        /// <summary>
        /// Indicates the amount to be reversed. Must be lower or equal AuthorizedAmount of the OriginalPOITransaction.
        /// A null values implies the full AuthorizedAmount should be reversed.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ReversedAmount { get; set; }

//        public CustomerOrder CustomerOrder { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new ReversalResponse()
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                },

                //PaymentReceipt = new List<PaymentReceipt>(),
                ReversedAmount = this.ReversedAmount
            };
        }
    }
}