using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    public class CircleCommand : Command
    {
        private Graphics graphics;
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();
            if (parameters.Length == 2)
            {
               if (int.TryParse(parameters[1], out int radius))
                {
                    Circle circle = new Circle(System.Drawing.Color.Aquamarine, shapeFactory.penX, shapeFactory.penY, radius);
                    shapeFactory.AddShape(circle);
                }
            }
        }
    }
}
