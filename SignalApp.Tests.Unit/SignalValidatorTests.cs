using SignalApp.ApplicationServices.Exceptions;
using SignalApp.ApplicationServices.Services;

namespace SignalApp.Tests.Unit
{
    [TestFixture]
    public class SignalValidatorTests
    {
        private SignalValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new SignalValidator();
        }

        [Test]
        public void ValidateSignal_ShouldThrowSignalValidationException_WhenAmplitudeInvalid()
        {
            Assert.Throws<SignalValidationException>(() =>
                _validator.Validate(0, 1, 10));
        }

        [Test]
        public void ValidateSignal_ShouldThrowSignalValidationException_WhenFrequencyInvalid()
        {
            Assert.Throws<SignalValidationException>(() =>
                _validator.Validate(1, -5, 10));
        }

        [Test]
        public void ValidateSignal_ShouldThrowSignalValidationException_WhenPointsCountInvalid()
        {
            Assert.Throws<SignalValidationException>(() =>
                _validator.Validate(1, 1, 1));
        }

        [Test]
        public void ValidateSignal_ShouldNotThrowSignalValidationException_WhenCorrectValues()
        {
            Assert.DoesNotThrow(() =>
                _validator.Validate(1, 1, 100));
        }
    }
}
