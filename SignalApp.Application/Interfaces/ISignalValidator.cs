using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.ApplicationServices.Interfaces
{
    public interface ISignalValidator
    {
        void Validate(double amplitude, double frequency, int pointsCount);
    }
}
