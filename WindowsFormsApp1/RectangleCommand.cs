using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class RectangleCommand : Command
    {
        private Graphics graphics;
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            graphics = shapeFactory.drawPanel.CreateGraphics();
            if (parameters.Length == 2)
            {
                string[] coordinates = parameters[1].Split(',');

                if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int width) && int.TryParse(coordinates[1], out int height))
                {
                    Rectangle rect = new Rectangle(shapeFactory.penColor, shapeFactory.penX - (width /2), shapeFactory.penY - (height /2), width, height, shapeFactory.fill);
                    shapeFactory.AddShape(rect);
                }
            }
        }
    }
}
