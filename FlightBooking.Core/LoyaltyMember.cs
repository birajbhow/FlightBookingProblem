namespace FlightBooking.Core
{
    public class LoyaltyMember :  Passenger
    {
        public override PassengerType Type => PassengerType.LoyaltyMember;
        public override int AllowedBags => 2;
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }

        
    }
}
