using FlightBooking.Core.DomainObjects.Passenger;
using System;

namespace FlightBooking.Core
{
    /// <summary>
    /// Important flight information aggregation
    /// </summary>
    public class FlightSummary
    {   
        private readonly FlightRoute _flightRoute;

        public FlightSummary(FlightRoute flightRoute)
        {
            _flightRoute = flightRoute;
            Cost = 0;
            Revenue = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            TotalExpectedBaggage = 0;
            SeatsTaken = 0;
        }
        
        public double Cost { get; set; }
        public double Revenue { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalExpectedBaggage { get; set; }
        public int SeatsTaken { get; set; }
        public double ProfitSurplus => Revenue - Cost;

        /// <summary>
        /// Flight summary info updated when new passenger is added
        /// </summary>
        /// <param name="passenger">A type of passenger</param>
        public void Update(Passenger passenger)
        {
            SeatsTaken++;
            TotalExpectedBaggage += passenger.AllowedBags;
            Cost += _flightRoute.BaseCost;

            switch(passenger.Type)
            {
                //case PassengerType.AirlineEmployee:
                //    break;
                // Total loyalty points for flight changes based on loyalty member settings
                case PassengerType.LoyaltyMember:
                    {
                        var loyaltyMember = passenger as LoyaltyMember;
                        if (loyaltyMember.IsUsingLoyaltyPoints)
                        {
                            TotalLoyaltyPointsRedeemed += Convert.ToInt32(Math.Ceiling(_flightRoute.BasePrice));
                        }
                        else
                        {
                            TotalLoyaltyPointsAccrued += _flightRoute.LoyaltyPointsGained;
                            Revenue += passenger.TicketPrice;
                        }
                    }
                    break;
                default:
                    // Total flight revenue updated based on ticket price of a passenger type
                    Revenue += passenger.TicketPrice;
                    break;
            }
        }
    }
}
