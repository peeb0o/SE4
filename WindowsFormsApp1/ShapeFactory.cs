using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4
{
    public class ShapeFactory
    {
        private Panel drawPanel;

        public ShapeFactory(Panel panel)
        {
            drawPanel = panel;
        }

        public void DrawLine(Point startPoint, Point endPoint)
        {
            Graphics graphics = drawPanel.CreateGraphics();
            graphics.DrawLine(Pens.Black, startPoint, endPoint);
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            Graphics graphics = drawPanel.CreateGraphics();
            graphics.DrawRectangle(Pens.Black, rectangle);
        }

        public void DrawEllipse(Rectangle rectangle)
        {
            Graphics graphics = drawPanel.CreateGraphics();
            graphics.DrawEllipse(Pens.Black, rectangle);
        }
    }
}
