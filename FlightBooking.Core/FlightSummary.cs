using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core
{
    public class FlightSummary
    {
        private const string Indentation = "    ";
        
        public FlightSummary(FlightRoute flightRoute)
        {
            FlightRoute = flightRoute;
            Passengers = new List<Passenger>();
            Cost = 0;
            Profit = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            TotalExpectedBaggage = 0;
            SeatsTaken = 0;
        }

        public Plane Aircraft { get; set; }
        public List<Passenger> Passengers { get; private set; }
        public FlightRoute FlightRoute { get; private set; }
        public double Cost { get; set; }
        public double Profit { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalExpectedBaggage { get; set; }
        public int SeatsTaken { get; set; }
        public double ProfitSurplus => Profit - Cost;
        public bool FlightStatus => ProfitSurplus > 0 &&
                SeatsTaken < Aircraft.NumberOfSeats &&
                SeatsTaken / (double)Aircraft.NumberOfSeats > FlightRoute.MinimumTakeOffPercentage;

        public override string ToString()
        {   
            var sb = new StringBuilder($"Flight summary for {FlightRoute.Title}");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine($"Total passengers: {SeatsTaken}");
            sb.AppendLine($"{Indentation}General sales: {Passengers.Count(p => p.Type == PassengerType.General)}");
            sb.AppendLine($"{Indentation}Loyalty member sales: {Passengers.Count(p => p.Type == PassengerType.LoyaltyMember)}");
            sb.AppendLine($"{Indentation}Airline employee comps: {Passengers.Count(p => p.Type == PassengerType.AirlineEmployee)}");            
            sb.AppendLine();
            sb.AppendLine($"Total expected baggage: {TotalExpectedBaggage}");            
            sb.AppendLine();
            sb.AppendLine($"Total revenue from flight:  {Profit}");
            sb.AppendLine($"Total costs from flight::  {Cost}");

            if (ProfitSurplus > 0)
            {
                sb.AppendLine($"Flight generating profit of: {ProfitSurplus}");
            } 
            else
            {
                sb.AppendLine($"Flight losing money of: {ProfitSurplus}");
            }
            
            sb.AppendLine();
            sb.AppendLine($"Total loyalty points given away: {TotalLoyaltyPointsAccrued}");
            sb.AppendLine($"Total loyalty points redeemed: {TotalLoyaltyPointsRedeemed}");
            sb.AppendLine();
            sb.AppendLine($"{(FlightStatus ? "THIS FLIGHT MAY PROCEED" : "FLIGHT MAY NOT PROCEED")}");

            return sb.ToString();
        }
    }
}
