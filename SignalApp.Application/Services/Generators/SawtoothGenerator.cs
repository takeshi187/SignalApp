using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;

namespace SignalApp.ApplicationServices.Services.Generators
{
    public class SawtoothGenerator : ISignalGenerator
    {
        public SignalTypeEnum SignalType => SignalTypeEnum.Sawtooth;

        public List<SignalPoint> Generate(double amplitude, double frequency, int pointsCount)
        {
            var points = new List<SignalPoint>(pointsCount);
            double dt = 1.0 / pointsCount; // время (t) между соседними точками (timeStep) разделить 1.0 = 1 сек, pointsCount = на n частей.

            for (int i = 0; i < pointsCount; i++)
            {
                double time = i * dt; // время (t) для текущей точки (i).
                double value = amplitude * (2 * (frequency * time - Math.Floor(frequency * time + 0.5))); // y(t)=A⋅(2⋅(f t−floor(f t +0.5)))
                points.Add(new SignalPoint(time, value));
            }

            return points;
        }
    }
}
