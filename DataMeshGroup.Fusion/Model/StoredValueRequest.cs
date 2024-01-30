using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueRequest : MessagePayload
    {
        public StoredValueRequest() :
            base(MessageClass.Service, MessageCategory.StoredValue, MessageType.Request)
        {
            StoredValueData = new List<StoredValueData>();
        }

        /// <summary>
        /// Create a StoredValueRequest with default parameters
        /// </summary>
        /// <param name="transactionID">Unique reference for this sale ticket. Written to SaleData.SaleTransactionID.TransactionID</param>        
        /// <param name="storedValueData">Data related to the stored value card.</param>
        public StoredValueRequest(string transactionID, List<StoredValueData> storedValueData = null) :
            base(MessageClass.Service, MessageCategory.StoredValue, MessageType.Request)
        {
            SaleData = new SaleData()
            {
                SaleTransactionID = new TransactionIdentification(transactionID)
            };
            StoredValueData = storedValueData ?? new List<StoredValueData>();            
        }

        /// <summary>
        /// Sale System information attached to this stored value
        /// </summary>
        public SaleData SaleData { get; set; }

        /// <summary>
        /// Data related to the stored value card.
        /// </summary>
        public List<StoredValueData> StoredValueData { get; set; }

        /// <summary>
        /// Returns the first item in StoredValueData
        /// </summary>
        [JsonIgnore]
        public StoredValueData StoredValueDataItem
        {
            get
            {
                return ((StoredValueData == null) || (StoredValueData.Count == 0)) ? null : StoredValueData[0];
            }
        }

        /// <summary>
        /// Adds stored value data
        /// </summary>
        /// <param name="storedValueData"></param>
        public void AddStoredValueData(StoredValueData storedValueData)
        {
            if(StoredValueData == null)
            {
                StoredValueData = new List<StoredValueData>();
            }
            StoredValueData.Add(storedValueData);
        }

        /// <summary>
        /// Adds stored value data
        /// </summary>
        /// <param name="storedValueTransactionType"></param>
        /// <param name="itemAmount"></param>
        /// <param name="storedValueProvider"></param>
        /// <param name="storedValueAccountID"></param>
        /// <param name="originalPOITransaction"></param>
        /// <param name="productCode"></param>
        /// <param name="eanUpc"></param>
        /// <param name="totalFeesAmount"></param>
        /// <param name="currency"></param>
        public void AddStoredValueData(
            StoredValueTransactionType storedValueTransactionType,
            decimal? itemAmount,
            string storedValueProvider = null,             
            StoredValueAccountID storedValueAccountID = null,
            OriginalPOITransaction originalPOITransaction = null,
            string productCode = null,
            string eanUpc = null,            
            decimal? totalFeesAmount = null,
            string currency = null)
        {
            if (StoredValueData == null)
            {
                StoredValueData = new List<StoredValueData>();
            }
            StoredValueData.Add(new StoredValueData() 
            {
                StoredValueTransactionType = storedValueTransactionType,
                ItemAmount = itemAmount,
                StoredValueProvider = storedValueProvider,
                StoredValueAccountID = storedValueAccountID,
                OriginalPOITransaction = originalPOITransaction,
                ProductCode = productCode,
                EanUpc = eanUpc,
                TotalFeesAmount = totalFeesAmount,
                Currency = currency

            });
        }

        /// <summary>
        /// Adds stored value account id
        /// </summary>
        /// <param name="entryMode"></param>
        /// <param name="storedValueAccountType"></param>
        /// <param name="storedValueProvider"></param>
        /// <param name="ownerName"></param>
        /// <param name="expiryData"></param>
        /// <param name="identificationType"></param>
        /// <param name="storeValueID"></param>
        public void AddStoredValueAccountID(
            EntryMode entryMode,
            StoredValueAccountType storedValueAccountType = StoredValueAccountType.GiftCard,
            string storedValueProvider = null,
            string ownerName = null,
            string expiryData = null,
            IdentificationType identificationType = IdentificationType.PAN,
            string storeValueID = null)
        {
            AddStoredValueAccountID(0, entryMode);
        }

        /// <summary>
        /// Adds stored value account id
        /// </summary>
        /// <param name="index"></param>
        /// <param name="entryMode"></param>
        /// <param name="storedValueAccountType"></param>
        /// <param name="storedValueProvider"></param>
        /// <param name="ownerName"></param>
        /// <param name="expiryDate"></param>
        /// <param name="identificationType"></param>
        /// <param name="storeValueID"></param>
        public void AddStoredValueAccountID(int index,
            EntryMode entryMode,
            StoredValueAccountType storedValueAccountType = StoredValueAccountType.GiftCard,
            string storedValueProvider = null,
            string ownerName = null,
            string expiryDate = null,            
            IdentificationType identificationType = IdentificationType.PAN,
            string storeValueID = null)
        {
            if (StoredValueData == null)
            {
                StoredValueData = new List<StoredValueData>();
                index = 0;
            }
            else if(index > StoredValueData.Count)
            {
                return;
            }
            StoredValueData[index].StoredValueAccountID = new StoredValueAccountID()
            {
                StoredValueAccountType = storedValueAccountType,
                StoredValueProvider = storedValueProvider,
                OwnerName = ownerName,
                ExpiryDate = expiryDate,
                EntryMode = entryMode,
                IdentificationType = identificationType,
                StoredValueID = storeValueID
            };
        }

        /// <summary>
        /// Created default stored value response message
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            StoredValueResult storedValueResult = new StoredValueResult();

            return new StoredValueResponse()
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                },

                StoredValueResult = GetStoredValueResultList(StoredValueData),

                SaleData = SaleData
            };
        }

        private List<StoredValueResult> GetStoredValueResultList(List<StoredValueData> storedValueData)
        {
            List<StoredValueResult> storedValueResult = null;
            if (storedValueData != null)
            {
                storedValueResult = new List<StoredValueResult>();
                foreach(StoredValueData storedValueDataItem in storedValueData) {
                    if (storedValueDataItem == null)
                        continue;
                    storedValueResult.Add(GetStoredValueResult(storedValueDataItem));
                }
            }
            return storedValueResult;
        }

        private StoredValueResult GetStoredValueResult(StoredValueData storedValueData)
        {
            StoredValueResult storedValueResult = null;
            if (storedValueData != null)
            {
                storedValueResult = new StoredValueResult()
                {
                    StoredValueTransactionType = storedValueData.StoredValueTransactionType,
                    ProductCode = storedValueData.ProductCode,
                    EanUpc = storedValueData.EanUpc,
                    ItemAmount = storedValueData.ItemAmount,
                    TotalFeesAmount = storedValueData.TotalFeesAmount,
                    Currency = storedValueData.Currency,
                    StoredValueAccountStatus = new StoredValueAccountStatus()
                    {
                        StoredValueAccountID = storedValueData.StoredValueAccountID,
                        CurrentBalance = 0.00M
                    }
                };
            }
            return storedValueResult;
        }
    }
}
