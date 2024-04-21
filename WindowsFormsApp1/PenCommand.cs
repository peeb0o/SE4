using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class PenCommand : Command
    {

        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length == 2)
            {
                string[] coordinates = parameters[1].Split(',');

                if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
                {
                    shapeFactory.MovePen(x, y);
                }

            }
        }
    }
}
