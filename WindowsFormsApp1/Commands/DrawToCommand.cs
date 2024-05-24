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
            if (parameters.Length == 2)
            {
                string[] coordinates = parameters[1].Split(',');

                int x = 0;
                int y = 0;


                // Check if the first coordinate is a variable or a literal
                if (variableManager.VariableExists(coordinates[0].Trim()))
                {
                    x = variableManager.GetVariableValue(coordinates[0].Trim());
                }
                else if (!int.TryParse(coordinates[0].Trim(), out x))
                {
                    // Handle invalid coordinate input
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid X coordinate");
                    return;
                }

                // Check if the second coordinate is a variable or a literal
                if (variableManager.VariableExists(coordinates[1].Trim()))
                {
                    y = variableManager.GetVariableValue(coordinates[1].Trim());
                }
                else if (!int.TryParse(coordinates[1].Trim(), out y))
                {
                    // Handle invalid coordinate input
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid Y coordinate");
                    return;
                }

                //Draw line
                DrawTo line = new DrawTo(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, x, y);
                shapeFactory.AddShape(line);
                shapeFactory.MovePen(x, y);
            }
            else
            {
                PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid number of parameters");
                throw new InvalidParameterCountException("Invalid number of parameters");
            }
        }
    }
}
