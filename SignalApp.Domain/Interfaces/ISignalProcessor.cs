using SignalApp.Domain.Models;

namespace SignalApp.Domain.Interfaces
{
    public interface ISignalProcessor
    {
        double GetMax(List<SignalPoint> points);
        double GetMin(List<SignalPoint> points);
        double GetAverage(List<SignalPoint> points);
        int ZeroCrossingsCount(List<SignalPoint> points);
    }
}
