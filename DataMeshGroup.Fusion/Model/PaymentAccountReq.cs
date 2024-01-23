using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentAccountReq
    {
        public PaymentAccountReq() 
        {
        }

        /// <summary>
        /// Creates PaymentAccountReq using Stored Value card data
        /// </summary>
        /// <param name="storedValueAccountID"></param>
        public PaymentAccountReq(StoredValueAccountID storedValueAccountID)
        {
            PaymentInstrumentData = new PaymentInstrumentData();
            PaymentInstrumentData.PaymentInstrumentType = PaymentInstrumentType.StoredValue;
            PaymentInstrumentData.StoredValueAccountID = storedValueAccountID;
        }


        /// <summary>
        /// Creates PaymentAccountReq using card data
        /// </summary>
        /// <param name="cardData"></param>
        public PaymentAccountReq(CardData cardData)
        {
            PaymentInstrumentData = new PaymentInstrumentData();
            PaymentInstrumentData.PaymentInstrumentType = PaymentInstrumentType.Card;
            PaymentInstrumentData.CardData = cardData;
        }

        /// <summary>
        /// Type of cardholder account used for the transaction
        /// </summary>
        public AccountType AccountType { get; set; } = AccountType.Default;

        /// <summary>
        /// Reference to the last CardAcquisition, to use the same card
        /// </summary>
        public TransactionIdentification CardAcquisitionReference { get; set; }

        /// <summary>
        /// Data related to the instrument of payment for the transaction
        /// </summary>
        public PaymentInstrumentData PaymentInstrumentData { get; set; }
    }
}
