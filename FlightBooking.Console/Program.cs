using System;
using FlightBooking.Core;
using FlightBooking.Core.Constants;
using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;

namespace FlightBooking.Console
{
    internal class Program
    {
        private static ScheduledFlight _scheduledFlight ;

        private static void Main(string[] args)
        {
            SetupAirlineData();
            
            string command;
            do
            {
                System.Console.WriteLine("Please enter command.");
                command = System.Console.ReadLine() ?? "";
                var enteredText = command.ToLower();
                if (enteredText.Contains(Commands.PrintSummary))
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine(_scheduledFlight.GetSummary());
                }
                else if (enteredText.Contains(Commands.AddGeneral))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new GeneralPassenger
                    {   
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3])                        
                    });
                }
                else if (enteredText.Contains(Commands.AddLoyalty))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new LoyaltyMember
                    {
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                        LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
                        IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
                    });
                }
                else if (enteredText.Contains(Commands.AddAirline))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new Employee
                    {
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains(Commands.AddDiscounted))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new DiscountedPassenger
                    {
                        Name = passengerSegments[2],
                        Age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains(Commands.Exit))
                {
                    Environment.Exit(1);
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(Commands.UnKnown);
                    System.Console.ResetColor();
                }
            } while (command != Commands.Exit);
        }

        private static void SetupAirlineData()
        {
            var londonToParis = new FlightRoute("London", "Paris")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.5
            };

            var airline = new Airline
            {
                Aircrafts = new System.Collections.Generic.List<Plane>
                {
                    new Plane { Id = 111, Name = "Antonov AN-2", NumberOfSeats = 12 },
                    new Plane { Id = 222, Name = "Boeing 737", NumberOfSeats = 15 },
                    new Plane { Id = 333, Name = "Airbus A380", NumberOfSeats = 20 }
                }
            };

            _scheduledFlight = new ScheduledFlight(londonToParis, airline.Aircrafts[0], airline);
        }
    }
}
