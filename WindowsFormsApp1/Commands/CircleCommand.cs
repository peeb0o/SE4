using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SE4.Variables;
using SE4.Exceptions;

namespace SE4
{
    /// <summary>
    /// Class for parsing circle draw commands passed from CommandParser class
    /// </summary>
    public class CircleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of CircleCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to check value of radius passed to see if it is a literal or variable. </param>
        public CircleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the circle drawing command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw the shape. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the radius of the circle to be drawn. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();

            //Check parameters are exactly 2 
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters in circle command. Syntax: Circle <radius>");
            }

            //Split on space, radius will always be second element
            String[] radiusString = parameters[1].Split(' ');

            //Check if radius is a variable or literal
            int radius = GetRadiusValue(parameters[1]);

            //Draw circle
            Circle circle = new Circle(shapeFactory.penColor, shapeFactory.penX - radius, shapeFactory.penY - radius, radius, shapeFactory.fill);

            //Check if in syntax check and draw if not 
            if (!syntaxCheck)
            shapeFactory.AddShape(circle);
        }

        /// <summary>
        /// Method which checks to see if the radius value passed is either a literal or a variable. 
        /// This allows circles to be drawn using variable names rather than a literal integer. 
        /// </summary>
        /// <param name="radius"> The string to be checked for either a literal or variable value. </param>
        /// <returns> Returns the integer value of the radius string checked. </returns>
            private int GetRadiusValue(string radius)
        {
            //Check if variable
            if (variableManager.VariableExists(radius))
            {
                return variableManager.GetVariableValue(radius);
            }

            //Otherwise tryparse int
            if (int.TryParse(radius, out int value))
            {
                return value;
            }

            //Throw exception in case invalid radius passed
            throw new CommandException($"Invalid radius value: {radius}");
        }
    }
}
