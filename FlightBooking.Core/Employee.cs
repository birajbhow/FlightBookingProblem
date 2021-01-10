using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core
{
    public class Employee : Passenger
    {
        public override PassengerType Type => PassengerType.AirlineEmployee;

        public override int AllowedBags => 1;
    }
}
