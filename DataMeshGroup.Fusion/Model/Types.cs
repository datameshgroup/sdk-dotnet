using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

//public GenericProfile GenericProfile { get; set; } = GenericProfile.Basic;
//public List<ServiceProfile> ServiceProfiles { get; private set; } = new List<ServiceProfile>();


namespace DataMeshGroup.Fusion.Model
{
    [JsonConverter(typeof(StringEnumConverterWithDefault<MessageClass>))]
    public enum MessageClass 
    { 
        /// <summary>
        /// Unknown message class (default)
        /// </summary>
        Unknown,
        /// <summary>
        /// A transaction message pair initiated by the Sale System, and requested to the POI System
        /// </summary>
        Service,
        /// <summary>
        /// A device message pair
        /// </summary>
        Device,
        /// <summary>
        /// An unsolicited event notification by the POI System to the Sale System.
        /// </summary>
        Event
    };

    [JsonConverter(typeof(StringEnumConverterWithDefault<MessageCategory>))]
    public enum MessageCategory 
    { Unknown, Abort, Admin, BalanceInquiry, Batch, CardAcquisition, CardReaderAPDU, CardReaderInit, CardReaderPowerOff, Diagnosis, Display, EnableService, Event, GetTotals, Input, InputUpdate, Login, Logout, Loyalty, Payment, PIN, Print, Reconciliation, Reversal, Sound, StoredValue, TransactionReport, TransactionStatus, Transmit };

    [JsonConverter(typeof(StringEnumConverterWithDefault<MessageType>))]
    public enum MessageType { Unknown, Request, Response, Notification };

    [JsonConverter(typeof(StringEnumConverterWithDefault<Result>))]
    public enum Result { Unknown, Success, Failure, Partial };

    [JsonConverter(typeof(StringEnumConverterWithDefault<TerminalEnvironment>))]
    public enum TerminalEnvironment { Unknown, Attended, SemiAttended, Unattended }

    [JsonConverter(typeof(StringEnumConverterWithDefault<SaleCapability>))]
    public enum SaleCapability { Unknown, CashierStatus, CashierError, CashierDisplay, POIReplication, CashierInput, CustomerAssistance, CustomerDisplay, CustomerError, CustomerInput, PrinterReceipt, PrinterDocument, PrinterVoucher, MagStripe, ICC, EMVContactless }

    [JsonConverter(typeof(StringEnumConverterWithDefault<GenericProfile>))]
    public enum GenericProfile { Unknown, Basic, Standard, Extended, Custom };

    [JsonConverter(typeof(StringEnumConverterWithDefault<ServiceProfile>))]
    public enum ServiceProfile { Unknown, Synchro, Standard, OneTimeRes, Reservation, Loyalty, StoredValue, PIN, CardReader, Sound, Communication };

    [JsonConverter(typeof(StringEnumConverterWithDefault<TokenRequestedType>))]
    public enum TokenRequestedType { Unknown, Transaction, Customer };

    [JsonConverter(typeof(StringEnumConverterWithDefault<UnitOfMeasure>))]
    public enum UnitOfMeasure { Unknown, Case, Foot, UKGallon, USGallon, Gram, Kilogram, Pound, Meter, Centimetre, Litre, Centilitre, Ounce, Quart, Pint, Mile, Kilometre, Yard, Other, Unit };

    [JsonConverter(typeof(StringEnumConverterWithDefault<PaymentInstrumentType>))]
    public enum PaymentInstrumentType { Unknown, Card, Check, Mobile, StoredValue, Cash };

    [JsonConverter(typeof(StringEnumConverterWithDefault<PaymentType>))]
    public enum PaymentType
    {
        /// <summary>
        /// Unknown or invalid payment type. Do not use.
        /// </summary>
        Unknown,

        /// <summary>
        /// A purchase transaction or purchase with cash-out transaction
        /// </summary>
        Normal,

        /// <summary>
        /// A refund transaction
        /// </summary>
        Refund,

        /// <summary>
        /// A cash-out only transaction
        /// </summary>
        CashAdvance

