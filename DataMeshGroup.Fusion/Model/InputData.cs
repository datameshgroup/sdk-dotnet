namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Information related to an Input request.
    /// It conveys the target input logical device, the type of input command, and possible minimum and maximum length of the input.
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// Indicates the device the input should be on. Will be set to CashierInput
        /// </summary>
        public Device Device { get; set; }


        /// <summary>
        /// Qualification of the information to sent to an output logical device, to display or print to the Cashier or the Customer.
        /// Will be set to Input or CustomerAssistance
        /// </summary>
        public InfoQualify InfoQualify { get; set; }

        /// <summary>
        /// Type of requested input
        /// </summary>
        public InputCommand InputCommand { get; set; }

        /// <summary>
        /// Request Notification of card entered in the POI card reader. Not supported.
        /// </summary>
        public bool? NotifyCardInputFlag { get; set; }

        /// <summary>
        /// Maximum input time in seconds before the POS should respond with a timeout
        /// </summary>
        public int? MaxInputTime { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is GetAnyKey. 
        /// When true, Sale System should immediatly respond to this InputRequest with an InputResponse and not wait for the operator input.
        /// When false, the Sale System should only respond to this InputReuest with an InputResponse once the operator has acknoledged the input request.
        /// Default behaviour is "true"
        /// </summary>
        public bool? ImmediateResponseFlag { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is TextString, DigitString, DecimalString, Password or GetMenuEntry. 
        /// Lower or equal to MaxLength. 
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is TextString, DigitString, DecimalString, Password or GetMenuEntry.
        /// Greater or equal to MinLength.
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is DecimalString.
        /// Greater than MinLength, lower than MaxLength.
        /// </summary>
        public int? MaxDecimalLength { get; set; }

        /// <summary>
        /// Indicates that the user must confirm the entered characters, when the maximum allowed length is reached. 
        /// 
        /// Used during the processing of an Input command TextString, DigitString or DecimalString with MaxLength 
        /// or MaxDecimalLength present in the request.
        /// 
        /// Default False
        /// </summary>
        public bool? WaitUserValidationFlag { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is TextString, DigitString, DecimalString or Password.
        /// 
        /// On the TextString, DigitString and DecimalString input commands: default string displayed on the input field before entering the string.
        /// 
        /// On GetConfirmation input command: "Y" for yes, "N" for no.
        /// </summary>
        public string DefaultInputString { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is TextString, DigitString, DecimalString or Password.
        /// 
        /// String mask to get information requiring a specific format.
        /// 
        /// For the processing of an Input command TextString, DigitString or DecimalString. 
        /// Some information as date or plate number required to be entered with a certain format. 
        /// The String mask can contain the following characters: 
        /// - 'd': a digit '0' to '9' 
        /// - 'a': an alphabetic character 'a' to 'z', 'A' to 'Z' 
        /// - 's': any other printable US ASCII character - any other character will be displayed but not entered (including space) 
        /// - '\': the escape characters to precede the characters 'd', 'a', 's' or '\', in order to display these characters
        /// 
        /// The string sent in the response contains the string mask replaces the 'a,'d' and 's' characters by what is entered by the user.
        /// 
        /// </summary>
        public string StringMask { get; set; }

        /// <summary>
        /// Supported where <see cref="InputCommand"/> is not TextString, DigitString, DecimalString or Password.
        /// 
        /// Indicate if the entered character has to be displayed from the right to the left of the display field.
        /// 
        /// Default False
        /// </summary>
        public bool? FromRightToLeftFlag { get; set; }


        /// <summary>
        /// Indicates to mask the characters entered by the user (i.e. replacing in the display of the input, the entered character by a standard character as '*').
        /// 
        /// Supported where <see cref="InputCommand"/> is not TextString, DigitString or Password
        /// 
        /// Default False
        /// </summary>
        public bool? MaskCharactersFlag { get; set; }

        /// <summary>
        /// If it has the value True, it indicates that the "Back" function key (respectively "Home" function key) may be used to go back to the immediate upper level of the menu (resp. to the home of the menu).
        /// If it has the value False, it indicates that the current menu level has no parent menu.
        /// </summary>
        public bool? MenuBackFlag { get; set; }
    }
}
