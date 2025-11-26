using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Models
{
    public class SignalPoint
    {
        public double Time { get; private set; } // x - момент времени, на котором измеряется сигнал (частота, измерение кол-ва времени между точками).
        public double Value { get; private set; } // y - значение сигнала в момент времени (форма сигнала, физическое значение сигнала).

        public SignalPoint(double time, double value)
        {
            Time = time;
            Value = value;
        }
    }
}
