using SignalApp.ApplicationServices.Exceptions;
using SignalApp.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.ApplicationServices.Services
{
    public class SignalValidator : ISignalValidator
    {
        public void Validate(double amplitude, double frequency, int pointsCount)
        {
            if (amplitude <= 0)
                throw new SignalValidationException("Амплитуда должна быть больше нуля.");

            if (frequency <= 0)
                throw new SignalValidationException("Частота должна быть больше нуля.");

            if(pointsCount < 100 || pointsCount > 10000)
                throw new SignalValidationException("Количество точек сигнала должно быть в пределах от 100 до 10000.");
        }
    }
}
