using System;
using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class LoginRequest : MessagePayload
    {
        public LoginRequest() :
            base(MessageClass.Service, MessageCategory.Login, MessageType.Request)
        {
        }

        /// <summary>
        /// Create a <see cref="LoginRequest"/> with the minimum required parameters
        /// </summary>
        /// <param name="providerIdentification">SaleSoftware.ProviderIdentification</param>
        /// <param name="applicationName">SaleSoftware.ApplicationName</param>
        /// <param name="softwareVersion">SaleSoftware.SoftwareVersion</param>
        /// <param name="certificationCode">SaleSoftware.CertificationCode</param>
        /// <param name="saleCapabilities">SaleTerminalData.SaleCapabilities. If null will default to POS printer support only</param>
        public LoginRequest(string providerIdentification, string applicationName, string softwareVersion, string certificationCode, List<SaleCapability> saleCapabilities = null) :
            base(MessageClass.Service, MessageCategory.Login, MessageType.Request)
        {
            SaleSoftware = new SaleSoftware()
            {
                ProviderIdentification = providerIdentification,
                ApplicationName = applicationName,
                SoftwareVersion = softwareVersion,
                CertificationCode = certificationCode
            };

            if (saleCapabilities == null)
            {
                saleCapabilities = new List<SaleCapability>() { SaleCapability.PrinterReceipt, SaleCapability.CashierStatus, SaleCapability.CashierError };
            }

            SaleTerminalData = new SaleTerminalData() { SaleCapabilities = saleCapabilities };
        }


        public string DateTime { get; set; } = System.DateTime.Now.Add(TimeSpan.Zero).ToString("yyyy-MM-ddThh:mm:ss.sszzzzzz", System.Globalization.CultureInfo.InvariantCulture);
        public SaleSoftware SaleSoftware { get; set; }
        public SaleTerminalData SaleTerminalData { get; set; }
        public bool? TrainingModeFlag { get; set; } = false;
        public string OperatorLanguage { get; set; } = "en";
        public string OperatorID { get; set; }
        public string ShiftNumber { get; set; }
        public string TokenRequestedType { get; set; }
        public string CustomerOrderReq { get; set; }
        public string POISerialNumber { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new LoginResponse
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