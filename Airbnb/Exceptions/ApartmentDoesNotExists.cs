using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Exceptions
{
    public class ApartmentDoesNotExists : Exception
    {
        public ApartmentDoesNotExists() : base() { }
        public ApartmentDoesNotExists(string message) : base(message) { }
        public ApartmentDoesNotExists(string message, Exception innerException) : base(message, innerException) { }

    }
}
