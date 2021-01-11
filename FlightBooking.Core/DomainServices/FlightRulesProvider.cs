using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Core.DomainServices
{
    /// <summary>
    /// A service to encapsulate the flight business rules to print summary information
    /// TODO: Refactor to Write rules based on interface
    /// </summary>
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

        /// <summary>
        /// Business rule to decide whether flight can proceed or not
        /// </summary>
        /// <returns></returns>
        public bool CanProceed()
        {
            /// Ignoring the profit rule if number of airline employees are more than 
            /// the minimum percentage of passengers required
            var result = SeatTakenRule && MinimumTakeOffRule;
            if (NumberOfAirlineEmployee < flightRoute.MinimumTakeOffPercentage)
            {
                return result && ProfitRule;
            }
            return result;
        }

        /// <summary>
        /// Flight overbooking rule:
        /// If flight is overbooked, check with parent airline if other flights with required
        /// seat capacity available.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> RunOverbookingRule()
        {
            return SeatTakenRule
                ? new List<string>()
                : airline.Aircrafts
                    .Where(a => a.Id != aircraft.Id && a.NumberOfSeats > aircraft.NumberOfSeats)
                    .Select(a => a.Name);
        }

        #region Private Methods
        
        /// <summary>
        /// Check if current flight revenue is more than cost
        /// </summary>
        private bool ProfitRule => flightSummary.ProfitSurplus > 0;

        /// <summary>
        /// Check if seats taken are less than the available seats on the aircraft
        /// </summary>
        private bool SeatTakenRule => flightSummary.SeatsTaken <= aircraft.NumberOfSeats;

        /// <summary>
        /// Check if current passenger percentage is higher than minimum take off percentage
        /// </summary>
        private bool MinimumTakeOffRule => CurrentPassengerPercent > flightRoute.MinimumTakeOffPercentage;

        /// <summary>
        /// Current passenger percentage
        /// </summary>
        private double CurrentPassengerPercent => flightSummary.SeatsTaken / (double)aircraft.NumberOfSeats;

        /// <summary>
        /// Number of this airline employee passengers
        /// </summary>
        private int NumberOfAirlineEmployee => passengers.Count(p => p.Type == PassengerType.AirlineEmployee);

        #endregion Private Methods
    }
}
