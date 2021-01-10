using FlightBooking.Core;
using NUnit.Framework;

namespace FlightBooking.Tests
{
    public class ScheduledFlightTests
    {
        private ScheduledFlight _subject;

        [SetUp]
        public void Setup()
        {
            var flightRoute = new FlightRoute("London", "NewYork") { BaseCost = 100, MinimumTakeOffPercentage = 0.5 };
            this._subject = new ScheduledFlight(flightRoute);
        }

        [Test]
        public void GetSummary_Returns_Valid_Info()
        {
            // arrange            
            // act
            var result = this._subject.GetSummary();

            // assert
            Assert.IsTrue(result.Length > 0);
        }

        [Test]
        public void AddPassenger_Works_Fine()
        {
            // arrange
            // act
            this._subject.AddPassenger(new GeneralPassenger
            {
                Name = "passenger1",
                Age = 30
            });

            // assert
            Assert.AreEqual(1, this._subject.Passengers.Count);
        }

        [Test]
        public void SetAircraftForRoute_Works_Fine()
        {
            // arrange
            var aircraftName = "Boeing 001";
            var aircraft = new Plane
            {
                Id = 1,
                Name = aircraftName,
                NumberOfSeats = 200
            };

            // act
            this._subject.SetAircraftForRoute(aircraft);

            // assert
            Assert.AreEqual(aircraftName, this._subject.Aircraft.Name);
        }

        [Test]
        public void FlightRoute_Works_Fine()
        {            
            // assert
            Assert.AreEqual(100, this._subject.FlightRoute.BaseCost);
        }
    }
}