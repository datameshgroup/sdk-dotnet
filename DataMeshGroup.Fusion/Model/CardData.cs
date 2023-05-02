using System;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Contains information relating to the card used for the payment
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// Indicates the card type used. One of VISA, Mastercard, American Express, Diners Club, JCB, UnionPay, CUP Debit, Discover, Debit, AliPay, WeChat Pay, Card
        /// NOTE: <see cref="PaymentBrand"/> is not an enum. This list will expand in the future as new payment types are added.
        /// </summary>
        public string PaymentBrand { get; set; }

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

        [Obsolete]
        public ProtectedCardData ProtectedCardData { get; set; }
        
        //public string AllowedProductCodes { get; set; }
        //public AllowedProduct AllowedProduct { get; set; }

        /// <summary>
        /// Only present if EntryMode is "File". Object with identifies the payment token.
        /// </summary>
        public PaymentToken PaymentToken { get; set; }

        //public CustomerOrder CustomerOrder { get; set; }
    }
}
