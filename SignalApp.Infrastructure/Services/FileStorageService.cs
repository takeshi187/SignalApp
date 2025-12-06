using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using System.Globalization;
using System.IO;

namespace SignalApp.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IPathProviderService _pathProvider;

        public FileStorageService(IPathProviderService pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public string SaveToTxt(
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint> points)
        {
            string signalsDirectory = _pathProvider.GetSignalsDirectory();

            if(!Directory.Exists(signalsDirectory))
                Directory.CreateDirectory(signalsDirectory);

            string filename = $"{signalType}_A{amplitude}_F{frequency}_P{pointsCount}_{DateTime.UtcNow:yyyyMMdd}.txt";
            string filepath = Path.Combine(signalsDirectory, filename);

            using var writer = new StreamWriter(filepath); // using для автозакрытия файла.

            writer.WriteLine($"SignalType={signalType}");
            writer.WriteLine($"Amplitude={amplitude}");
            writer.WriteLine($"Frequency={frequency}");
            writer.WriteLine($"PointsCount={pointsCount}");
            writer.WriteLine("time\tvalue");

            foreach (var p in points)
            {
                writer.WriteLine($"{p.Time.ToString(CultureInfo.InvariantCulture)}\t{p.Value.ToString(CultureInfo.InvariantCulture)}");
                // CultureInfo для сохранения данных в формате, не зависящем от культуры.
            }

            return filepath;
        }      
    }
}
