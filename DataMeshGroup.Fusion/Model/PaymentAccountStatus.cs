using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentAccountStatus
    {
        public PaymentAccountStatus() { }

        public PaymentInstrumentData PaymentInstrumentData { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency {  get; set; }

        /// <summary>
        /// Balance of the Account
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentBalance { get; set; }

        /// <summary>
        /// Data related to the response from the payment Acquirer
        /// </summary>
        public PaymentAcquirerData PaymentAcquirerData { get; set; }        
    }
}
