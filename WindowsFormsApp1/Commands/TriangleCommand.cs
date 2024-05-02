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
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();
            if (parameters.Length == 2)
            {
                if (int.TryParse(parameters[1], out int length))
                {
                    Triangle triangle = new Triangle(shapeFactory.penColor, shapeFactory.penX, shapeFactory.penY, length, shapeFactory.fill);
                    shapeFactory.AddShape(triangle);
                }
            }
        }
    }
}
