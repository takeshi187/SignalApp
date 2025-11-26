using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Application.Interfaces
{
    public interface ISignalService
    {
        List<SignalPoint> Generate(SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount);

        double GetMax(List<SignalPoint> points);
        double GetMin(List<SignalPoint> points);
        double GetAverage(List<SignalPoint> points);
        int ZeroCrossingsCount(List<SignalPoint> points);
    }
}