        //Completion,
        //Instalment,
        //OneTimeReservation,
        //FirstReservation,
        //UpdateReservation,
        //Completion,
        //CashDeposit,
        //Recurring,
        //Instalment,
        //IssuerInstalment,
        //PaidOut,
        //VoiceAuthorisation
    }


    [JsonConverter(typeof(StringEnumConverterWithDefault<OutputFormat>))]
    public enum OutputFormat { Unknown, MessageRef, Text, XHTML, Barcode };


    [JsonConverter(typeof(StringEnumConverterWithDefault<CustomerOrderReq>))]
    public enum CustomerOrderReq { Unknown, Open, Closed, Both };

    [JsonConverter(typeof(StringEnumConverterWithDefault<CurrencySymbol>))]
    public enum CurrencySymbol { Unknown, AUD };

    [JsonConverter(typeof(StringEnumConverterWithDefault<PaymentBrand>))]
    public enum PaymentBrand
    {
        Unknown,

        VISA,

        MasterCard,
        
        [EnumMember(Value = "American Express")] 
        AmericanExpress,
        
        [EnumMember(Value = "Diners Club")] 
        DinersClub,
        
        JCB,

        UnionPay,
        
        [EnumMember(Value = "CUP Debit")] 
        CUPDebit,
        
        Discover
    };


    [JsonConverter(typeof(StringEnumConverterWithDefault<EntryMode>))]
    public enum EntryMode { Unknown, RFID, Keyed, Manual, File, Scanned, MagStripe, ICC, SynchronousICC, Tapped, Contactless, Mobile }


    [JsonConverter(typeof(StringEnumConverterWithDefault<AuthenticationMethod>))]
    public enum AuthenticationMethod
    {
        Unknown,
        Bypass,
        ManualVerification,
        MerchantAuthentication,
        OfflinePIN,
        OnLinePIN,
        PaperSignature,
        SecuredChannel, 
        SecureCertificate, 
        SecureNoCertificate, 
        SignatureCapture, 
        UnknownMethod
    }


    public enum UnifyURL
    {
        /// <summary>
        /// wss://cloudposintegration.io/nexodev
        /// </summary>
        Test,

        /// <summary>
        /// wss://???
        /// </summary>
        Production,

        /// <summary>
        /// A custom URL
        /// </summary>        
        Custom
    }

    public enum UnifyRootCA
    {
        /// <summary>
        /// RootCA for connecting to the test environment
        /// </summary>
        Test,

        /// <summary>
        /// RootCA for connecting to the production environment
        /// </summary>
        Production,

        /// <summary>
        /// Use the system key store
        /// </summary>
        System,

        /// <summary>
        /// A custom RootCA
        /// </summary>        
        Custom
    }

    


    /// <summary>
    /// Any event which occurs outside a transaction requested by the Sale System.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<EventToNotify>))]
    public enum EventToNotify
    {
        /// <summary>
        /// Unknown (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// Begin of POI Maintenance
        /// </summary>
        BeginMaintenance,

        /// <summary>
        /// End of POI Maintenance
        /// </summary>
        EndMaintenance,

        /// <summary>
        /// The POI Terminal or the POI System is shutting down
        /// </summary>
        Shutdown,

        /// <summary>
        /// The POI Terminal or the POI System is now ready to work
        /// </summary>
        Initialised,

        /// <summary>
        /// The POI Terminal or the POI System cannot work
        /// </summary>
        OutOfOrder,

        /// <summary>
        /// An Abort request has been sent to abort a message which is already completed.
        /// </summary>
        Completed,

        /// <summary>
        /// One or several device request has been sent by the POI during the processing of a service requested by the Sale System. The processing is cancelled by the Customer or stopped by the POI. If the device response is not received by the POI, an event is sent to inform the Sale to abort internally these device requests.
        /// </summary>
        Abort,

        /// <summary>
        /// A POI terminal requests the payment of the transaction identified by the content of EventDetails in the Event notification.
        /// </summary>
        SaleWakeUp,

