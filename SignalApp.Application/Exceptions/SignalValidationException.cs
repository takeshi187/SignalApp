using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.ApplicationServices.Exceptions
{
    public class SignalValidationException : Exception
    {
        public SignalValidationException(string message) : base(message) { }
    }
}
