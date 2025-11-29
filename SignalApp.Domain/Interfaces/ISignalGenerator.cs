using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;

namespace SignalApp.Domain.Interfaces
{
    public interface ISignalGenerator
    {
        SignalTypeEnum SignalType { get; }

        List<SignalPoint> Generate(
            double amplitude,
            double frequency,
            int pointsCount);
    }
}
