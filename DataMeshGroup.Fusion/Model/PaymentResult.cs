using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentResult
    {
        public PaymentType PaymentType { get; set; }
        public PaymentInstrumentData PaymentInstrumentData { get; set; }
        public AmountsResp AmountsResp { get; set; }
        //public Instalment Instalment { get; set; }
        public CurrencyConversion CurrencyConversion { get; set; }
        //public bool? MerchantOverrideFlag { get; set; }
        //public CapturedSignature CapturedSignature { get; set; }
        //public string ProtectedSignature { get; set; }
        public string CustomerLanguage { get; set; }
        public bool? OnlineFlag { get; set; }
        public List<AuthenticationMethod> AuthenticationMethod { get; set; }
        public string ValidityDate { get; set; }

        /// <summary>
        /// Data related to the response from the payment acquirer
        /// </summary>
        public PaymentAcquirerData PaymentAcquirerData { get; set; }

        /// <summary>
        /// Currency of the Balance
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Balance of the Account
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentBalance { get; set; }
    }
}
