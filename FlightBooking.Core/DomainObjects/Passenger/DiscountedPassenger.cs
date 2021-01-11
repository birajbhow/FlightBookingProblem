namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class DiscountedPassenger : Passenger
    {
        private double _price;

        public override PassengerType Type => PassengerType.Discounted;

        // Business Rule: No baggage allowance
        public override int AllowedBags => 0;

        // Business Rule: Half price ticket price
        public override double TicketPrice
        {
            get => _price / 2;
            set => _price = value;
        }
    }
}
