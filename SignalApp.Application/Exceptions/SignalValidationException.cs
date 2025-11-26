using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Application.Exceptions
{
    public class SignalValidationException : Exception
    {
        public SignalValidationException(string message) : base(message) { }
    }
}
