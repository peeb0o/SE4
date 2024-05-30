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
    public class TriangleCommand : Command
    {
        private Graphics graphics;
        private VariableManager variableManager;
        
        public TriangleCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

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

            //Throw exception in case invalid radius passed
            throw new CommandException($"Invalid length value: {length}");
        }

    }   
}
