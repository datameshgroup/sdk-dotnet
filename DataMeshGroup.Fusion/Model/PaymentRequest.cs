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
        public PaymentRequest(string transactionID, decimal requestedAmount, List<SaleItem> saleItems = null, PaymentType paymentType = PaymentType.Normal) :
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
                SaleItem = saleItems ?? new List<SaleItem>()
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

        public SaleItem AddSaleItem(
                string productCode,
                string productLabel,
                decimal itemAmount = 0M,
                decimal quantity = 1,
                UnitOfMeasure unitOfMeasure = UnitOfMeasure.Other,
                int? itemID = null,
                int? parentItemID = null,
                string eanUpc = null,
                string additionalProductInfo = null,
                decimal? unitPrice = null,
                string taxCode = null,
                string saleChannel = null,
                decimal? costBase = null,
                decimal? discount = null,
                string category = null,
                string subCategory = null,
                string brand = null,
                int? quantityInStock = null
            )
        {
            if (PaymentTransaction == null)
            {
                throw new System.InvalidOperationException("Unable to access SaleItem array as PaymentTransaction == null");
            }

            if (PaymentTransaction.SaleItem == null)
            {
                PaymentTransaction.SaleItem = new List<SaleItem>();
            }

            if (!itemID.HasValue)
            {
                itemID = PaymentTransaction.SaleItem.Count;
            }

            if (!unitPrice.HasValue)
            {
                unitPrice = itemAmount;
            }

            var saleItem = new SaleItem()
            {
                ItemID = itemID.Value,
                ParentItemID = parentItemID,
                ProductCode = productCode,
                ProductLabel = productLabel,
                ItemAmount = itemAmount,
                Quantity = quantity,
                UnitOfMeasure = unitOfMeasure,
                EanUpc = eanUpc,
                AdditionalProductInfo = additionalProductInfo,
                UnitPrice = unitPrice.Value,
                TaxCode = taxCode,
                SaleChannel = saleChannel,
                CostBase = costBase,
                Discount = discount,
                Category = category,
                SubCategory = subCategory,
                Brand = brand,
                QuantityInStock = quantityInStock
            };

            PaymentTransaction.SaleItem.Add(saleItem);
            return saleItem;
        }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new PaymentResponse()
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                },

                SaleData = SaleData
            };
        }
    }
}