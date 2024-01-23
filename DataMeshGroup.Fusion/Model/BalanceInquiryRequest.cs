using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class BalanceInquiryRequest : MessagePayload
    {
        public BalanceInquiryRequest():
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
        { }

        public BalanceInquiryRequest(PaymentAccountReq paymentAccountReq):
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
        {
            PaymentAccountReq = paymentAccountReq;
        }

        public BalanceInquiryRequest(LoyaltyAccountReq loyaltyAccountReq):
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
        {
            LoyaltyAccountReq = loyaltyAccountReq;
        }

        public BalanceInquiryRequest(StoredValueAccountID storedValueAccountID):
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
        {
            PaymentAccountReq = new PaymentAccountReq(storedValueAccountID);            
        }

        public BalanceInquiryRequest(CardData cardData):
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
        {
            PaymentAccountReq = new PaymentAccountReq(cardData);
        }

        public BalanceInquiryRequest(LoyaltyAccountID loyaltyAccountID):
            base(MessageClass.Service, MessageCategory.BalanceInquiry, MessageType.Request)
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
