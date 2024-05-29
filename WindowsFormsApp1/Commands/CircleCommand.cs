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
    public class CircleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;

        public CircleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }
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
