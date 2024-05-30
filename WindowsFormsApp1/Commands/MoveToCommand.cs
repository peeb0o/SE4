using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    public class MoveToCommand : Command
    {
        private VariableManager variableManager;
        public MoveToCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

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
