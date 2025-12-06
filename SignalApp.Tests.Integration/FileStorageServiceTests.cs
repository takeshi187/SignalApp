using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Services;

namespace SignalApp.Tests.Integration
{
    [TestFixture]
    public class FileStorageServiceTests
    {
        private IFileStorageService _storageService;
        private IPathProviderService _pathProviderService;

        [SetUp]
        public void Setup()
        {
            _pathProviderService = new PathProviderService();
            _storageService = new FileStorageService(_pathProviderService);
        }

        [Test]
        public void SaveToTxt_ShouldCreateFile_AndReturnPath()
        {
            var points = new List<SignalPoint>
            {
                new SignalPoint(0, 1),
                new SignalPoint(1, -1),
            };

            var filePath = _storageService.SaveToTxt(
                SignalTypeEnum.Sine,
                amplitude: 1,
                frequency: 1,
                pointsCount: 2,
                points
            );

            Assert.That(File.Exists(filePath), Is.True);
        }
    }
}

