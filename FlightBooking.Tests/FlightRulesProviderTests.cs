using FlightBooking.Core.DomainServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Tests
{
    [TestFixture]
    public class FlightRulesProviderTests
    {
        [Test]
        public void CanProceed_Returns_True()
        {
            // arrange
            var flightRoute = MockData.FlightRoute;
            flightRoute.MinimumTakeOffPercentage = 0.1;
            var subject = new FlightRulesProvider(MockData.FlightSummary, flightRoute, MockData.Aircraft("AAA", 10), 
                new List<Core.DomainObjects.Passenger.Passenger> { MockData.GetGeneralPassenger() }, MockData.Airline);

            // act
            var result = subject.CanProceed();

            // assert
            Assert.IsTrue(result);
        }

        /// TODO: Add More Tests
    }
}
