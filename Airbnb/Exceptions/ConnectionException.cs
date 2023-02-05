using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException() : base() { }
        public ConnectionException(string message) : base(message) { }
        public ConnectionException(string message, Exception innerException) : base(message, innerException) { }

    }
}
