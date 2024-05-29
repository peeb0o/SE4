using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE4.Exceptions;

namespace SE4
{
    public class FillCommand : Command
    {
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {

            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed in fill command. Syntax: fill <on/off>");
            }

            
            string option = parameters[1];

            if (parameters[1].ToLower() == "on" && !syntaxCheck)
            {
                shapeFactory.SetFillValue(true);
            }
            else if (parameters[1].ToLower() == "off" && !syntaxCheck)
            {
                shapeFactory.SetFillValue(false);
            }
            else
            {
                throw new CommandException("Invalid value passed. Please set fill value to either on or off.");
            }
        }
    }
}
