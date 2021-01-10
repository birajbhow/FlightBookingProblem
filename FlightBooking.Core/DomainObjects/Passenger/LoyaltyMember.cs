using System;

namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class LoyaltyMember :  Passenger
    {
        private double _price;

        public override PassengerType Type => PassengerType.LoyaltyMember;
        public override int AllowedBags => base.AllowedBags + 1;        
        public bool IsUsingLoyaltyPoints { get; set; }
        public int LoyaltyPoints { get; set; }
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
