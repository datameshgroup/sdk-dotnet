using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentRequest : MessagePayload
    {
        public PaymentRequest() :
            base(MessageClass.Service, MessageCategory.Payment, MessageType.Request)
        {
        }

        /// <summary>
        /// Create a PaymentRequest with default parameters
        /// </summary>
        /// <param name="transactionID">Unique reference for this sale ticket. Written to SaleData.SaleTransactionID.TransactionID</param>
        /// <param name="requestedAmount">The requested amount for the transaction sale items, including cash back and tip requested</param>
        /// <param name="paymentType">Defaults to "Normal". Indicates the type of payment to process. "Normal", "Refund", or "CashAdvance"</param>
        public PaymentRequest(string transactionID, decimal requestedAmount, List<SaleItem> saleItems, PaymentType paymentType = PaymentType.Normal) :
            base(MessageClass.Service, MessageCategory.Payment, MessageType.Request)
        {
            SaleData = new SaleData()
            {
                SaleTransactionID = new TransactionIdentification(transactionID)
            };
            PaymentTransaction = new PaymentTransaction()
            {
                AmountsReq = new AmountsReq()
                {
                    RequestedAmount = requestedAmount
                },
                SaleItem = saleItems
            };
            PaymentData = new PaymentData()
            {
                PaymentType = paymentType
            };
        }


        public SaleData SaleData { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
        public PaymentData PaymentData { get; set; }
        //public LoyaltyData LoyaltyData { get; set; }

    }
}