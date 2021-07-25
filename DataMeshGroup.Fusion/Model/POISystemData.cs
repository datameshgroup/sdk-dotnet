namespace DataMeshGroup.Fusion.Model
{
    public class POISystemData
    {
        public string DateTime { get; set; }
        public POISoftware POISoftware { get; set; }
        public POITerminalData POITerminalData { get; set; }
        public POIStatus POIStatus { get; set; }
        public bool? TokenRequestStatus { get; set; }
    }
}
