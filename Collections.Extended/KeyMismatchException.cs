using System;

namespace Asjc.Collections.Extended
{
    public class KeyMismatchException : Exception
    {
        public KeyMismatchException() { }

        public KeyMismatchException(string message) : base(message) { }

        public KeyMismatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}
