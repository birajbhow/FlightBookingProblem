using System;
using System.Linq;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        private FlightSummary _flightSummary;

        public ScheduledFlight(FlightRoute flightRoute)
        {
            _flightSummary = new FlightSummary(flightRoute);
        }

        public FlightRoute FlightRoute { get { return _flightSummary.FlightRoute; } }
        public Plane Aircraft { get { return _flightSummary.Aircraft; } }
        public List<Passenger> Passengers { get { return _flightSummary.Passengers; } }

        public void AddPassenger(Passenger passenger)
        {
            _flightSummary.Passengers.Add(passenger);
            _flightSummary.SeatsTaken++;
            _flightSummary.TotalExpectedBaggage += passenger.AllowedBags;
            _flightSummary.Cost += FlightRoute.BaseCost;
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            _flightSummary.Aircraft = aircraft;
        }
        
        public string GetSummary()
        {   
            foreach (var passenger in _flightSummary.Passengers)
            {
                switch (passenger.Type)
                {
                    case(PassengerType.General):
                        {
                            _flightSummary.Profit += FlightRoute.BasePrice;
                            break;
                        }
                    case(PassengerType.LoyaltyMember):
                        {
                            var loyaltyMember = passenger as LoyaltyMember;
                            if (loyaltyMember.IsUsingLoyaltyPoints)
                            {
                                var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(FlightRoute.BasePrice));
                                loyaltyMember.LoyaltyPoints -= loyaltyPointsRedeemed;
                                _flightSummary.TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                            }
                            else
                            {
                                _flightSummary.TotalLoyaltyPointsAccrued += FlightRoute.LoyaltyPointsGained;
                                _flightSummary.Profit += FlightRoute.BasePrice;                           
                            }
                            break;
                        }
                    case (PassengerType.AirlineEmployee):
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return _flightSummary.ToString();
        }
    }
}
