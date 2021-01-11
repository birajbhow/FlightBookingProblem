using FlightBooking.Core.DomainObjects.Passenger;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core.DomainServices
{
    /// <summary>
    /// A class which encapsulate all the print messages for a flight (using flight rules provider)
    /// </summary>
    public class PrintManager
    {
        private const string Indentation = "    ";
        private readonly FlightRoute _flightRoute;
        private readonly FlightSummary _flightSummary;
        private readonly FlightRulesProvider _flightRuleManager;
        private readonly List<Passenger> _passengers;

        public PrintManager(FlightRoute flightRoute, FlightSummary flightSummary,
            FlightRulesProvider flightRuleManager, List<Passenger> passengers)
        {
            this._flightRoute = flightRoute;
            this._flightSummary = flightSummary;
            this._flightRuleManager = flightRuleManager;
            this._passengers = passengers;
        }
        public string BuildFlightSummary()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight summary for {_flightRoute.Title}");

            sb.AppendLine();
            
            PrintPassengerInfo(sb);            
            
            sb.AppendLine();
            
            PrintBaggageInfo(sb);            
            
            sb.AppendLine();
            
            PrintRevenueAndCost(sb);            
            
            PrintProfitLoss(sb);            
            
            sb.AppendLine();
            
            PrintLoyaltyInfo(sb);            
            
            sb.AppendLine();
            
            PrintFlightStatus(sb);            
            
            return sb.ToString();
        }

        #region Private Methods
        private void PrintLoyaltyInfo(StringBuilder sb)
        {
            sb.AppendLine($"Total loyalty points given away: {_flightSummary.TotalLoyaltyPointsAccrued}");
            sb.AppendLine($"Total loyalty points redeemed: {_flightSummary.TotalLoyaltyPointsRedeemed}");
        }

        private void PrintRevenueAndCost(StringBuilder sb)
        {
            sb.AppendLine($"Total revenue from flight:  {_flightSummary.Revenue}");
            sb.AppendLine($"Total costs from flight::  {_flightSummary.Cost}");
        }

        private void PrintBaggageInfo(StringBuilder sb)
        {
            sb.AppendLine($"Total expected baggage: {_flightSummary.TotalExpectedBaggage}");
        }

        private void PrintPassengerInfo(StringBuilder sb)
        {
            sb.AppendLine($"Total passengers: {_flightSummary.SeatsTaken}");
            sb.AppendLine($"{Indentation}General sales: {_passengers.Count(p => p.Type == PassengerType.General)}");
            sb.AppendLine($"{Indentation}Loyalty member sales: {_passengers.Count(p => p.Type == PassengerType.LoyaltyMember)}");
            sb.AppendLine($"{Indentation}Airline employee comps: {_passengers.Count(p => p.Type == PassengerType.AirlineEmployee)}");
            sb.AppendLine($"{Indentation}Discounted sales: {_passengers.Count(p => p.Type == PassengerType.Discounted)}");
        }

        private void PrintProfitLoss(StringBuilder sb)
        {
            if (_flightSummary.ProfitSurplus > 0)
            {
                sb.AppendLine($"Flight generating profit of: {_flightSummary.ProfitSurplus}");
            }
            else
            {
                sb.AppendLine($"Flight losing money of: {_flightSummary.ProfitSurplus}");
            }
        }

        /// <summary>
        /// Print flight flying status based on flight rules provider
        /// </summary>
        /// <param name="sb"></param>
        private void PrintFlightStatus(StringBuilder sb)
        {
            /// Can flight proceed to fly based on business rules?
            if (_flightRuleManager.CanProceed())
            {
                sb.AppendLine("THIS FLIGHT MAY PROCEED");
            }
            else
            {
                /// Print flight won't proceed with other alternative aircraft details
                sb.AppendLine("FLIGHT MAY NOT PROCEED");
                var availableAircrafts = _flightRuleManager.RunOverbookingRule();
                if (availableAircrafts?.Any() == true)
                {
                    sb.AppendLine("Other more suitable aircraft are:");
                    foreach(var aircraft in availableAircrafts)
                    {
                        sb.AppendLine($"{aircraft} could handle this flight.");
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
