namespace FlightBooking.Core.DomainObjects.Passenger
{
    public abstract class Passenger
    {
        public abstract PassengerType Type { get; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual int AllowedBags => 1;
        public virtual double TicketPrice { get; set; }
    }
}
