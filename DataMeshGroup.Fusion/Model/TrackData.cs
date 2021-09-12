namespace DataMeshGroup.Fusion.Model
{
    public class TrackData
    {
        /// <summary>
        /// Card track number. 1, 2, or 3
        /// </summary>
        public string TrackNumb { get; set; }
        /// <summary>
        /// Card track format
        /// </summary>
        public TrackFormat TrackFormat { get; set; }

        /// <summary>
        /// Card track content
        /// </summary>
        public string TrackValue { get; set; }
    }
}
