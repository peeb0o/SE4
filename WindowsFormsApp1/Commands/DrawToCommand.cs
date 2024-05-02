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
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length == 2)
            {
                string[] coordinates = parameters[1].Split(',');

                if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
                {

                    DrawTo line = new DrawTo(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, x, y);
                    shapeFactory.AddShape(line);
                    shapeFactory.MovePen(x, y);

                } else
                {
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid number of parameters");
                }
            }
            else
            {
                PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid number of parameters");
            }
        }
    }
}
