using System.Collections.Generic;

namespace FlightBooking.Core.DomainObjects
{
    /// <summary>
    /// Airline company
    /// </summary>
    public class Airline
    {
        /// <summary>
        /// List of avaiable aircrafts
        /// </summary>
        public List<Plane> Aircrafts { get; set; }
    }
}
