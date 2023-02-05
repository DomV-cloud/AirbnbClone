using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists() : base() { }
        public UserAlreadyExists(string message) : base(message) { }
        public UserAlreadyExists(string message, Exception innerException) : base(message, innerException) { }

    }
}
