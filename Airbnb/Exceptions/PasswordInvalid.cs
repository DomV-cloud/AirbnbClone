using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class PasswordInvalid : Exception
    {
        public PasswordInvalid() : base() { }
        public PasswordInvalid(string message) : base(message) { }
        public PasswordInvalid(string message, Exception innerException) : base(message, innerException) { }

    }
}
