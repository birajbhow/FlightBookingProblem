namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class Employee : Passenger
    {
        public override PassengerType Type => PassengerType.AirlineEmployee;
        public override double TicketPrice => 0;
    }
}
