namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class Employee : Passenger
    {
        public override PassengerType Type => PassengerType.AirlineEmployee;

        // Business Rule: Employee fly free
        public override double TicketPrice => 0;
    }
}
