using FlightBooking.Core;
using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Tests
{
    public static class MockData
    {
        public static GeneralPassenger GetGeneralPassenger()
        {
            return new GeneralPassenger
            {
                Name = "General Passenger",
                Age = 30,
                TicketPrice = 100
            };
        }

        public static LoyaltyMember GetLoyaltyPassenger(bool isUsingLoyaltyPoints)
        {
            return new LoyaltyMember
            {
                Name = "Loyalty Member",
                Age = 30,
                TicketPrice = 100,
                IsUsingLoyaltyPoints = isUsingLoyaltyPoints,
                LoyaltyPoints = 1000
            };
        }

        public static Employee GetEmployeePassenger()
        {
            return new Employee
            {
                Name = "Airline Employee",
                Age = 30,
                TicketPrice = 100
            };
        }

        public static DiscountedPassenger GetDiscountedPassenger()
        {
            return new DiscountedPassenger
            {
                Name = "Discounted Passenger",
                Age = 30,
                TicketPrice = 100                
            };
        }

        public static FlightRoute FlightRoute => new FlightRoute("London", "Torrento")
        {
            BaseCost = 50,
            BasePrice = 100,
            LoyaltyPointsGained = 10,
            MinimumTakeOffPercentage = 0.5
        };

        public static Plane Aircraft(string name, int numberOfSeats) => new Plane 
        {
            Id = new Random().Next(1, 100),
            Name = name, 
            NumberOfSeats = numberOfSeats 
        };

        public static Airline Airline => new Airline
        {
            Aircrafts = new List<Plane>
            {
                Aircraft("AAA1", 2),
                Aircraft("AAA2", 3)
            }
        };

        public static FlightSummary FlightSummary => new FlightSummary(FlightRoute)
        {
            SeatsTaken = 5,
            TotalExpectedBaggage = 5,
            Revenue = 100.0,
            Cost = 50.0
        };
    }
}
