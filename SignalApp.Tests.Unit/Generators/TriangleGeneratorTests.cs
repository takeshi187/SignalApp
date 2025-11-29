using SignalApp.ApplicationServices.Services.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Tests.Unit.Generators
{
    [TestFixture]
    public class TriangleGeneratorTests
    {
        [Test]
        public void Generate_ShouldProduceTriangleWave()
        {
            var gen = new TriangleGenerator();

            var result = gen.Generate(1, 1, 50);

            var max = result.Max(p => p.Value);
            var min = result.Min(p => p.Value);

            Assert.That(max, Is.EqualTo(1).Within(0.05));
            Assert.That(min, Is.EqualTo(-1).Within(0.05));
        }
    }
}
