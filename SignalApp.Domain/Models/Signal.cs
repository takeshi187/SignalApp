using SignalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Models
{
    public class Signal
    {
        public int SignalId { get; private set; }
        public SignalTypeEnum SignalType { get; private set; }
        public double Amplitude { get; private set; }
        public double Frequency { get; private set; }
        public int PointsCount {  get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<SignalPoint> Points { get; private set; } = new();

        public Signal(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint>points)
        {
            SignalType = signalType;
            Amplitude = amplitude;
            Frequency = frequency;
            PointsCount = pointsCount;
            CreatedAt = DateTime.UtcNow;
            Points = points;
        }

        private Signal() { }
    }
}
