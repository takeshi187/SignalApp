using SignalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Models
{
    public class Signal
    {
        public int SignalId { get; set; }
        public SignalTypeEnum SignalType { get; set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public int PointsCount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<SignalPoint> Points { get; set; } = new();
    }
}
