using SignalApp.ApplicationServices.Services.Generators;

namespace SignalApp.Tests.Unit.Generators
{
    [TestFixture]
    public class SquareGeneratorTests
    {
        [Test]
        public void SquareGenerator_ShouldGenerateSignal_WhenValid()
        {
            var gen = new SquareGenerator();

            var result = gen.Generate(2, 1, 10);

            Assert.That(result.Any(p => p.Value == 2));
            Assert.That(result.Any(p => p.Value == -2));
        }
    }
}
