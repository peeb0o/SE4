using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    /// <summary>
    /// Class for parsing moveto command passed from CommandParser class
    /// </summary>
    public class MoveToCommand : Command
    {
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of MoveToCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to check value of coordinates passed to see if they are literals or variables. </param>
        public MoveToCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the moveto command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to move the pen to the desired coordinates. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the coordinates for the pen to move to. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the move operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed for moveto command. Syntax: moveto <x,y>");
            }

            string[] coordinates = parameters[1].Split(',');

            if (coordinates.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of coordinates passed for moveto command. Syntax: moveto <x,y>");

            }

            if (string.IsNullOrWhiteSpace(coordinates[1]) || string.IsNullOrWhiteSpace(coordinates[0]))
            {
                throw new InvalidParameterCountException("Coordinate cannot be an empty string");
            }

            //Check if coordinate is a variable or literal
            int x = GetCoordinateValue(coordinates[0]);
            int y = GetCoordinateValue(coordinates[1]);

            if (!syntaxCheck)
                shapeFactory.MovePen(x, y);

        }

        /// <summary>
        /// Method for checking whether the coordinate value passed is a literal value or a variable.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        private int GetCoordinateValue(string coordinate)
        {
            //Check if variable
            if (variableManager.VariableExists(coordinate))
            {
                //Return variable value
                return variableManager.GetVariableValue(coordinate);
            }

            //Otherwise tryparse int
            if (int.TryParse(coordinate, out int value))
            {
                return value;
            }

            //Throw exception in case invalid coordinate passed
            throw new CommandException($"Invalid dimension value: {coordinate}");
        }
    }
}
