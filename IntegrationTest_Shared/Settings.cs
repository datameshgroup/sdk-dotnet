using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class Settings
    {
        public Settings()
        {
        }
        public string SaleID { get; set; }
        public string POIID { get; set; }
        public string KEK { get; set; }
        public string ProviderIdentification { get; set; }
        public string ApplicationName { get; set; }
        public string SoftwareVersion { get; set; }
        public string CertificationCode { get; set; }
        public string OperatorID { get; set; }
        public string ShiftNumber { get; set; }
        public bool EnableLogFile { get; set; }
        public string CustomNexoURL { get; set; }
        public bool UseTestEnvironment { get; set; }
    }
}
