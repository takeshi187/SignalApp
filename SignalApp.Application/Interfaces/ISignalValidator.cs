using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Application.Interfaces
{
    public interface ISignalValidator
    {
        void Validate(double amplitude, double frequency, int pointsCount);
    }
}
