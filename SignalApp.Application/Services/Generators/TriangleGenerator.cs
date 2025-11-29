using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;

namespace SignalApp.ApplicationServices.Services.Generators
{
    public class TriangleGenerator : ISignalGenerator
    {
        public SignalTypeEnum SignalType => SignalTypeEnum.Triangle;

        public List<SignalPoint> Generate(double amplitude, double frequency, int pointsCount)
        {
            var points = new List<SignalPoint>(pointsCount);
            double dt = 1.0 / pointsCount; // время (t) между соседними точками (timeStep) разделить 1.0 = 1 сек, pointsCount = на n частей.

            for (int i = 0; i < pointsCount; i++)
            {
                double time = i * dt; // время (t) для текущей точки (i).
                double value = (2 * amplitude / Math.PI) * Math.Asin(Math.Sin(2 * Math.PI * frequency * time)); // y(t) = (2A / π) * asin(sin(2π f t))
                points.Add(new SignalPoint(time, value));
            }

            return points;
        }
    }
}
