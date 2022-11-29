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


        /// <summary>
        /// Helper function to create <see cref="SaleItem"/> objects add sale items and modifiers to the payment, this function will attempt to create values where possible.
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="productLabel"></param>
        /// <param name="itemAmount"></param>
        /// <param name="quantity"></param>
        /// <param name="unitOfMeasure"></param>
        /// <param name="itemID"></param>
        /// <param name="parentItemID"></param>
        /// <param name="eanUpc"></param>
        /// <param name="additionalProductInfo"></param>
        /// <param name="unitPrice"></param>
        /// <param name="taxCode"></param>
        /// <param name="saleChannel"></param>
        /// <param name="costBase"></param>
        /// <param name="discount"></param>
        /// <param name="discountReason"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        /// <param name="categories"></param>
        /// <param name="brand"></param>
        /// <param name="quantityInStock"></param>
        /// <param name="restricted"></param>
        /// <param name="pageURL"></param>
        /// <param name="imageURLs"></param>
        /// <param name="style"></param>
        /// <param name="size"></param>
        /// <param name="colour"></param>
        /// <param name="weight"></param>
        /// <param name="weightUnitOfMeasure"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
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
                string discountReason = null,
                string category = null,
                string subCategory = null,
                List<string> categories = null,
                string brand = null,
                decimal? quantityInStock = null,
                bool? restricted = null,
                string pageURL = null,
                List<string> imageURLs = null,
                string style = null,
                string size = null,
                string colour = null,
                decimal? weight = null,
                WeightUnitOfMeasure? weightUnitOfMeasure = null,
                List<string> tags = null
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
                DiscountReason = discountReason,
                Brand = brand,
                QuantityInStock = quantityInStock,
                Restricted = restricted,
                PageURL = pageURL,
                ImageURLs = imageURLs,
                Style = style,
                Size = size,
                Colour = colour,
                Weight = weight,
                WeightUnitOfMeasure = weightUnitOfMeasure,
                Tags = tags
            };

            if(category != null || subCategory != null)
            {
                saleItem.Category = category;
                saleItem.SubCategory = subCategory;
            }
            else
            {
                saleItem.Categories = categories;
            }


            PaymentTransaction.SaleItem.Add(saleItem);
            return saleItem;
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new PaymentResponse()
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                },

                PaymentResult = new PaymentResult()
                {
                    PaymentType = this.PaymentData.PaymentType,
                    PaymentInstrumentData = new PaymentInstrumentData()
                    {
                        PaymentInstrumentType = PaymentInstrumentType.Card,
                        CardData = new CardData()
                        {
                            PaymentBrand = "Card",
                            EntryMode = EntryMode.Contactless
                        }
                    }
                },

                SaleData = SaleData
            };
        }
    }
}