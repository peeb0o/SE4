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
    // Command for drawing a line
    public class DrawToCommand : Command
    {
        private VariableManager variableManager;

        public DrawToCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {

            //Check parameters are exactly 2 
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters");
            }

            
            //Split coordinates on the comma
            string[] coordinates = parameters[1].Split(',');

            //Check 2 coordinates passed
            if (coordinates.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of coordinates passed");
            }

            //Check if coordinate is a variable or literal
            int x = GetCoordinateValue(coordinates[0]);
            int y = GetCoordinateValue(coordinates[1]);

            //Draw line
            DrawTo line = new DrawTo(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, x, y);
            shapeFactory.AddShape(line);
            shapeFactory.MovePen(x, y);
            
        }

        private int GetCoordinateValue(string coordinate)
        {
            //Check if variable
            if (variableManager.VariableExists(coordinate))
            {
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
