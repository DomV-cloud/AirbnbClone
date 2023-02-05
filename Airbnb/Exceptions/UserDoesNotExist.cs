using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class UserDoesNotExist : Exception
    {
        public UserDoesNotExist() : base() { }
        public UserDoesNotExist(string message) : base(message) { }
        public UserDoesNotExist(string message, Exception innerException) : base(message, innerException) { }

    }
}
