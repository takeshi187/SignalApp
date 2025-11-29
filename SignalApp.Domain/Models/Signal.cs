using SignalApp.Domain.Enums;

namespace SignalApp.Domain.Models
{
    public class Signal
    {
        public int SignalId { get; private set; }
        public SignalTypeEnum SignalType { get; private set; }
        public double Amplitude { get; private set; }
        public double Frequency { get; private set; }
        public int PointsCount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<SignalPoint> Points { get; private set; }

        public Signal(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint> points)
        {
            SignalType = signalType;
            Amplitude = amplitude;
            Frequency = frequency;
            PointsCount = pointsCount;
            CreatedAt = DateTime.UtcNow;
            Points = points;

            foreach (var point in points)
                point.SetSignal(this);
        }

        private Signal() { }
    }
}
