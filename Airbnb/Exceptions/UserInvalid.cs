using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class UserInvalid : Exception
    {
        public UserInvalid() : base() { }
        public UserInvalid(string message) : base(message) { }
        public UserInvalid(string message, Exception innerException) : base(message, innerException) { }

    }
}