        /// <summary>
        /// The POI has performed, or want to perform an automatic administrative process, e.g. the reports at the end of day.
        /// </summary>
        SaleAdmin,

        /// <summary>
        /// The customer has selected a different language on the POI.
        /// </summary>
        CustomerLanguage,

        /// <summary>
        /// The customer has pressed a specific key on the POI.
        /// </summary>
        KeyPressed,

        /// <summary>
        /// Problem of security
        /// </summary>
        SecurityAlarm,

        /// <summary>
        /// When the Customer assistance is stopped, because the Customer has completed its input.
        /// </summary>
        StopAssistance,

        /// <summary>
        /// A card is inserted in the card reader (see Input request and NotifyCardInputFlag)
        /// </summary>
        CardInserted,

        /// <summary>
        /// A card is removed from the card reader.
        /// </summary>
        CardRemoved,

        /// <summary>
        /// A message request is rejected. An error explanation and the message in error have to be put in the EventDetails data element.
        /// </summary>
        Reject,

        /// <summary>
        /// 
        /// </summary>
        CompletedMessage
    }


    [JsonConverter(typeof(StringEnumConverterWithDefault<POICapability>))]
    public enum POICapability { Unknown, CashierDisplay, CashierError, CashierInput, CustomerDisplay, CustomerError, CustomerInput, PrinterReceipt, PrinterDocument, PrinterVoucher, MagStripe, ICC, EMVContactless, CashHandling }

    /// <summary>
    /// Providers extra detail in the event of an error which enables refinement of error processing by the sale software. Error conditions may be added over time.To ensure forwards compatibility any error handling in the Sale System should allow for a "catch all" which handles currently undefined error conditions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<ErrorCondition>))]
    public enum ErrorCondition
    {
        /// <summary>
        /// Unknown error condition (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// The Initiator of the request has sent an Abort message request, which was accepted and processed.
        /// </summary>
        Aborted,

        /// <summary>
        /// The system is busy, try later
        /// </summary>
        Busy,

        /// <summary>
        /// The user has aborted the transaction on the PED keyboard, for instance during PIN entering.
        /// </summary>
        Cancel,

        /// <summary>
        /// Device out of order
        /// </summary>
        DeviceOut,

        /// <summary>
        /// If the Input Device request a NotifyCardInputFlag and the card holder enters a card in the card reader without answers to the Input command, the POI abort the Input command processing, and answer a dedicated ErrorCondition value in the Input response message.
        /// </summary>
        InsertedCard,

        /// <summary>
        /// The transaction is still in progress and then the command cannot be processed
        /// </summary>
        InProgress,

        /// <summary>
        /// Not logged in
        /// </summary>
        LoggedOut,

        /// <summary>
        /// Error on the format of the message, AdditionalResponse shall contain the identification of the data, and the reason in clear text.
        /// </summary>
        MessageFormat,

        /// <summary>
        /// A service request is sent during a Service dialogue. A combination of services not possible to provide. During the CardReaderInit message processing, the user has entered a card which has to be protected by the POI, and cannot be processed with this device request from the external, and then the Sale System.
        /// </summary>
        NotAllowed,

        /// <summary>
        /// The transaction is not found (e.g. for a reversal or a repeat)
        /// </summary>
        NotFound,

        /// <summary>
        /// Some sale items are not payable by the card proposed by the card holder.
        /// </summary>
        PaymentRestriction,

        /// <summary>
        /// The transaction is refused by the host or the rules associated to the card, and cannot be repeated.
        /// </summary>
        Refusal,

        /// <summary>
        /// The hardware is not available (absent, not configured...)
        /// </summary>
        UnavailableDevice,

        /// <summary>
        /// The service is not available (not implemented, not configured, protocol version too old...)
        /// </summary>
        UnavailableService,

        /// <summary>
        /// The card entered by the card holder cannot be processed by the POI because this card is not configured in the system
        /// </summary>
        InvalidCard,

