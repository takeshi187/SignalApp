using SignalApp.ApplicationServices.Interfaces;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;

namespace SignalApp.ApplicationServices.Services
{
    public class SignalService : ISignalService
    {
        private readonly ISignalValidator _validator;
        private readonly ISignalProcessor _processor;
        private readonly IEnumerable<ISignalGenerator> _generators;
        private readonly ISignalRepository _signalRepository;
        private readonly IFileStorageService _fileStorage;

        public SignalService(ISignalValidator validator,
            IEnumerable<ISignalGenerator> generators,
            ISignalProcessor processor,
            ISignalRepository signalRepository,
            IFileStorageService fileStorage)
        {
            _validator = validator;
            _generators = generators;
            _processor = processor;
            _signalRepository = signalRepository;
            _fileStorage = fileStorage;
        }

        public List<SignalPoint> Generate(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount)
        {
            _validator.Validate(amplitude, frequency, pointsCount);

            var generator = _generators.FirstOrDefault(g => g.SignalType == signalType);

            if (generator == null)
                throw new Exception($"Генератор для сигнала {signalType} не найден.");

            return generator.Generate(amplitude, frequency, pointsCount);
        }

        public async Task<Signal> GenerateAndSaveToDbAsync(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount)
        {
            var points = Generate(signalType, amplitude, frequency, pointsCount);

            var signal = new Signal(signalType, amplitude, frequency, pointsCount, points);

            return await _signalRepository.AddAsync(signal);
        }

        public string GenerateAndSaveToFile(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount)
        {
            var points = Generate(signalType, amplitude, frequency, pointsCount);

            var signal = new Signal(
                signalType,
                amplitude,
                frequency,
                pointsCount,
                points);

            return _fileStorage.SaveToTxt(
                signalType,
                amplitude,
                frequency,
                pointsCount,
                points);
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
