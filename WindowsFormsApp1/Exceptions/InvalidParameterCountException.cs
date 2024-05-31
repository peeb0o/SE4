using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Exceptions
{
    /// <summary>
    /// Exception class which represents errors that occur during command parsing where an incorrect number of parameters have been passed.
    /// </summary>
    public class InvalidParameterCountException : CommandException
    {
        /// <summary>
        /// Initialises an instance of the InvalidParameterCountException class
        /// </summary>
        /// <param name="message"> Message which describes the error being thrown. </param>
        public InvalidParameterCountException(string message) : base(message)
        {
        }
    }
}
