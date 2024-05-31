using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE4.Exceptions;

namespace SE4
{
    /// <summary>
    /// Class for parsing fill command passed from CommandParser class
    /// </summary>
    public class FillCommand : Command
    {
        /// <summary>
        /// Executes the fill command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw the shape. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the string indicating if fill is on or off. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the fill mode is unchanged. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            //Should be exactly 2
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed in fill command. Syntax: fill <on/off>");
            }

            //Check if parameter sent is either off or on and set fill value accordingly, otherwise throw an exception
            string option = parameters[1];

            if (parameters[1].ToLower() == "on" && !syntaxCheck)
            {
                shapeFactory.SetFillValue(true);
            }
            else if (parameters[1].ToLower() == "off" && !syntaxCheck)
            {
                shapeFactory.SetFillValue(false);
            }
            else if (parameters[1] != "on" || parameters[1] != "off")
            {
                throw new CommandException($"Invalid value {parameters[1]} passed. Syntax: fill <on/off>.");
            }
        }
    }
}
