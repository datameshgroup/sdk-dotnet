using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model.Transit
{
    /// <summary>
    /// Details of a stop associated with a trip
    /// </summary>
    public class Stop
    {

        public Stop(int stopIndex = 0)
        {
            this.StopIndex = stopIndex;
        }

        /// <summary>
        /// Sequential index of the stop, starting from 0. 
        ///   - Start of trip is represented by lowest StopIndex
        ///   - End of trip is represented by highest StopIndex
        /// </summary>
        public int StopIndex { get; set; }

        /// <summary>
        /// Unique identifier for the stop
        /// </summary>
        public string StopID { get; set; }

        /// <summary>
        /// Name of the stop location to be displayed on the receipt. e.g. suburb name, or pre-defined value such as HOSPITAL, HOME, OFFICE etc
        /// </summary>
        public string StopName { get; set; }

        /// <summary>
        /// Unique identifier for the zone the stop.
        /// </summary>
        public string ZoneID { get; set; }

        /// <summary>
        /// Latitude of the stop location.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Longitude of the stop location.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Timestamp the stop occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
