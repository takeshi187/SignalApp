using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;

namespace SignalApp.ApplicationServices.Interfaces
{
    public interface ISignalService
    {
        List<SignalPoint> Generate(SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount);

        Task<Signal> GenerateAndSaveToDbAsync(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount);

        string GenerateAndSaveToFile(
        SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount);

        double GetMax(List<SignalPoint> points);
        double GetMin(List<SignalPoint> points);
        double GetAverage(List<SignalPoint> points);
        int ZeroCrossingsCount(List<SignalPoint> points);
    }
}
