using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Contains information relating to the card used for the payment
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// Use <see cref="CardData.PaymentBrandEnum"/> to avoid using string compare
        /// 
        /// Indicates the card type used. One of VISA, Mastercard, American Express, Diners Club, JCB, UnionPay, CUP Debit, Discover, Debit, AliPay, WeChat Pay, Card
        /// Note that <see cref="PaymentBrand"/> is not an enum. This list will expand in the future as new payment types are added.
        /// </summary>
        private string paymentBrand;
        public string PaymentBrand 
        {
            get
            {
                return paymentBrand;
            }
            set
            {
                paymentBrand = value;
                PaymentBrandEnum = ParseEnum<PaymentBrand>(value) ?? Model.PaymentBrand.Unknown;
            }
        }


        /// <summary>
        /// Indicates the card type used. 
        /// </summary>
        [JsonIgnore]
        public PaymentBrand PaymentBrandEnum { get; private set; }


        private Account account = Account.Unknown;
        /// <summary>
        /// Defines the account type used for the payment 
        /// </summary>
        public Account Account 
        { 
            // Handle mapping here as sometimes account is returned as null/blank for Credit 
            get
            {
                return (account == Account.Unknown) ? Account.Credit : account;
            }
            set
            {
                account = value;
            }
        }

        /// <summary>
        /// PAN masked with dots, first 6 and last 4 digits visible
        /// </summary>
        public string MaskedPAN { get; set; }

        /// <summary>
        /// Card expiry formatted as YYMM
        /// </summary>
        private string expiry;
        public string Expiry 
        { 
            get => expiry;
            set
            {
                if (value?.Length != 4 || !int.TryParse(value, out int _)) 
                    return;
                expiry = value;
            } 
        }

        [Obsolete]
        public string PaymentAccountRef { get; set; }

        /// <summary>
        /// Indicates how the card was presented
        /// </summary>
        public EntryMode EntryMode { get; set; }

        /// <summary>
        /// Alphabetic with 2 or 3 characters, or numeric code conforms to ISO 3166 – 1. Indicates the country of the card issuer
        /// </summary>
        public string CardCountryCode { get; set; }

        /// <summary>
        /// Product ID of the card based on a defined DMG enumeration.
        /// </summary>
        public string PaymentBrandId { get; set; }

        /// <summary>
        /// Free text display of the payment type as detected by the POI Terminal app.
        /// </summary>
        public string PaymentBrandLabel { get; set; }

        [Obsolete]
        public ProtectedCardData ProtectedCardData { get; set; }
        
        //public string AllowedProductCodes { get; set; }
        //public AllowedProduct AllowedProduct { get; set; }

        /// <summary>
        /// Only present if EntryMode is "File". Object with identifies the payment token.
        /// </summary>
        public PaymentToken PaymentToken { get; set; }

        //public CustomerOrder CustomerOrder { get; set; }


        public static T? ParseEnum<T>(string value) where T : struct, Enum
        {
            if (value == null)
            {
                return null;
            }

            // Loop through all possible values for T
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                // Try direct string compare
                string enumStr = enumValue.ToString();
                if (value.Equals(enumStr, StringComparison.OrdinalIgnoreCase))
                {
                    return enumValue;
                }

                // If direct compare doesn't work, check for EnumMemberAttribute
                enumStr = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault()?.Value;
                if(value.Equals(enumStr, StringComparison.OrdinalIgnoreCase))
                {
                    return enumValue;
                }
            }

            // If no match is found, return null
            return null;
        }
    }
}
