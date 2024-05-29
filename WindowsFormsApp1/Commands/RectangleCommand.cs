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
    public class RectangleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;

        public RectangleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

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

            //Call method to check if variable or literal
            int width = GetDimensionValue(dimensions[0]);
            int height = GetDimensionValue(dimensions[1]);

            //Draw rectangle
            Rectangle rect = new Rectangle(shapeFactory.penColor, shapeFactory.penX - (width /2), shapeFactory.penY - (height /2), width, height, shapeFactory.fill);
            
            if(!syntaxCheck)
            shapeFactory.AddShape(rect);
        }

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
