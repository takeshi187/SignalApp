using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Models
{
    public class SignalPoint
    {
        public int SignalPointId { get; private set; }

        public double Time { get; private set; } // x - момент времени, на котором измеряется сигнал (частота, измерение кол-ва времени между точками).
        public double Value { get; private set; } // y - значение сигнала в момент времени (форма сигнала, физическое значение сигнала).
                                                  
        public int SignalId { get; private set; }
        public Signal Signal { get; private set; }

        public SignalPoint(double time, double value)
        {
            Time = time;
            Value = value;
        }

        public void SetSignal(Signal signal)
        {
            Signal = signal;
            SignalId = signal.SignalId;
        }

        private SignalPoint() { }
    }
}
