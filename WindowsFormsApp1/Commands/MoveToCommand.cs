using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    public class MoveToCommand : Command
    {
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed for moveto command. Syntax: moveto <x,y>");
            }

            string[] coordinates = parameters[1].Split(',');

            if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
            {
                if (!syntaxCheck)
                    shapeFactory.MovePen(x, y);
            }
            else if (!int.TryParse(coordinates[0], out int x1) || !int.TryParse(coordinates[1], out int y1))
            {
                throw new CommandException("Invalid values passed for moveto command.");
            }
        }
    }
}
