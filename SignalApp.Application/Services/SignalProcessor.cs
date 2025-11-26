using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Application.Services
{
    public class SignalProcessor : ISignalProcessor
    {
        public double GetMax(List<SignalPoint> points)
        {
            return points.Max(p => p.Value);
        }

        public double GetMin(List<SignalPoint> points)
        {
            return points.Min(p => p.Value);
        }

        public double GetAverage(List<SignalPoint> points)
        {
            return points.Average(p => p.Value);
        }

        public int ZeroCrossingsCount(List<SignalPoint> points)
        {
            int count = 0;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i - 1].Value <= 0 && points[i].Value > 0 ||
                    points[i - 1].Value >= 0 && points[i].Value < 0) // Проверка значения сигнала между соседними точками,
                                                                     // если у одной точки значение положительное,
                                                                     // а у соседней отрицательное, то это считается пересечением.
                {
                    count++;
                }
            }

            return count;
        }
    }
}
