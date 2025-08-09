using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.DataAccess
{
    public class ActivationException : Exception
    {
        public ActivationException() { }

        public ActivationException(string message)
            : base(message)
        {

        }

        public ActivationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
