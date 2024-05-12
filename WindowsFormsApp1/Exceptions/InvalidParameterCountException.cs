using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Exceptions
{
    public class InvalidParameterCountException : CommandException
    {
        public InvalidParameterCountException(string message) : base(message)
        {
        }
    }
}
