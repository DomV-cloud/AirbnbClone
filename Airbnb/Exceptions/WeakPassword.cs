using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class WeakPassword : Exception
    {
        public WeakPassword() : base() { }
        public WeakPassword(string message) : base(message) { }
        public WeakPassword(string message, Exception innerException) : base(message, innerException) { }

    }
}
