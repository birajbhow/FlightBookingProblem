using System;

namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class LoyaltyMember :  Passenger
    {
        private double _price;

        public override PassengerType Type => PassengerType.LoyaltyMember;
        
        // Business Rule: Loyalty members allowed one additional baggage
        public override int AllowedBags => base.AllowedBags + 1;        
        public bool IsUsingLoyaltyPoints { get; set; }
        public int LoyaltyPoints { get; set; }

        // Business Rule: Current loyalty points reduces if using loyalty points
        public override double TicketPrice
        {
            get => _price;
            set
            {
                _price = value;
                if (IsUsingLoyaltyPoints)
                {
                    LoyaltyPoints -= Convert.ToInt32(Math.Ceiling(_price));
                }
            }
        }
    }
}
