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
        /// <returns>true/false</returns>
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
        /// <returns>List of other alternative flight names</returns>
        public IEnumerable<string> RunOverbookingRule()
        {
            return SeatTakenRule
                ? new List<string>()
                : airline.Aircrafts
                    .Where(a => a.Id != aircraft.Id && a.NumberOfSeats >= OverbookedSeats)
                    .Select(a => a.Name);
        }

        #region Private Flight Rules

        /// <summary>
        /// Rule: The revenue generated from the flight must exceed the cost of the flight
        /// </summary>
        private bool ProfitRule => flightSummary.ProfitSurplus > 0;

        /// <summary>
        /// Rule: The number of passengers cannot exceed the amount of seats on the plane
        /// </summary>
        private bool SeatTakenRule => flightSummary.SeatsTaken <= aircraft.NumberOfSeats;

        /// <summary>
        /// Rule: The aircraft must have a minimum percentage of passengers booked for that route
        /// </summary>
        private bool MinimumTakeOffRule => CurrentPassengerPercent > flightRoute.MinimumTakeOffPercentage;

        #endregion Private Flight Rules

        #region Private Methods
        /// <summary>
        /// Current passenger percentage
        /// </summary>
        private double CurrentPassengerPercent => flightSummary.SeatsTaken / (double)aircraft.NumberOfSeats;

        /// <summary>
        /// Number of this airline employee passengers
        /// </summary>
        private int NumberOfAirlineEmployee => passengers.Count(p => p.Type == PassengerType.AirlineEmployee);

        /// <summary>
        /// Number of overbooked seats than available on this flight
        /// </summary>
        private int OverbookedSeats => flightSummary.SeatsTaken - aircraft.NumberOfSeats;
        #endregion Private Methods
    }
}
