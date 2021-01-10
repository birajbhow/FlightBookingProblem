using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core.DomainObjects.Passenger
{
    public class Employee : Passenger
    {
        public override PassengerType Type => PassengerType.AirlineEmployee;
    }
}
