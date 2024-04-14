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
    public class DrawLineCommand : Command
    {
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length == 3)
            {
                string[] startPoint = parameters[1].Split(',');
                string[] endPoint = parameters[2].Split(',');

                if (startPoint.Length == 2 && endPoint.Length == 2 &&
                    int.TryParse(startPoint[0], out int startX) &&
                    int.TryParse(startPoint[1], out int startY) &&
                    int.TryParse(endPoint[0], out int endX) &&
                    int.TryParse(endPoint[1], out int endY))
                {
                    var line = new Line(new Point(startX, startY), new Point(endX, endY));
                    shapeFactory.AddShape(line);
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
