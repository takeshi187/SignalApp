using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SignalApp.Domain.Interfaces;
using SignalApp.Infrastructure.Services;

namespace SignalApp.Tests.Unit
{
    [TestFixture]
    public class PathProviderServiceTests
    {
        private IPathProviderService _pathProviderService;

        [SetUp]
        public void Setup()
        {
            _pathProviderService = new PathProviderService();
        }

        [Test]
        public void GetBaseDirectory_ShouldReturnExistingDirectory_WhenValid()
        {
            var path = _pathProviderService.GetBaseDirectory();

            Assert.That(path, Is.Not.Null);
            Assert.That(Directory.Exists(path), Is.True);
        }

        [Test]
        public void GetSignalsDirectory_ShouldCreateDirectory_IfNotExists()
        {
            var path = _pathProviderService.GetSignalsDirectory();

            Assert.That(Directory.Exists(path), Is.True);
        }
    }
}
