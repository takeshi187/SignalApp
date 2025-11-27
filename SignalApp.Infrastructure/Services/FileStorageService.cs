using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SignalApp.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        public string SaveToTxt(
            string directory,
            SignalTypeEnum signalType,
            double amplitude,
            double frequency,
            int pointsCount,
            List<SignalPoint> points)
        {
            if(!Directory.Exists(directory)) 
                Directory.CreateDirectory(directory);

            string filename = $"{signalType}_A{amplitude}_F{frequency}_P{pointsCount}_{DateTime.UtcNow:yyyyMMdd}.txt";
            string filepath = Path.Combine(directory, filename);

            using var writer = new StreamWriter(filepath); // using для атозакрытия файла.

            writer.WriteLine($"SignalType={signalType}");
            writer.WriteLine($"Amplitude={amplitude}");
            writer.WriteLine($"Frequency={frequency}");
            writer.WriteLine($"PointsCount={pointsCount}");
            writer.WriteLine("time\tvalue");

            foreach(var p in points)
            {
                writer.WriteLine($"{p.Time.ToString(CultureInfo.InvariantCulture)}\t{p.Value.ToString(CultureInfo.InvariantCulture)}");
                // CultureInfo для сохранения данных в формате, не зависящем от культуры.
            }

            return filepath;
        }
    }
}
