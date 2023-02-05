using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class EmailInvalid : Exception
    {
        public EmailInvalid() : base() { }
        public EmailInvalid(string message) : base(message) { }
        public EmailInvalid(string message, Exception innerException) : base(message, innerException) { }

    }
}
