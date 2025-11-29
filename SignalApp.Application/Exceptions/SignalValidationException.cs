namespace SignalApp.ApplicationServices.Exceptions
{
    public class SignalValidationException : Exception
    {
        public SignalValidationException(string message) : base(message) { }
    }
}
