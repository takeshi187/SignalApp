using SignalApp.ApplicationServices.Services;
using SignalApp.Domain.Models;

namespace SignalApp.Tests.Unit
{
    [TestFixture]
    public class SignalProcessorTests
    {
        private SignalProcessor _signalProcessor;

        [SetUp]
        public void Setup()
        {
            _signalProcessor = new SignalProcessor();
        }


        private List<SignalPoint> Points => new()
        {
            new SignalPoint(0, -1),
            new SignalPoint(1, 2),
            new SignalPoint(2, 3)
        };

        [Test]
        public void GetMax_ShouldReturnMaxValue_WhenValid()
        {
            Assert.That(_signalProcessor.GetMax(Points), Is.EqualTo(3));
        }

        [Test]
        public void GetMin_ShouldReturnMinValue_WhenValid()
        {
            Assert.That(_signalProcessor.GetMin(Points), Is.EqualTo(-1));
        }

        [Test]
        public void GetAverage_ShouldReturnAverageValue_WhenValid()
        {
            double expected = (-1 + 2 + 3) / 3.0;
            Assert.That(_signalProcessor.GetAverage(Points), Is.EqualTo(expected));
        }

        [Test]
        public void ZeroCrossingsCount_ShouldReturnZeroCrossingsCount_WhenValid()
        {
            var points = new List<SignalPoint>
        {
            new(0, -1),
            new(1, 1),
            new(2, -1),
            new(3, 1)
        };

            Assert.That(_signalProcessor.ZeroCrossingsCount(points), Is.EqualTo(3));
        }
    }
}
