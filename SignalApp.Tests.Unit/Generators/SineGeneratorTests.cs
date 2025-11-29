using SignalApp.ApplicationServices.Services;
using SignalApp.ApplicationServices.Services.Generators;
using SignalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Tests.Unit.Generators
{
    [TestFixture]
    public class SineGeneratorTests
    {
        [Test]
        public void SinusGenerator_ShouldGenerateSignal_WhenValid()
        {
            var gen = new SineGenerator();

            var result = gen.Generate(1, 1, 5);

            Assert.That(result[0].Value, Is.EqualTo(0).Within(0.0001));
            Assert.That(result.Count, Is.EqualTo(5));
        }
    }
}