        /// <summary>
        /// Acquirer or any host is unreachable or has not answered to an online request, so is considered as temporary unavailable. Depending on the Sale context, the request could be repeated (to be compared with "Refusal").
        /// </summary>
        UnreachableHost,

        /// <summary>
        /// The user has entered the PIN on the PED keyboard and the verification fails
        /// </summary>
        WrongPIN
    }


    public static class ComponentType
    {
        public const string SERV = "SERV";
        public const string MDWR = "MDWR";
        public const string OPST = "OPST";
        public const string APPL = "APPL";
        public const string SECM = "SECM";
    }


    public static class AllowedPaymentBrand
    {
        public const string Visa = "Visa";
        public const string MasterCard = "MasterCard";
        public const string AmericanExpress = "American Express";
    }




    public static class GlobalStatus
    {
        public const string OK = "OK";
        public const string Busy = "Busy";
        public const string Maintenance = "Maintenance";
        public const string Unreachable = "Unreachable";
    }

    public static class PrinterStatus
    {
        public const string OK = "OK";
        public const string PaperLow = "PaperLow";
        public const string NoPaper = "NoPaper";
        public const string PaperJam = "PaperJam";
        public const string OutOfOrder = "OutOfOrder";
    }

    public static class IdentificationType
    {
        public const string PAN = "PAN";
        public const string ISOTrack2 = "ISOTrack2";
        public const string BarCode = "BarCode";
        public const string AccountNumber = "AccountNumber";
        public const string PhoneNumber = "PhoneNumber";
    }

    public static class IdentificationSupport
    {
        public const string NoCard = "NoCard";
        public const string LoyaltyCard = "LoyaltyCard";
        public const string HybridCard = "HybridCard";
        public const string LinkedCard = "LinkedCard";
    }

    public static class LoyaltyUnit
    {
        public const string Point = "Point";
        public const string Monetary = "Monetary";
    }

    public static class TrackFormat
    {
        public const string ISO = "ISO";
        public const string JISI = "JIS-I";
        public const string JISII = "JIS-II";
        public const string AAMVA = "AAMVA";
        public const string CMC7 = "CMC-7";
        public const string E13B = "E-13B";
    }

    public static class LoyaltyHandling
    {
        public const string Forbidden = "Forbidden";
        public const string Processed = "Processed";
        public const string Allowed = "Allowed";
        public const string Proposed = "Proposed";
        public const string Required = "Required";
    }

    public static class ForceEntryMode
    {
        public const string RFID = "RFID";
        public const string Keyed = "Keyed";
        public const string Manual = "Manual";
        public const string File = "File";
        public const string Scanned = "Scanned";
        public const string MagStripe = "MagStripe";
        public const string ICC = "ICC";
        public const string SynchronousICC = "SynchronousICC";
        public const string Tapped = "Tapped";
        public const string Contactless = "Contactless";
        public const string CheckReader = "CheckReader";
    }

    public static class InstalmentType
    {
        public const string DeferredInstalments = "DeferredInstalments";
        public const string EqualInstalments = "EqualInstalments";
        public const string InequalInstalments = "InequalInstalments";
    }

    public static class PeriodUnit
    {
        public const string Daily = "Daily";
        public const string Weekly = "Weekly";
        public const string Monthly = "Monthly";
        public const string Annual = "Annual";
    }

    public static class CheckTypeCode
    {
        public const string Personal = "Personal";
        public const string Company = "Company";
    }



    public static class DocumentQualifier
    {
        public const string SaleReceipt = "SaleReceipt";
        public const string CashierReceipt = "CashierReceipt";
        public const string CustomerReceipt = "CustomerReceipt";
        public const string Document = "Document";
        public const string Voucher = "Voucher";
        public const string Journal = "Journal";
    }

    public static class Color
    {
        public const string White = "White";
        public const string Black = "Black";
        public const string CustomerReceipt = "CustomerReceipt";
        public const string Red = "Red";
        public const string Green = "Green";
        public const string Blue = "Blue";
        public const string Magenta = "Magenta";
        public const string Yellow = "Yellow";
        public const string Cyan = "Cyan";
    }

