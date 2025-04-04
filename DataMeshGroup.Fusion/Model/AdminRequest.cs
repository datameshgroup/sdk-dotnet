﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Allows the POS to perform non-payment admin actions. Each <see cref="AdminRequest"/> is paired with an <see cref="AdminResponse"/>
    /// </summary>
    public class AdminRequest : MessagePayload
    {
        public AdminRequest() :
            base(MessageClass.Service, MessageCategory.Admin, MessageType.Request)
        {
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new AdminResponse
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }

        /// <summary>
        /// Defines the type of admin function to perform
        /// </summary>
        /// <example>
        /// PrintLastCustomerReceipt, PrintLastMerchantReceipt, PrintShiftTotals, PrintShiftTotals, MessageACK
        /// </example>
        public string ServiceIdentification { get; set; }

        /// <summary>
        /// If this admin request is linked to a previous message
        /// </summary>
        public MessageReference MessageReference { get; set; }
    }
}
