using System;
using System.Collections.Generic;

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
            if(index > -1 && index < result.Length - 2)
            {
                result = result.Substring(index+2).Trim();
            }

            return result;
        }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }

        public DisplayOutput DisplayOutput { get; set; }
    }
}