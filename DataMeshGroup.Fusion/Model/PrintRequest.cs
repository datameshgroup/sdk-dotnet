using DataMeshGroup.Fusion.Model;
using System;
using System.Runtime.CompilerServices;

namespace DataMeshGroup.Fusion.Model
{
    public class PrintRequest : MessagePayload
    {
        public PrintRequest() :
            base(MessageClass.Device, MessageCategory.Print, MessageType.Request)
        {
        }

        /// <summary>
        /// Get a plain text version of the specified receipt type
        /// </summary>
        /// <returns>
        /// A plain text version of the receipt specified, or null if no such receipt exists
        /// </returns>
        public string GetReceiptAsPlainText()
        {
            return PrintOutput?.OutputContent.GetContentAsPlainText();
        }

        /// <summary>
        /// Information to print and the way to process the print.
        /// </summary>
        public PrintOutput PrintOutput { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new PrintResponse
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }


    }
}