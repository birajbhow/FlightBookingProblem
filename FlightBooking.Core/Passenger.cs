namespace FlightBooking.Core
{
    public abstract class Passenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract int AllowedBags { get; }
        //public int LoyaltyPoints { get; set; }
        //public bool IsUsingLoyaltyPoints { get; set; }
        public abstract PassengerType Type { get; }        
    }
    
    public enum PassengerType
    {
        General,
        LoyaltyMember,
        AirlineEmployee
    }
}
