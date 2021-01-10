using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class DiscountedPassenger : Passenger
    {
        private double _price;

        public override PassengerType Type => PassengerType.Discounted;

        public override int AllowedBags => 0;

        public override double TicketPrice
        {
            get => _price / 2;
            set => _price = value;
        }
    }
}
