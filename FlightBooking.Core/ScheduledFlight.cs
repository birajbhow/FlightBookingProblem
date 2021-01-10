using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        private const string Indentation = "    ";
        private FlightSummary _flightSummary;

        public ScheduledFlight(FlightRoute flightRoute)
        {
            FlightRoute = flightRoute;
            Passengers = new List<Passenger>();
            _flightSummary = new FlightSummary(flightRoute);
        }

        public FlightRoute FlightRoute { get; }
        public Plane Aircraft { get; private set; }
        public List<Passenger> Passengers { get; }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
            _flightSummary.Update(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            Aircraft = aircraft;
        }
        
        public string GetSummary()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight summary for {FlightRoute.Title}");
            sb.AppendLine();
            sb.AppendLine($"Total passengers: {_flightSummary.SeatsTaken}");
            sb.AppendLine($"{Indentation}General sales: {Passengers.Count(p => p.Type == PassengerType.General)}");
            sb.AppendLine($"{Indentation}Loyalty member sales: {Passengers.Count(p => p.Type == PassengerType.LoyaltyMember)}");
            sb.AppendLine($"{Indentation}Airline employee comps: {Passengers.Count(p => p.Type == PassengerType.AirlineEmployee)}");
            sb.AppendLine();
            sb.AppendLine($"Total expected baggage: {_flightSummary.TotalExpectedBaggage}");
            sb.AppendLine();
            sb.AppendLine($"Total revenue from flight:  {_flightSummary.Profit}");
            sb.AppendLine($"Total costs from flight::  {_flightSummary.Cost}");

            if (_flightSummary.ProfitSurplus > 0)
            {
                sb.AppendLine($"Flight generating profit of: {_flightSummary.ProfitSurplus}");
            }
            else
            {
                sb.AppendLine($"Flight losing money of: {_flightSummary.ProfitSurplus}");
            }

            sb.AppendLine();
            sb.AppendLine($"Total loyalty points given away: {_flightSummary.TotalLoyaltyPointsAccrued}");
            sb.AppendLine($"Total loyalty points redeemed: {_flightSummary.TotalLoyaltyPointsRedeemed}");
            sb.AppendLine();

            var flightStatus = _flightSummary.GetFlightStatus(Aircraft?.NumberOfSeats ?? 0);
            sb.AppendLine($"{(flightStatus ? "THIS FLIGHT MAY PROCEED" : "FLIGHT MAY NOT PROCEED")}");

            return sb.ToString();
        }
    }
}