    public static class CharacterWidth
    {
        public const string SingleWidth = "SingleWidth";
        public const string DoubleWidth = "DoubleWidth";
    }

    public static class CharacterHeight
    {
        public const string SingleHeight = "SingleHeight";
        public const string DoubleHeight = "DoubleHeight";
        public const string HalfHeight = "HalfHeight";
    }

    public static class CharacterStyle
    {
        public const string Normal = "Normal";
        public const string Bold = "Bold";
        public const string Italic = "Italic";
        public const string Underlined = "Underlined";
        public const string Reverse = "Reverse";
    }

    public static class Alignment
    {
        public const string Left = "Left";
        public const string Right = "Right";
        public const string Centred = "Centred";
        public const string Justified = "Justified";
    }

    public static class BarcodeType
    {
        public const string EAN8 = "EAN8";
        public const string EAN13 = "EAN13";
        public const string UPCA = "UPCA";
        public const string Code25 = "Code25";
        public const string Code128 = "Code128";
        public const string PDF417 = "PDF417";
        public const string QRCode = "QRCode";
    }

    public static class StoredValueTransactionType
    {
        public const string Reserve = "Reserve";
        public const string Activate = "Activate";
        public const string Load = "Load";
        public const string Unload = "Unload";
        public const string Reverse = "Reverse";
        public const string Duplicate = "Duplicate";
    }

    public static class StoredValueAccountType
    {
        public const string GiftCard = "GiftCard";
        public const string PhoneCard = "PhoneCard";
        public const string Other = "Other";
    }

    public static class Device
    {
        public const string CashierDisplay = "CashierDisplay";
        public const string CustomerDisplay = "CustomerDisplay";
        public const string CashierInput = "CashierInput";
        public const string CustomerInput = "CustomerInput";
    }

    public static class InfoQualify
    {
        public const string Status = "Status";
        public const string Error = "Error";
        public const string Display = "Display";
        public const string Sound = "Sound";
        public const string Input = "Input";
        public const string POIReplication = "POIReplication";
        public const string CustomerAssistance = "CustomerAssistance";
        public const string Receipt = "Receipt";
        public const string Document = "Document";
        public const string Voucher = "Voucher";
    }

    public static class MenuEntryTag
    {
        public const string Selectable = "Selectable";
        public const string NonSelectable = "NonSelectable";
        public const string SubMenu = "SubMenu";
        public const string NonSelectableSubMenu = "NonSelectableSubMenu";
    }

    public static class ReversalReason
    {
        public const string CustCancel = "CustCancel";
        public const string MerchantCancel = "MerchantCancel";
        public const string Malfunction = "Malfunction";
        public const string Unable2Compl = "Unable2Compl";
    }

    public static class ReconciliationType
    {
        public const string SaleReconciliation = "SaleReconciliation";
        public const string AcquirerSynchronisation = "AcquirerSynchronisation";
        public const string AcquirerReconciliation = "AcquirerReconciliation";
        public const string InternalReconciliation = "InternalReconciliation";
    }

    public static class TransactionType
    {
        public const string Debit = "Debit";
        public const string Credit = "Credit";
        public const string ReverseDebit = "ReverseDebit";
        public const string ReverseCredit = "ReverseCredit";
        public const string OneTimeReservation = "OneTimeReservation";
        public const string CompletedDeffered = "CompletedDeffered";
        public const string FirstReservation = "FirstReservation";
        public const string UpdateReservation = "UpdateReservation";
        public const string CompletedReservation = "CompletedReservation";
        public const string CashAdvance = "CashAdvance";
        public const string IssuerInstalment = "IssuerInstalment";
        public const string Declined = "Declined";
        public const string Failed = "Failed";
        public const string Award = "Award";
        public const string ReverseAward = "ReverseAward";
        public const string Redemption = "Redemption";
        public const string ReverseRedemption = "ReverseRedemption";
        public const string Rebate = "Rebate";
        public const string ReverseRebate = "ReverseRebate";
    }
}
