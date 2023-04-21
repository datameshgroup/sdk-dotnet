using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model.Transit
{
    /// <summary>
    /// Contains details of a trip related to a payment 
    /// </summary>
    public class Trip
    {
        /// <summary>
        /// Constructs a default Trip
        /// </summary>
        public Trip(int numOfStops = 2)
        {
            Stops = new List<Stop>()
            {
                new Stop()
                {
                    StopIndex = 0
                },
                new Stop()
                {
                    StopIndex = 1
                }
            };
        }

        /// <summary>
        /// Total distance travelled in kilometers.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalDistanceTravelled { get; set; }

        /// <summary>
        /// Defines an array of stops for the trip. MUST contain a minimum of two entries which indicate start and end of the trip.
        /// </summary>
        public List<Stop> Stops { get; set; }

        /// <summary>
        /// Helper function to access the "Pickup" stop
        /// </summary>
        public Stop Pickup
        {
            get
            {
                return Stops.FirstOrDefault();
            }
            set
            {
                Stops[0] = value;
            }
        }

        /// <summary>
        /// Helper function to access the "Destination" stop
        /// </summary>
        public Stop Destination
        {
            get
            {
                return Stops.FirstOrDefault();
            }
            set
            {
                Stops[Stops.Count-1] = value;
            }
        }

    }
}
