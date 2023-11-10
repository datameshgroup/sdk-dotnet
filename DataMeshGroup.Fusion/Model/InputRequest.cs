using DataMeshGroup.Fusion.Model;
using System;
using System.Runtime.CompilerServices;

namespace DataMeshGroup.Fusion.Model
{
    public class InputRequest : MessagePayload
    {
        public InputRequest() :
            base(MessageClass.Device, MessageCategory.Input, MessageType.Request)
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
            return new InputResponse
            {
                OutputResult = new OutputResult()
                {
                    Response = response ?? new Response()
                    {
                        Result = Result.Failure,
                        ErrorCondition = ErrorCondition.Aborted,
                        AdditionalResponse = ""
                    }
                },
                InputResult = new InputResult()
                {
                    Response = response ?? new Response()
                    {
                        Result = Result.Failure,
                        ErrorCondition = ErrorCondition.Aborted,
                        AdditionalResponse = ""
                    }
                }
            };
        }

        /// <summary>
        /// Information to display
        /// </summary>
        public DisplayOutput DisplayOutput { get; set; }

        /// <summary>
        /// Information related to an Input request.
        /// It conveys the target input logical device, the type of input command, and possible minimum and maximum length of the input.
        /// </summary>
        public InputData InputData { get; set; }
    }
}