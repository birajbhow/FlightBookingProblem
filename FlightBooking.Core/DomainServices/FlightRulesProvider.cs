using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Core.DomainServices
{
    public class FlightRulesProvider
    {
        private readonly FlightSummary flightSummary;
        private readonly FlightRoute flightRoute;
        private readonly Plane aircraft;
        private readonly List<Passenger> passengers;
        private Airline airline;
        
        public FlightRulesProvider(FlightSummary flightSummary, FlightRoute flightRoute, Plane aircraft, List<Passenger> passengers, Airline airline)
        {
            this.flightSummary = flightSummary;
            this.flightRoute = flightRoute;
            this.aircraft = aircraft;
            this.passengers = passengers;
            this.airline = airline;
        }

        public bool CanProceed()
        {
            var result = SeatTakenRule && MinimumTakeOffRule;
            if (NumberOfAirlineEmployee < CurrentPassengerPercent)
            {
                return result && ProfitRule;
            }
            return result;
        }

        public IEnumerable<string> RunOverbookingRule()
        {
            return SeatTakenRule
                ? new List<string>()
                : airline.Aircrafts
                    .Where(a => a.Id != aircraft.Id && a.NumberOfSeats > aircraft.NumberOfSeats)
                    .Select(a => a.Name);
        }

        #region Private Methods
        
        private bool ProfitRule => flightSummary.ProfitSurplus > 0;
        private bool SeatTakenRule => flightSummary.SeatsTaken <= aircraft.NumberOfSeats;
        private bool MinimumTakeOffRule => CurrentPassengerPercent > flightRoute.MinimumTakeOffPercentage;
        private double CurrentPassengerPercent => flightSummary.SeatsTaken / (double)aircraft.NumberOfSeats;
        private int NumberOfAirlineEmployee => passengers.Count(p => p.Type == PassengerType.AirlineEmployee);

        #endregion Private Methods
    }
}
