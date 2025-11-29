using SignalApp.ApplicationServices.Services.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Tests.Unit.Generators
{
    [TestFixture]
    public class SawtoothGeneratorTests
    {
        [Test]
        public void SawtoothGenerator_ShouldGenerateSignal_WhenValid()
        {
            var gen = new SawtoothGenerator();

            var result = gen.Generate(1, 1, 100);

            Assert.That(result.Max(p => p.Value), Is.GreaterThan(0.7));
            Assert.That(result.Min(p => p.Value), Is.LessThan(-0.7));
        }
    }
}
