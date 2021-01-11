namespace FlightBooking.Core
{
    public class Plane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Number of available seat on an aircraft
        /// </summary>
        public int NumberOfSeats { get; set; }
    }
}
