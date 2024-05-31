using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Exceptions
{
    /// <summary>
    /// Exception class which represents errors that occur during colour evaluation.
    /// </summary>
    public class InvalidColourException : CommandException
    {
        /// <summary>
        /// Initialises an instance of the InvalidColourException class
        /// </summary>
        /// <param name="message"> Message which describes the error being thrown. </param>
        public InvalidColourException(string message) : base(message)
        {
        }
    }
}
