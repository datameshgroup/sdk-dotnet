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
            AccountType = AccountType.Default;
        }

        /// <summary>
        /// Type of cardholder account used for the transaction
        /// </summary>
        public AccountType AccountType {  get; set; }

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
