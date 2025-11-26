using SignalApp.Application.Interfaces;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Application.Services
{
    public class SignalService : ISignalService
    {
        private readonly ISignalValidator _validator;
        private readonly ISignalProcessor _processor;
        private readonly IEnumerable<ISignalGenerator> _generators;

        public SignalService(ISignalValidator validator,
            IEnumerable<ISignalGenerator> generators,
            ISignalProcessor processor)
        {
            _validator = validator;
            _generators = generators;
            _processor = processor;
        }

        public List<SignalPoint> Generate(SignalTypeEnum signalType, double amplitude, double frequency, int pointsCount)
        {       
            _validator.Validate(amplitude, frequency, pointsCount);

            var generator = _generators.FirstOrDefault(g => g.SignalType == signalType);

            if (generator == null)
                throw new Exception($"Генератор для сигнала {signalType} не найден.");

            return generator.Generate(amplitude, frequency, pointsCount);
        }

        public double GetMax(List<SignalPoint> points)
        {
            return _processor.GetMax(points);
        }

        public double GetMin(List<SignalPoint> points)
        {
            return _processor.GetMin(points);
        }

        public double GetAverage(List<SignalPoint> points)
        {
            return _processor.GetAverage(points);
        }

        public int ZeroCrossingsCount(List<SignalPoint> points)
        {
            return _processor.ZeroCrossingsCount(points);
        }
    }
}
