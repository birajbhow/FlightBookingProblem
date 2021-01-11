using FlightBooking.Core;
using FlightBooking.Core.DomainObjects;
using FlightBooking.Core.DomainObjects.Passenger;
using NUnit.Framework;

namespace FlightBooking.Tests
{
    public class ScheduledFlightTests
    {
        private ScheduledFlight _subject;

        [SetUp]
        public void Setup()
        {   
            this._subject = new ScheduledFlight(MockData.FlightRoute, MockData.Aircraft("AAA0", 10), MockData.Airline);
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
            Assert.AreEqual(50.0, this._subject.FlightRoute.BaseCost);
        }

        [Test]
        public void GetSummary_Check_CanProceed_True()
        {
            // arrange
            for(var i=0; i<8; i++)
            {
                this._subject.AddPassenger(MockData.GetGeneralPassenger());
            }            

            // act
            var result = this._subject.GetSummary();

            // assert
            Assert.IsTrue(result.Contains("THIS FLIGHT MAY PROCEED"));
        }

        [Test]
        public void GetSummary_Check_CanProceed_False_With_Additional_Message()
        {
            // arrange                        
            for (var i = 0; i < 13; i++)
            {
                this._subject.AddPassenger(MockData.GetGeneralPassenger());
            }

            // act
            var result = this._subject.GetSummary();

            // assert
            Assert.IsTrue(result.Contains("FLIGHT MAY NOT PROCEED"));
            Assert.IsTrue(result.Contains("AAA2 could handle this flight."));
        }
    }
}