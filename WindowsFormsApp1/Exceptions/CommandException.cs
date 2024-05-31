using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Exceptions
{
    /// <summary>
    /// Exception class which represents errors that occur during command execution or parsing.
    /// </summary>
    public class CommandException : Exception
    {
        /// <summary>
        /// Initialises an instance of the CommandException class
        /// </summary>
        /// <param name="message"> Message which describes the error being thrown. </param>
        public CommandException(string message) : base(message)
        {
        }
    }
}
