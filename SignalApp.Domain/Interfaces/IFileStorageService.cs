using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;

namespace SignalApp.Domain.Interfaces
{
    public interface IFileStorageService
    {
        string SaveToTxt(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint> points);
    }
}
