using System;

namespace SJDI {
    public class MissingRegistrationException : Exception
    {
        public MissingRegistrationException(string message) : base(message)
        {
        }
    }
}