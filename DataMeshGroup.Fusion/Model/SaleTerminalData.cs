using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleTerminalData
    {
        public TerminalEnvironment TerminalEnvironment { get; set; } = TerminalEnvironment.Attended;
        public List<SaleCapability> SaleCapabilities { get; set; } = new List<SaleCapability>();

        public SaleProfile SaleProfile { get; set; } = new SaleProfile() { GenericProfile = GenericProfile.Basic };
        public string TotalsGroupID { get; set; }
    }
}
