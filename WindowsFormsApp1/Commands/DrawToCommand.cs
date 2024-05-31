using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace SE4
{
    /// <summary>
    /// Class for parsing drawto command passed from CommandParser class
    /// </summary>
    public class DrawToCommand : Command
    {
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of DrawToCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to check value of coordinates passed to see if they are literals or variables. </param>
        public DrawToCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the drawto command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw the shape. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the coordinates for the line shape to be drawn to. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {

            //Check parameters are exactly 2 
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters. Syntax: drawto <x,y>");
            }

            
            //Split coordinates on the comma
            string[] coordinates = parameters[1].Split(',');

            //Check 2 coordinates passed
            if (coordinates.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of coordinates passed");
            }

            //Throw exception if empty or blank space passed as coordinates
            if(string.IsNullOrWhiteSpace(coordinates[1])|| string.IsNullOrWhiteSpace(coordinates[0]))
            {
                throw new InvalidParameterCountException("Coordinate cannot be an empty string");
            }

            //Check if coordinate is a variable or literal
            int x = GetCoordinateValue(coordinates[0]);
            int y = GetCoordinateValue(coordinates[1]);

            //Draw line
            Line line = new Line(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, x, y);

            //Only draw and move pen if not in syntax check
            if (!syntaxCheck)
            {
                shapeFactory.AddShape(line);
                shapeFactory.MovePen(x, y);
            }
            
        }

        /// <summary>
        /// Method which checks to see if the coordinates passed are literal or variable values.
        /// This allows the line to be drawn using both if the user wishes.
        /// </summary>
        /// <param name="coordinate"> The string to be checked for either a literal or variable value. </param>
        /// <returns> Returns the integer value of the passed coordinate string. </returns>
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
