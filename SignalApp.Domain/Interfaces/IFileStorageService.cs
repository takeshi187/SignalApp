using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Interfaces
{
    public interface IFileStorageService
    {
        string SaveToTxt(
            string directory,
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint> points);
    }
}
