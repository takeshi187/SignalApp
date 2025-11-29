using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Database;
using SignalApp.Infrastructure.Repositories;
using SignalApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Tests.Integration
{
    [TestFixture]
    public class FileStorageServiceTests
    {
        private FileStorageService _storageService;
        private string _testDirectory;

        [SetUp]
        public void Setup()
        {
            _storageService = new FileStorageService();
            _testDirectory = "TestSignals";

            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [Test]
        public void SaveToTxt_ShouldCreateFile_WhenValid()
        {
            var points = new List<SignalPoint>
        {
            new SignalPoint(0, 1),
            new SignalPoint(1, -1),
        };

            string path = _storageService.SaveToTxt(
                directory: _testDirectory,
                signalType: SignalTypeEnum.Sine,
                amplitude: 1,
                frequency: 2,
                pointsCount: 2,
                points: points
            );

            Assert.That(File.Exists(path), Is.True);

            string text = File.ReadAllText(path);

            Assert.That(text.Contains("SignalType=Sine"), Is.True);
            Assert.That(text.Contains("Amplitude=1"), Is.True);
            Assert.That(text.Contains("PointsCount=2"), Is.True);
            Assert.That(text.Contains("time\tvalue"), Is.True);
        }
    }
}

