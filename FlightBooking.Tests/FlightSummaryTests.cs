using FlightBooking.Core;
using FlightBooking.Core.DomainObjects.Passenger;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Tests
{
    [TestFixture]
    public class FlightSummaryTests
    {
        private FlightSummary subject;

        [SetUp]
        public void Setup()
        {
            var flightRoute = new FlightRoute("London", "Torrento")
            {
                BaseCost = 50,
                BasePrice = 100,
                LoyaltyPointsGained = 10,
                MinimumTakeOffPercentage = 0.5
            };
            this.subject = new FlightSummary(MockData.FlightRoute);
        }

        [Test]
        [TestCaseSource(nameof(GetPassengers))]
        public void Check_Summary_When_Passenger_Added(Passenger passenger, int seatsTaken, double profitSurplus,
            int expectedBaggage, int loyaltyPointsAccrued, int loyaltyPointsRedeemed)
        {
            // act
            this.subject.Update(passenger);

            // assert
            Assert.AreEqual(seatsTaken, this.subject.SeatsTaken);
            Assert.AreEqual(profitSurplus, this.subject.ProfitSurplus);
            Assert.AreEqual(expectedBaggage, this.subject.TotalExpectedBaggage);
            Assert.AreEqual(loyaltyPointsAccrued, this.subject.TotalLoyaltyPointsAccrued);
            Assert.AreEqual(loyaltyPointsRedeemed, this.subject.TotalLoyaltyPointsRedeemed);
        }

        private static IEnumerable<TestCaseData> GetPassengers
        {
            get
            {
                yield return new TestCaseData(MockData.GetGeneralPassenger(), 1, 50.0, 1, 0, 0);
                yield return new TestCaseData(MockData.GetLoyaltyPassenger(true), 1, -50.0, 2, 0, 100);
                yield return new TestCaseData(MockData.GetLoyaltyPassenger(false), 1, 50.0, 2, 10, 0);
                yield return new TestCaseData(MockData.GetEmployeePassenger(), 1, -50.0, 1, 0, 0);
                yield return new TestCaseData(MockData.GetDiscountedPassenger(), 1, 0, 0, 0, 0);
            }
        }
    }
}
