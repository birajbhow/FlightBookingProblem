using System;
using System.Linq;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        private FlightSummary _flightSummary;

        //private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        //private readonly string _newLine = Environment.NewLine;
        //private const string Indentation = "    ";

        public ScheduledFlight(FlightRoute flightRoute)
        {
            //FlightRoute = flightRoute;
            //Passengers = new List<Passenger>();
            this._flightSummary = new FlightSummary(flightRoute);
        }

        public FlightRoute FlightRoute { get; }
        public Plane Aircraft { get { return _flightSummary.Aircraft; } }
        public List<Passenger> Passengers { get { return _flightSummary.Passengers; } }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            this._flightSummary.Aircraft = aircraft;
        }
        
        public string GetSummary()
        {   
            //var result = "Flight summary for " + FlightRoute.Title;

            foreach (var passenger in Passengers)
            {
                switch (passenger.Type)
                {
                    case(PassengerType.General):
                        {
                            _flightSummary.Profit += FlightRoute.BasePrice;
                            _flightSummary.TotalExpectedBaggage++;
                            break;
                        }
                    case(PassengerType.LoyaltyMember):
                        {
                            if (passenger.IsUsingLoyaltyPoints)
                            {
                                var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(FlightRoute.BasePrice));
                                passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                                _flightSummary.TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                            }
                            else
                            {
                                _flightSummary.TotalLoyaltyPointsAccrued += FlightRoute.LoyaltyPointsGained;
                                _flightSummary.Profit += FlightRoute.BasePrice;                           
                            }
                            _flightSummary.TotalExpectedBaggage += 2;
                            break;
                        }
                    case(PassengerType.AirlineEmployee):
                        {
                            _flightSummary.TotalExpectedBaggage += 1;
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _flightSummary.Cost += FlightRoute.BaseCost;
                _flightSummary.SeatsTaken++;
            }

            return _flightSummary.ToString();

            //result += _verticalWhiteSpace;
            
            //result += "Total passengers: " + seatsTaken;
            //result += _newLine;
            //result += Indentation + "General sales: " + Passengers.Count(p => p.Type == PassengerType.General);
            //result += _newLine;
            //result += Indentation + "Loyalty member sales: " + Passengers.Count(p => p.Type == PassengerType.LoyaltyMember);
            //result += _newLine;
            //result += Indentation + "Airline employee comps: " + Passengers.Count(p => p.Type == PassengerType.AirlineEmployee);
            
            //result += _verticalWhiteSpace;
            //result += "Total expected baggage: " + totalExpectedBaggage;

            //result += _verticalWhiteSpace;

            //result += "Total revenue from flight: " + profitFromFlight;
            //result += _newLine;
            //result += "Total costs from flight: " + costOfFlight;
            //result += _newLine;

            //var profitSurplus = profitFromFlight - costOfFlight;

            //result += (profitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + profitSurplus;

            //result += _verticalWhiteSpace;

            //result += "Total loyalty points given away: " + totalLoyaltyPointsAccrued + _newLine;
            //result += "Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed + _newLine;

            //result += _verticalWhiteSpace;

            //if (profitSurplus > 0 &&
            //    seatsTaken < Aircraft.NumberOfSeats &&
            //    seatsTaken / (double) Aircraft.NumberOfSeats > FlightRoute.MinimumTakeOffPercentage)
            //{
            //    result += "THIS FLIGHT MAY PROCEED";
            //}
            //else
            //{
            //    result += "FLIGHT MAY NOT PROCEED";
            //}
            
            //return result;
        }
    }
}
