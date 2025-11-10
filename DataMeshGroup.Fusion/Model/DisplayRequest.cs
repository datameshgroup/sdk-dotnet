using DataMeshGroup.Fusion.Model;
using System;
using System.Runtime.CompilerServices;

namespace DataMeshGroup.Fusion.Model
{
    public class DisplayRequest : MessagePayload
    {
        public DisplayRequest() :
            base(MessageClass.Service, MessageCategory.Display, MessageType.Request)
        {
        }

        /// <summary>
        /// Get a plain text version of the display intended for the cashier
        /// </summary>
        /// <returns>A plain text version of the display intended for the cashier, or null if no such display exists</returns>
        public string GetCashierDisplayAsPlainText()
        {
            string result = DisplayOutput?.OutputContent?.OutputText?.Text;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            // Some strings will be formatted as "SHORT TEXT | FULL TEXT"
            int index = result.IndexOf("|");
            if (index > -1 && index < result.Length - 2)
            {
                result = result.Substring(index + 2).Trim();
            }

            return result;
        }

        /// <summary>
        /// Constructs a DisplayOutput for a Cashier display as plain text
        /// </summary>
        /// <param name="text">Cashier display text to set</param>
        public void SetCashierDisplayAsPlainText(string text)
        {
            DisplayOutput = new DisplayOutput()
            {
                OutputContent = new OutputContent()
                {
                    OutputText = new OutputText()
                    {
                        Text = text
                    }
                }
            };
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }

        public DisplayOutput DisplayOutput { get; set; }

        /// <summary>
        /// Indicates if the abort request option should be disabled on the display
        /// </summary>
        public bool? DisableAbortRequestOption { get; set; }
    }
}