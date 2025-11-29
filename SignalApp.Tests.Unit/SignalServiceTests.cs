using Moq;
using SignalApp.ApplicationServices.Interfaces;
using SignalApp.ApplicationServices.Services;
using SignalApp.ApplicationServices.Services.Generators;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;

namespace SignalApp.Tests.Unit
{
    [TestFixture]
    public class SignalServiceTests
    {
        private Mock<ISignalValidator> _signalValidatorMock;
        private Mock<ISignalProcessor> _signalProcessorMock;
        private Mock<ISignalGenerator> _signalGeneratorMock;
        private Mock<ISignalRepository> _signalRepositoryMock;
        private Mock<IFileStorageService> _fileStorageServiceMock;
        private SignalService _signalService;
        private string _testDirectory;

        [SetUp]
        public void Setup()
        {
            _signalValidatorMock = new Mock<ISignalValidator>();
            _signalProcessorMock = new Mock<ISignalProcessor>();
            _signalGeneratorMock = new Mock<ISignalGenerator>();
            _signalRepositoryMock = new Mock<ISignalRepository>();
            _fileStorageServiceMock = new Mock<IFileStorageService>();
            _signalService = new SignalService(
            _signalValidatorMock.Object,
            new List<ISignalGenerator> {
                new SineGenerator(),
                new SquareGenerator(),
                new TriangleGenerator(),
                new SawtoothGenerator()
                },
                _signalProcessorMock.Object,
                _signalRepositoryMock.Object,
                _fileStorageServiceMock.Object
            );

            _testDirectory = "TestSignals";

            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [Test]
        public void Generate_ShouldGenerateSineSignal_WhenValidSignalType()
        {
            var signalType = SignalTypeEnum.Sine;
            double amplitude = 5.0;
            double frequency = 2.0;
            int pointsCount = 100;

            var result = _signalService.Generate(signalType, amplitude, frequency, pointsCount);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(pointsCount));
        }

        [Test]
        public void Generate_ShouldGenerateSquareSignal_WhenValidSignalType()
        {
            var signalType = SignalTypeEnum.Square;
            double amplitude = 5.0;
            double frequency = 2.0;
            int pointsCount = 100;

            var result = _signalService.Generate(signalType, amplitude, frequency, pointsCount);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(pointsCount));
        }

        [Test]
        public void Generate_ShouldGenerateTriangleSignal_WhenValid()
        {
            var signalType = SignalTypeEnum.Triangle;
            double amplitude = 5.0;
            double frequency = 2.0;
            int pointsCount = 100;

            var result = _signalService.Generate(signalType, amplitude, frequency, pointsCount);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(pointsCount));
        }

        [Test]
        public void Generate_ShouldGenerateSawtoothSignal_WhenValid()
        {
            var signalType = SignalTypeEnum.Sawtooth;
            double amplitude = 5.0;
            double frequency = 2.0;
            int pointsCount = 100;

            var result = _signalService.Generate(signalType, amplitude, frequency, pointsCount);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(pointsCount));
        }

        [Test]
        public async Task GenerateAndSaveToDbAsync_ShouldSaveSignal_WhenValid()
        {
            var signalType = SignalTypeEnum.Sine;
            double amplitude = 5.0;
            double frequency = 2.0;
            int pointsCount = 100;

            var points = new List<SignalPoint> { new SignalPoint(0, 5.0) };
            _signalRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Signal>())).ReturnsAsync(new Signal(signalType, amplitude, frequency, pointsCount, points));

            var result = await _signalService.GenerateAndSaveToDbAsync(signalType, amplitude, frequency, pointsCount);

            _signalRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Signal>()), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SignalType, Is.EqualTo(signalType));
        }

        [Test]
        public void GetMax_ShouldReturnMaxValue_WhenValid()
        {
            var points = new List<SignalPoint>
            {
                new SignalPoint(0, 1),
                new SignalPoint(1, 3),
                new SignalPoint(2, -1)
            };

            _signalProcessorMock.Setup(p => p.GetMax(It.IsAny<List<SignalPoint>>())).Returns(3.0);

            var result = _signalService.GetMax(points);

            Assert.That(result, Is.EqualTo(3.0));
            _signalProcessorMock.Verify(p => p.GetMax(points), Times.Once);
        }

        [Test]
        public void GetMin_ShouldReturnMinValue_WhenValid()
        {
            var points = new List<SignalPoint>
            {
                new SignalPoint(0, 1),
                new SignalPoint(1, 3),
                new SignalPoint(2, -1)
            };

            _signalProcessorMock.Setup(p => p.GetMin(It.IsAny<List<SignalPoint>>())).Returns(-1.0);

            var result = _signalService.GetMin(points);


            Assert.That(result, Is.EqualTo(-1.0));
            _signalProcessorMock.Verify(p => p.GetMin(points), Times.Once);
        }

        [Test]
        public void GetAverage_ShouldReturnAverageValue_WhenValid()
        {
            var points = new List<SignalPoint>
            {
                new SignalPoint(0, 1),
                new SignalPoint(1, 3),
                new SignalPoint(2, 5)
            };

            _signalProcessorMock.Setup(p => p.GetAverage(points)).Returns(3.0);

            var result = _signalService.GetAverage(points);

            Assert.That(3.0, Is.EqualTo(result));
        }

        [Test]
        public void ZeroCrossingsCount_ShouldReturnZeroCrossingsCount_WhenValid()
        {
            var points = new List<SignalPoint>
            {
                new SignalPoint(0, 1),
                new SignalPoint(1, -1),
                new SignalPoint(2, 1)
            };

            _signalProcessorMock.Setup(p => p.ZeroCrossingsCount(points)).Returns(2);

            var result = _signalService.ZeroCrossingsCount(points);

            Assert.That(2, Is.EqualTo(result));
        }
    }
}
