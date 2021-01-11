using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using FlightBooking.Core.DomainServices;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        private readonly FlightSummary _flightSummary;
        private readonly Airline _airline;

        public ScheduledFlight(FlightRoute flightRoute, Plane aircraft, Airline airline)
        {
            FlightRoute = flightRoute;
            Aircraft = aircraft;            
            Passengers = new List<Passenger>();
            _airline = airline;
            _flightSummary = new FlightSummary(flightRoute);            
        }

        public FlightRoute FlightRoute { get; }
        public Plane Aircraft { get; private set; }
        public List<Passenger> Passengers { get; }

        public void AddPassenger(Passenger passenger)
        {
            passenger.TicketPrice = FlightRoute.BasePrice;
            Passengers.Add(passenger);
            _flightSummary.Update(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            Aircraft = aircraft;
        }

        public string GetSummary()
        {
            var flightRulesManager = new FlightRulesProvider(_flightSummary, FlightRoute, Aircraft, Passengers, _airline);
            var printManager = new PrintManager(FlightRoute, _flightSummary, flightRulesManager, Passengers);
            return printManager.BuildFlightSummary();
        }
    }
}
