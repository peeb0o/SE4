using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SE4.Variables;

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
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();
            if (parameters.Length == 2)
            {
               String radiusString = parameters[1].Trim();
               int radius = 0;

                if (variableManager.VariableExists(radiusString))
                {
                    radius = variableManager.GetVariableValue(radiusString);
                }
                else
                {
                    radius = int.Parse(radiusString);
                }

               Circle circle = new Circle(shapeFactory.penColor, shapeFactory.penX- radius, shapeFactory.penY - radius, radius, shapeFactory.fill);
               shapeFactory.AddShape(circle);
                
            }
        }
    }
}
