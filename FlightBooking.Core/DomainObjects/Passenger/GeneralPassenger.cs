using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class GeneralPassenger : Passenger
    {
        public override PassengerType Type => PassengerType.General;

        public override int AllowedBags => 1;
    }
}
