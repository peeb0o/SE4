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
    /// Class for parsing rectangle draw commands passed from CommandParser class
    /// </summary>
    public class RectangleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of RectangleCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to check value of dimension passed to see if it is a literal or variable. </param>
        public RectangleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the rectangle drawing command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw the shape. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the width and height of the rectangle to be drawn. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();

            //Check parameters are exactly 2 
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters for drawing a rectangle. Syntax: rectangle <width,height>");
            }

            
            //Split dimensions on the comma
            string[] dimensions = parameters[1].Split(',');

            //Check correct number of dimensions
            if (dimensions.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of dimensions passed. Please pass width and height.");
            }

            if (string.IsNullOrWhiteSpace(dimensions[0]) || string.IsNullOrWhiteSpace(dimensions[1]))
            {
                throw new InvalidParameterCountException("Invalid dimension passed. Please pass width and height.");
            }

            //Call method to check if variable or literal
            int width = GetDimensionValue(dimensions[0]);
            int height = GetDimensionValue(dimensions[1]);

            //Draw rectangle
            Rectangle rect = new Rectangle(shapeFactory.penColor, shapeFactory.penX - (width /2), shapeFactory.penY - (height /2), width, height, shapeFactory.fill);
            
            if(!syntaxCheck)
            shapeFactory.AddShape(rect);
        }

        /// <summary>
        /// Method which checks to see if the dimension value passed is either a literal or a variable. 
        /// This allows rectangles to be drawn using variable names rather than a literal integer. 
        /// </summary>
        /// <param name="dimension"> The string to be checked for either a literal or variable value. </param>
        /// <returns> Returns the integer value of the dimension string checked. </returns>
        private int GetDimensionValue(string dimension)
        {
            //Check if variable
            if (variableManager.VariableExists(dimension))
            {
                return variableManager.GetVariableValue(dimension);
            }

            //Otherwise tryparse int
            if (int.TryParse(dimension, out int value))
            {
                return value;
            }

            //Throw exception in case invalid dimension passed
            throw new CommandException($"Invalid dimension value: {dimension}");
        }
    }
}
