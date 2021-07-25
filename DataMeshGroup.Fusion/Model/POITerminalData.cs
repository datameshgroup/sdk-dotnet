using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class POITerminalData
    {
        public string TerminalEnvironment { get; set; }
        public List<POICapability> POICapabilities { get; set; } = new List<POICapability>();
        public POIProfile POIProfile { get; set; }
        public string POISerialNumber { get; set; }
    }
}
