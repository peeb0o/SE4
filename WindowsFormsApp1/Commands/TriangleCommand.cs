using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// Class for parsing triangle draw commands passed from CommandParser class
    /// </summary>
    public class TriangleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of TriangleCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to check value of sidelength passed to see if it is a literal or variable. </param>
        public TriangleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the triangle drawing command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw the shape. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the sidelength of the triangle to be drawn. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();

            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters");
            }

            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new InvalidParameterCountException($"Invalid side length value: {parameters[1]}");
            }
           
            int length = GetLengthValue(parameters[1]);

            Triangle triangle = new Triangle(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, length, shapeFactory.fill);

            if (!syntaxCheck)
                shapeFactory.AddShape(triangle);
        }

        /// <summary>
        /// Method which checks to see if the length value passed is either a literal or a variable. 
        /// This allows triangles to be drawn using variable names rather than a literal integer. 
        /// </summary>
        /// <param name="length"> The string to be checked for either a literal or variable value. </param>
        /// <returns> Returns the integer value of the length string checked. </returns>
        private int GetLengthValue(string length)
        {
            //Check if variable
            if (variableManager.VariableExists(length))
            {
                return variableManager.GetVariableValue(length);
            }

            //Otherwise tryparse int
            if (int.TryParse(length, out int value))
            {
                return value;
            }

            //Throw exception in case invalid length passed
            throw new CommandException($"Invalid length value: {length}");
        }

    }   
}
