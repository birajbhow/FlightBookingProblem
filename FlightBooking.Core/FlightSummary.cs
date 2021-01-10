using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core
{
    public class FlightSummary
    {   
        private readonly FlightRoute _flightRoute;

        public FlightSummary(FlightRoute flightRoute)
        {
            _flightRoute = flightRoute;
            Cost = 0;
            Profit = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            TotalExpectedBaggage = 0;
            SeatsTaken = 0;
        }
        
        public double Cost { get; set; }
        public double Profit { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalExpectedBaggage { get; set; }
        public int SeatsTaken { get; set; }
        public double ProfitSurplus => Profit - Cost;

        public bool GetFlightStatus(int numberOfSeats) => 
            ProfitSurplus > 0
            && SeatsTaken < numberOfSeats
            && SeatsTaken / (double)numberOfSeats > _flightRoute.MinimumTakeOffPercentage;

        public void Update(Passenger passenger)
        {
            SeatsTaken++;
            TotalExpectedBaggage += passenger.AllowedBags;
            Cost += _flightRoute.BaseCost;

            if (passenger.Type == PassengerType.General)
            {
                Profit += _flightRoute.BasePrice;
            }

            if (passenger.Type == PassengerType.LoyaltyMember)
            {
                var loyaltyMember = passenger as LoyaltyMember;
                if (loyaltyMember.IsUsingLoyaltyPoints)
                {
                    var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(_flightRoute.BasePrice));
                    TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                    loyaltyMember.LoyaltyPoints -= loyaltyPointsRedeemed;
                }
                else
                {
                    TotalLoyaltyPointsAccrued += _flightRoute.LoyaltyPointsGained;
                    Profit += _flightRoute.BasePrice;
                }
            }
        }
    }
}
