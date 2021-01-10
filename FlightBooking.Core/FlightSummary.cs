using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core
{
    public class FlightSummary
    {
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";
        private readonly FlightRoute _flightRoute;

        public FlightSummary(FlightRoute flightRoute)
        {
            //GeneralPassengerCount = 0;
            //LoyaltyPassengerCount = 0;
            //EmployeeCount = 0;
            Passengers = new List<Passenger>();
            Cost = 0;
            Profit = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            TotalExpectedBaggage = 0;
            SeatsTaken = 0;
            _flightRoute = flightRoute;
        }

        //public int GeneralPassengerCount { get; set; }
        //public int LoyaltyPassengerCount { get; set; }
        //public int EmployeeCount { get; set; }
        public Plane Aircraft { get; set; }
        public List<Passenger> Passengers { get; private set; }
        public double Cost { get; set; }
        public double Profit { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalExpectedBaggage { get; set; }
        public int SeatsTaken { get; set; }

        public override string ToString()
        {
            var result = "Flight summary for " + _flightRoute.Title;

            result += _verticalWhiteSpace;

            result += "Total passengers: " + SeatsTaken;
            result += _newLine;
            result += Indentation + "General sales: " + Passengers.Count(p => p.Type == PassengerType.General);
            result += _newLine;
            result += Indentation + "Loyalty member sales: " + Passengers.Count(p => p.Type == PassengerType.LoyaltyMember);
            result += _newLine;
            result += Indentation + "Airline employee comps: " + Passengers.Count(p => p.Type == PassengerType.AirlineEmployee);

            result += _verticalWhiteSpace;
            result += "Total expected baggage: " + TotalExpectedBaggage;

            result += _verticalWhiteSpace;

            result += "Total revenue from flight: " + Profit;
            result += _newLine;
            result += "Total costs from flight: " + Cost;
            result += _newLine;

            var profitSurplus = Profit - Cost;

            result += (profitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + profitSurplus;

            result += _verticalWhiteSpace;

            result += "Total loyalty points given away: " + TotalLoyaltyPointsAccrued + _newLine;
            result += "Total loyalty points redeemed: " + TotalLoyaltyPointsRedeemed + _newLine;

            result += _verticalWhiteSpace;

            if (profitSurplus > 0 &&
                SeatsTaken < Aircraft.NumberOfSeats &&
                SeatsTaken / (double)Aircraft.NumberOfSeats > _flightRoute.MinimumTakeOffPercentage)
            {
                result += "THIS FLIGHT MAY PROCEED";
            }
            else
            {
                result += "FLIGHT MAY NOT PROCEED";
            }

            return result;
        }
    }
}
