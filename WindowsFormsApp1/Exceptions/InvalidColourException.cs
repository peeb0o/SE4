using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Exceptions
{
    public class InvalidColourException : CommandException
    {
        public InvalidColourException(string message) : base(message)
        {
        }
    }
}
