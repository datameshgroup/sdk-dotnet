using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class BalanceInquiryRequest : MessagePayload
    {
        public BalanceInquiryRequest() { }

        public BalanceInquiryRequest(PaymentAccountReq paymentAccountReq)
        {
            PaymentAccountReq = paymentAccountReq;
        }

        public BalanceInquiryRequest(LoyaltyAccountReq loyaltyAccountReq)
        {
            LoyaltyAccountReq = loyaltyAccountReq;
        }

        public BalanceInquiryRequest(StoredValueAccountID storedValueAccountID)
        {
            PaymentAccountReq = new PaymentAccountReq(storedValueAccountID);            
        }

        public BalanceInquiryRequest(CardData cardData)
        {
            PaymentAccountReq = new PaymentAccountReq(cardData);
        }

        public BalanceInquiryRequest(LoyaltyAccountID loyaltyAccountID)
        {
            LoyaltyAccountReq = new LoyaltyAccountReq(loyaltyAccountID);
        }

        /// <summary>
        /// Data related to the account pointed by the payment card
        /// </summary>
        public PaymentAccountReq PaymentAccountReq { get; set; }

        /// <summary>
        /// Data related to a requested Loyalty program or account
        /// </summary>
        public LoyaltyAccountReq LoyaltyAccountReq { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new BalanceInquiryResponse()
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }
    }
}
