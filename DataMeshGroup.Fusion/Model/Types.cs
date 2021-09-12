using Newtonsoft.Json;
using System.ComponentModel;
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
    { 
        Unknown, 
        Abort, 
        Admin, 
        BalanceInquiry, 
        Batch, 
        CardAcquisition, 
        CardReaderAPDU, 
        CardReaderInit, 
        CardReaderPowerOff, 
        Diagnosis, 
        Display, 
        EnableService, 
        Event, 
        GetTotals, 
        Input, 
        InputUpdate, 
        Login, 
        Logout, 
        Loyalty, 
        Payment, 
        PIN, 
        Print, 
        Reconciliation, 
        Reversal, 
        Sound, 
        StoredValue, 
        TransactionReport, 
        TransactionStatus, 
        Transmit 
    };

    [JsonConverter(typeof(StringEnumConverterWithDefault<MessageType>))]
    public enum MessageType 
    { 
        Unknown, 
        Request, 
        Response, 
        Notification 
    };

    [JsonConverter(typeof(StringEnumConverterWithDefault<Result>))]
    public enum Result 
    { 
        Unknown, 
        Success, 
        Failure, 
        Partial 
    };

    [JsonConverter(typeof(StringEnumConverterWithDefault<TerminalEnvironment>))]
    public enum TerminalEnvironment 
    { 
        Unknown, 
        Attended, 
        SemiAttended, 
        Unattended 
    }


    /// <summary>
    /// Advises the POI System of the Sale System capabilities and willingness to receive/send event messages.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<SaleCapability>))]
    public enum SaleCapability 
    {
        /// <summary>
        /// Unknown sale capability
        /// </summary>
        Unknown,

        /// <summary>
        /// POI Terminal changes of state are reported back to the Sale System. e.g. "Waiting for Card Presentment", "PIN Entry"
        /// </summary>
        CashierStatus,

        /// <summary>
        /// POI Terminal errors are reported back to the Sale System. e.g. "Out of paper", "Card Swipe Error"
        /// </summary>
        CashierError,

        /// <summary>
        /// Allows the cashier to provide input from the Sale System. e.g. "Signature OK?"
        /// </summary>
        CashierInput,

        /// <summary>
        ///  Allows the cashier to provide assistance to card holders by providing input at specific screens but card holders can still select on terminal. e.g. "Account Selection (Cheque, Savings or Credit)"
        /// </summary>
        CustomerAssistance,

        /// <summary>
        /// Redirects receipts to the Sale System instead of the POI Terminal.
        /// </summary>
        PrinterReceipt

        //CashierDisplay,
        //POIReplication,
        //CustomerDisplay,
        //CustomerError,
        //CustomerInput,
        //PrinterDocument, 
        //PrinterVoucher, 
        //MagStripe, 
        //ICC, 
        //EMVContactless 
    }


    [JsonConverter(typeof(StringEnumConverterWithDefault<GenericProfile>))]
    public enum GenericProfile { Unknown, Basic, Standard, Extended, Custom };

    [JsonConverter(typeof(StringEnumConverterWithDefault<ServiceProfile>))]
    public enum ServiceProfile { Unknown, Synchro, Standard, OneTimeRes, Reservation, Loyalty, StoredValue, PIN, CardReader, Sound, Communication };

    [JsonConverter(typeof(StringEnumConverterWithDefault<TokenRequestedType>))]
    public enum TokenRequestedType { Unknown, Transaction, Customer };

    [JsonConverter(typeof(StringEnumConverterWithDefault<UnitOfMeasure>))]
    public enum UnitOfMeasure { Unknown, Case, Foot, UKGallon, USGallon, Gram, Kilogram, Pound, Meter, Centimetre, Litre, Centilitre, Ounce, Quart, Pint, Mile, Kilometre, Yard, Other, Unit };

    [JsonConverter(typeof(StringEnumConverterWithDefault<WeightUnitOfMeasure>))]
    public enum WeightUnitOfMeasure { Unknown, Gram, Kilogram, Pound, Ounce, Other };

    /// <summary>
    /// Type of payment instrument
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<PaymentInstrumentType>))]
    public enum PaymentInstrumentType 
    {
        /// <summary>
        /// Unknown payment instrument type
        /// </summary>
        Unknown,

        /// <summary>
        /// Payment card (credit or debit).
        /// </summary>
        Card,

        /// <summary>
        /// Paper check.
        /// </summary>
        Check,

        /// <summary>
        /// Operator account accessed by a mobile phone.
        /// </summary>
        Mobile,

        /// <summary>
        /// Account accesed by a stored value instrument such as a card or a certificate.
        /// </summary>
        StoredValue,

        /// <summary>
        /// Cash managed by a cash handling system.
        /// </summary>
        Cash
    };

    
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


    /// <summary>
    /// Payment instrument entry mode requested by the Sale System.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<EntryMode>))]
    public enum EntryMode
    {
        Unknown,

        /// <summary>
        /// Payment instrument information are taken from RFID
        /// </summary>
        RFID,

        /// <summary>
        /// Manual key entry
        /// </summary>
        Keyed,

        /// <summary>
        /// Reading of embossing or OCR of printed data either at time of transaction or after the event.
        /// </summary>
        Manual,

        /// <summary>
        /// Account data on file
        /// </summary>
        File,

        /// <summary>
        /// Scanned by a bar code reader
        /// </summary>
        Scanned,

        /// <summary>
        /// Magnetic stripe 
        /// </summary>
        MagStripe,

        /// <summary>
        /// Contact ICC (asynchronous)
        /// </summary>
        ICC,

        /// <summary>
        /// Contact ICC (synchronous)
        /// </summary>
        SynchronousICC,

        /// <summary>
        /// Contactless card reader Magnetic Stripe
        /// </summary>
        Tapped,

        /// <summary>
        /// Contactless card reader conform to ISO 14443
        /// </summary>
        Contactless,

        /// <summary>
        /// Check Reader
        /// </summary>
        Mobile
    }


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


    /// <summary>
    /// Qualification of the information to sent to an output logical device, to display or print to the Cashier or the Customer.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<InfoQualify>))]
    public enum InfoQualify
    {
        /// <summary>
        /// Unknown qualification (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// The information is a new state on which the message sender is entering.For instance, during a payment, the POI could display to the Cashier that POI request an authorisation to the host acquirer.
        /// </summary>
        Status,

        /// <summary>
        /// The information is related to an error situation occurring on the message sender.
        /// </summary>
        Error,

        /// <summary>
        /// Standard display interface.
        /// </summary>
        Display,

        /// <summary>
        /// Standard sound interface.
        /// </summary>
        Sound,

        /// <summary>
        /// Answer to a question or information to be entered by the Cashier or the Customer, at the request of the POI Terminal or the Sale Terminal.
        /// </summary>
        Input,

        /// <summary>
        /// Information displayed on the Cardholder POI interface, replicated on the Cashier interface.
        /// </summary>
        POIReplication,

        /// <summary>
        /// Input of the Cardholder POI interface which can be entered by the Cashier to assist the Customer.
        /// </summary>
        CustomerAssistance,

        /// <summary>
        /// Where you print the Payment receipt that could be located on the Sale Terminal or in some cases a restricted Sale ticket on the POI Terminal.
        /// </summary>
        Receipt,

        /// <summary>
        /// When the POI System wants to print specific document (check, dynamic currency conversion ...). Used by the Sale System when the printer is not located on the Sale System.
        /// </summary>
        Document,

        /// <summary>
        /// Coupons, voucher or special ticket generated by the POI or the Sale System and to be printed.
        /// </summary>
        Voucher
    }

    /// <summary>
    /// Logical device located on a Sale Terminal or a POI Terminal, in term of class of information to output (display, print or store), or input (keyboard) for the Cashier or the Customer.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<Device>))]
    public enum Device
    {
        /// <summary>
        /// Unknown device (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// Used by the POI System (or the Sale System when the device is managed by the POI Terminal), to display some information to the Cashier.
        /// </summary>
        CashierDisplay,
        
        /// <summary>
        /// Used by the Sale System (or the POI System when the device is managed by the Sale Terminal), to display some information to the Customer.
        /// </summary>
        CustomerDisplay,
        
        /// <summary>
        /// Any kind of keyboard allowing all or part of the commands of the Input message request from the Sale System to the POI System (InputCommand data element). The output device attached to this input device is the CashierDisplay device.
        /// </summary>
        CashierInput,
        
        /// <summary>
        /// Any kind of keyboard allowing all or part of the commands of the Input message request from the POI System to the Sale System (InputCommand data element). The output device attached to this input device is the CustomerDisplay device.
        /// </summary>
        CustomerInput
    }

    /// <summary>
    /// Qualification of the document to print to the Cashier or the Customer.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<DocumentQualifier>))]
    public enum DocumentQualifier
    {
        /// <summary>
        /// Unknown qualification (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// Where the POI system print the Sale receipt when requested by the Sale Terminal.
        /// </summary>
        SaleReceipt,

        /// <summary>
        /// Where the Sale system print the Cashier copy of the Payment receipt when requested by the POI Terminal.
        /// </summary>
        CashierReceipt,

        /// <summary>
        /// Where you print the Customer Payment receipt that could be located on the Sale Terminal or in some cases a restricted Customer Sale ticket on the POI Terminal.
        /// </summary>
        CustomerReceipt,

        /// <summary>
        /// When the POI System wants to print specific document (check, dynamic currency conversion ...). Used by the Sale System when the printer is not located on the Sale System.
        /// </summary>
        Document,

        /// <summary>
        /// Coupons, voucher or special ticket generated by the POI or the Sale System and to be printed.
        /// </summary>
        Voucher,

        /// <summary>
        /// When the POI or the Sale System wants to store a message on the journal printer or electronic journal of the Sale Terminal (it is sometimes a Sale Logging/Journal Printer).
        /// </summary>
        Journal
    }

    /// <summary>
    /// Type of Reconciliation requested by the Sale to the POI.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<ReconciliationType>))]
    public enum ReconciliationType
    {
        /// <summary>
        /// Unknown reconciliation type (default)
        /// </summary>
        [Description("Unknown")]
        Unknown,

        /// <summary>
        /// Reconciliation with closure of the current period
        /// </summary>
        [Description("Sale Reconciliation")]
        SaleReconciliation,

        /// <summary>
        /// Reconciliation and closure of the current period, with synchronisation of the reconciliation between the POI and Acquirers.
        /// </summary>
        // [Description("Acquirer Synchronisation")]
        //AcquirerSynchronisation,

        /// <summary>
        /// Reconciliation between the POI and one or several Acquirers only. There is no reconciliation between the Sale System and the POI System.
        /// </summary>
        [Description("Acquirer Reconciliation")]
        AcquirerReconciliation,

        /// <summary>
        /// Request result of a previous reconciliation.
        /// </summary>
        [Description("Previous Reconciliation")]
        PreviousReconciliation,

        /// <summary>
        /// Returns the current reconciliation totals without closing the current period
        /// </summary>
        [Description("Internal Reconciliation")]
        InternalReconciliation
    }

    /// <summary>
    /// Type of transaction for which totals are grouped.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// An unknown transaction type
        /// </summary>
        Unknown,

        /// <summary>
        /// Payment Debit transactions (e.g. if PaymentType is "Normal")
        /// </summary>
        Debit,

        /// <summary>
        /// Payment Credit transactions (e.g. if PaymentType is "Refund")
        /// </summary>
        Credit,

        /// <summary>
        /// Payment Reversal Debit transactions
        /// </summary>
        ReverseDebit,

        /// <summary>
        /// Payment Reversal Credit transactions
        /// </summary>
        ReverseCredit,

        /// <summary>
        /// Outstanding OneTimeReservation transactions, i.e. between OneTimeReservation and Completion
        /// </summary>
        OneTimeReservation,

        /// <summary>
        /// OneTimeReservation transactions which have been completed by the Completion.
        /// </summary>
        CompletedDeffered,

        /// <summary>
        /// Outstanding FirstReservation transactions, i.e. between FirstReservation and UpdateReservation or Completion
        /// </summary>
        FirstReservation,

        /// <summary>
        /// Outstanding UpdateReservation transactions, i.e. between UpdateReservation and UpdateReservation or Completion
        /// </summary>
        UpdateReservation,

        /// <summary>
        /// Reservation transactions which have been completed by the Completion.
        /// </summary>
        CompletedReservation,

        /// <summary>
        /// Cash Advance transactions.
        /// </summary>
        CashAdvance,

        /// <summary>
        /// Issuer instalment transactions.
        /// </summary>
        IssuerInstalment,

        /// <summary>
        /// Transactions which has not been approved (Result = "Failure" and ErrorCondition = "Refusal").
        /// </summary>
        Declined,

        /// <summary>
        /// Transactions which have not successfully completed (Result = "Failure" and ErrorCondition not equal to "Refusal").
        /// </summary>
        Failed,

        /// <summary>
        /// Loyalty Award Transaction
        /// </summary>
        Award,

        /// <summary>
        /// Loyalty Reversal Award Transaction
        /// </summary>
        ReverseAward,

        /// <summary>
        /// Loyalty Redemption Transaction
        /// </summary>
        Redemption,

        /// <summary>
        /// Loyalty Reversal Redemption Transaction
        /// </summary>
        ReverseRedemption,

        /// <summary>
        /// Loyalty Rebate Transaction
        /// </summary>
        Rebate,

        /// <summary>
        /// Loyalty Reversal Rebate Transaction
        /// </summary>
        ReverseRebate
    }

    /// <summary>
    /// Sent in the Login Request to describe the type of component
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<ComponentType>))]
    public enum ComponentType
    {
        Unknown,

        /// <summary>
        /// Server
        /// </summary>
        SERV,

        /// <summary>
        /// MiddleWare
        /// </summary>
        MDWR,

        /// <summary>
        /// Operating System
        /// </summary>
        OPST,

        /// <summary>
        /// Sale Application
        /// </summary>
        APPL,

        /// <summary>
        /// Security Module
        /// </summary>
        SECM
    }

    /// <summary>
    /// Global status of a POI Server and POI Terminal
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<GlobalStatus>))]
    public enum GlobalStatus
    {
        Unknown,

        /// <summary>
        /// The POI is ready to receive and process a request
        /// </summary>
        OK,

        /// <summary>
        /// The POI Terminal cannot process a request because another processing is in  progress
        /// </summary>
        Busy,

        /// <summary>
        /// The POI is in maintenance processing
        /// </summary>
        Maintenance,

        /// <summary>
        /// The POI is unreachable or not responding
        /// </summary>
        Unreachable
    }

    /// <summary>
    /// Indicates if the printer is working and usable.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<PrinterStatus>))]
    public enum PrinterStatus
    {
        Unknown,

        /// <summary>
        /// The printer is operational.
        /// </summary>
        OK,

        /// <summary>
        /// The printer is operational but paper roll is almost empty.
        /// </summary>
        PaperLow,

        /// <summary>
        /// Paper roll is empty, an operator must insert a new paper roll.
        /// </summary>
        NoPaper,

        /// <summary>
        /// An operator must remove the paper jam manually
        /// </summary>
        PaperJam,
        
        /// <summary>
        /// The printer is out of order
        /// </summary>
        OutOfOrder
    }




    /// <summary>
    /// Type of account identification
    /// In a request message, it informs the POI System the type of the account or card identification, when provided by the Sale Terminal.
    /// (e.g.because the card information are a bar-code read by the Cashier on a scanner device). In a response message, it informs the
    /// Sale System the type of the account or card identification.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<IdentificationType>))]
    public enum IdentificationType
    {
        Unknown,

        /// <summary>
        /// Standard card identification (card number)
        /// </summary>
        PAN,

        /// <summary>
        /// ISO Track 2 including identification
        /// </summary>
        ISOTrack2,

        /// <summary>
        /// Bar-code with a specific form of identification
        /// </summary>
        BarCode,

        /// <summary>
        /// Account number
        /// </summary>
        AccountNumber,

        /// <summary>
        /// A phone number identifies the account on which the phone card is assigned.
        /// </summary>
        PhoneNumber
    }

    /// <summary>
    /// Support of the loyalty account identification. Allows knowing where and how you have found the loyalty account identification.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<IdentificationSupport>))]
    public enum IdentificationSupport
    {
        Unknown,

        /// <summary>
        /// The identification is not found on a card
        /// </summary>
        NoCard,

        /// <summary>
        /// The identification is on a card dedicated to this loyalty brand.
        /// </summary>
        LoyaltyCard,

        /// <summary>
        /// The identification is on a card which might be used both for the loyalty and the payment.
        /// </summary>
        HybridCard,

        /// <summary>
        /// The loyalty account is implicitly attached to the payment card. This is usually detected by the loyalty Acquirer.
        /// </summary>
        LinkedCard
    }

    /// <summary>
    /// Card track format
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<TrackFormat>))]
    public enum TrackFormat
    {
        Unknown,

        /// <summary>
        /// ISO card track format - ISO 7813 - ISO 4909
        /// </summary>
        ISO,

        /// <summary>
        /// Japenese track format I
        /// </summary>
        JISI,

        /// <summary>
        /// Japenese track format II
        /// </summary>
        JISII,

        /// <summary>
        /// American driver license
        /// </summary>
        AAMVA,

        /// <summary>
        /// ((Magnetic Ink Character Recognition, using the CMC-7 font - ISO 1004) Line at  the bottom of a check containing the bank account and the check number.
        /// </summary>
        CMC7,

        /// <summary>
        /// (Magnetic Ink Character Recognition, using the E-13B font) Line at the bottom of a check containing the bank account and the check number
        /// </summary>
        E13B
    }



    /// <summary>
    /// Payment instrument entry mode requested by the Sale System.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<ForceEntryMode>))]
    public enum ForceEntryMode
    {
        Unknown,

        /// <summary>
        /// Payment instrument information are taken from RFID
        /// </summary>
        RFID,

        /// <summary>
        /// Manual key entry
        /// </summary>
        Keyed,

        /// <summary>
        /// Reading of embossing or OCR of printed data either at time of transaction or after the event.
        /// </summary>
        Manual,

        /// <summary>
        /// Account data on file
        /// </summary>
        File,

        /// <summary>
        /// Scanned by a bar code reader
        /// </summary>
        Scanned,

        /// <summary>
        /// Magnetic stripe 
        /// </summary>
        MagStripe,

        /// <summary>
        /// Contact ICC (asynchronous)
        /// </summary>
        ICC,

        /// <summary>
        /// Contact ICC (synchronous)
        /// </summary>
        SynchronousICC,

        /// <summary>
        /// Contactless card reader Magnetic Stripe
        /// </summary>
        Tapped,

        /// <summary>
        /// Contactless card reader conform to ISO 14443
        /// </summary>
        Contactless,

        /// <summary>
        /// Check Reader
        /// </summary>
        Mobile
    }


    /// <summary>
    /// Type of instalment transaction.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<InstalmentType>))]
    public enum InstalmentType
    {
        Unknown,

        /// <summary>
        ///  The payment of the service or goods is deferred.
        /// </summary>
        DeferredInstalments,

        /// <summary>
        /// The payment is split in several instalments of equal amounts.
        /// </summary>
        EqualInstalments,

        /// <summary>
        /// The payment is split in several instalments of different amounts.
        /// </summary>
        InequalInstalments
    }

    /// <summary>
    /// Type of instalment transaction.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverterWithDefault<PeriodUnit>))]
    public enum PeriodUnit
    {
        Unknown,
        Daily,
        Weekly,
        Monthly,
        Annual
    }

    //public static class CheckTypeCode
    //{
    //    public const string Personal = "Personal";
    //    public const string Company = "Company";
    //}

    //public static class Color
    //{
    //    public const string White = "White";
    //    public const string Black = "Black";
    //    public const string CustomerReceipt = "CustomerReceipt";
    //    public const string Red = "Red";
    //    public const string Green = "Green";
    //    public const string Blue = "Blue";
    //    public const string Magenta = "Magenta";
    //    public const string Yellow = "Yellow";
    //    public const string Cyan = "Cyan";
    //}

    //public static class CharacterWidth
    //{
    //    public const string SingleWidth = "SingleWidth";
    //    public const string DoubleWidth = "DoubleWidth";
    //}

    //public static class CharacterHeight
    //{
    //    public const string SingleHeight = "SingleHeight";
    //    public const string DoubleHeight = "DoubleHeight";
    //    public const string HalfHeight = "HalfHeight";
    //}

    //public static class CharacterStyle
    //{
    //    public const string Normal = "Normal";
    //    public const string Bold = "Bold";
    //    public const string Italic = "Italic";
    //    public const string Underlined = "Underlined";
    //    public const string Reverse = "Reverse";
    //}

    //public static class Alignment
    //{
    //    public const string Left = "Left";
    //    public const string Right = "Right";
    //    public const string Centred = "Centred";
    //    public const string Justified = "Justified";
    //}

    //public static class BarcodeType
    //{
    //    public const string EAN8 = "EAN8";
    //    public const string EAN13 = "EAN13";
    //    public const string UPCA = "UPCA";
    //    public const string Code25 = "Code25";
    //    public const string Code128 = "Code128";
    //    public const string PDF417 = "PDF417";
    //    public const string QRCode = "QRCode";
    //}

    //public static class StoredValueTransactionType
    //{
    //    public const string Reserve = "Reserve";
    //    public const string Activate = "Activate";
    //    public const string Load = "Load";
    //    public const string Unload = "Unload";
    //    public const string Reverse = "Reverse";
    //    public const string Duplicate = "Duplicate";
    //}

    //public static class StoredValueAccountType
    //{
    //    public const string GiftCard = "GiftCard";
    //    public const string PhoneCard = "PhoneCard";
    //    public const string Other = "Other";
    //}

    //public static class MenuEntryTag
    //{
    //    public const string Selectable = "Selectable";
    //    public const string NonSelectable = "NonSelectable";
    //    public const string SubMenu = "SubMenu";
    //    public const string NonSelectableSubMenu = "NonSelectableSubMenu";
    //}

    //public static class ReversalReason
    //{
    //    public const string CustCancel = "CustCancel";
    //    public const string MerchantCancel = "MerchantCancel";
    //    public const string Malfunction = "Malfunction";
    //    public const string Unable2Compl = "Unable2Compl";
    //}

    //public static class LoyaltyUnit
    //{
    //    public const string Point = "Point";
    //    public const string Monetary = "Monetary";
    //}

    //public static class LoyaltyHandling
    //{
    //    public const string Forbidden = "Forbidden";
    //    public const string Processed = "Processed";
    //    public const string Allowed = "Allowed";
    //    public const string Proposed = "Proposed";
    //    public const string Required = "Required";
    //}

    //public static class AllowedPaymentBrand
    //{
    //    public const string Visa = "Visa";
    //    public const string MasterCard = "MasterCard";
    //    public const string AmericanExpress = "American Express";
    //}

}
