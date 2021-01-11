using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using FlightBooking.Core.DomainServices;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    /// <summary>
    /// A class to encompasse all the details regarding current flight
    /// </summary>
    public class ScheduledFlight
    {
        private readonly FlightSummary _flightSummary;
        private readonly Airline _airline;
        private readonly object _addLock = new object();

        public ScheduledFlight(FlightRoute flightRoute, Plane aircraft, Airline airline)
        {
            FlightRoute = flightRoute;
            Aircraft = aircraft ?? new Plane();
            Passengers = new List<Passenger>();
            _airline = airline ?? new Airline();
            _flightSummary = new FlightSummary(flightRoute);  
        }

        public FlightRoute FlightRoute { get; }
        public Plane Aircraft { get; private set; }
        public List<Passenger> Passengers { get; }

        /// <summary>
        /// Add new passenger and update flight summary
        /// </summary>
        /// <param name="passenger"></param>
        public void AddPassenger(Passenger passenger)
        {
            lock(_addLock)
            {
                passenger.TicketPrice = FlightRoute.BasePrice;
                Passengers.Add(passenger);
                _flightSummary.Update(passenger);
            }
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            Aircraft = aircraft;
        }

        /// <summary>
        /// Build and Generate this flight summary (based on flight rules)
        /// </summary>
        /// <returns></returns>
        public string GetSummary()
        {
            lock(_addLock)
            {
                var flightRulesManager = new FlightRulesProvider(_flightSummary, FlightRoute, Aircraft, Passengers, _airline);
                var printManager = new PrintManager(FlightRoute, _flightSummary, flightRulesManager, Passengers);
                return printManager.BuildFlightSummary();
            }
        }
    }
}
