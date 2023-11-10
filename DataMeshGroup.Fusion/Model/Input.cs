namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data entered by the user, related to the input command.
    /// Choice of a data which contains data entered by the user on the requested device, depending on the requested InputCommand:
    ///   - GetConfirmation: the input is in ConfirmedFlag.
    ///   - GetAnyKey: there is no input.
    ///   - GetFunctionKey: the input is in FunctionKey
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Mirrored from request
        /// </summary>
        public InputCommand InputCommand { get; set; }

        /// <summary>
        /// Confirmation or not of what has been requested to the user in a GetConfirmation input command.
        /// Mandatory, if InputCommand is GetConfirmation or SiteManager Not allowed, otherwise
        /// </summary>
        public bool? ConfirmedFlag { get; set; }

        /// <summary>
        /// The number of the function key which is typed by the Customer on the POI or the Cashier on the Sale Terminal.
        /// Mandatory, if InputCommand is GetFunctionKey Not allowed, otherwise
        /// </summary>
        public int? FunctionKey { get; set; }

        /// <summary>
        /// Mandatory, if InputCommand is TextString Not allowed, otherwise
        /// The text which is typed by the Customer on the POI or the Cashier on the Sale Terminal.
        /// Input entered by the user, in response to an InputCommand "TextString" or "Password" for a plaintext password.
        /// </summary>
        public string TextInput { get; set; }

        /// <summary>
        /// Mandatory, if InputCommand is DigitString Not allowed, otherwise
        /// The digits which are typed by the Customer on the POI or the Cashier on the Sale Terminal.
        /// </summary>
        public int? DigitInput { get; set; }

        /// <summary>
        /// Mandatory, if InputCommand is Password Not allowed, otherwise
        /// A text password which is typed by the Customer on the POI or the Cashier on the Sale Terminal.
        /// </summary>
        // public ContentInformationType Password { get; set; }

        /// <summary>
        /// Mandatory, if InputCommand is GetMenuEntry Not allowed, otherwise
        /// 
        /// The index from 1 to n, of the menu item which is selected by the Cashier on the Sale Terminal.
        /// The value -1 indicates that the immediate upper level of the menu is requested.
        /// The value 0 indicates that the root of the menu is requested.
        /// </summary>
        public int? MenuEntryNumber { get; set; }
    }
}
