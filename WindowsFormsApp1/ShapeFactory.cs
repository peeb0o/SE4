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
        private readonly Panel drawingPanel;

        public ShapeFactory(Panel panel)
        {
            drawingPanel = panel;
        }

        public void DrawLine(Point startPoint, Point endPoint)
        {
            Graphics graphics = drawingPanel.CreateGraphics();
            graphics.DrawLine(Pens.Black, startPoint, endPoint);
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            Graphics graphics = drawingPanel.CreateGraphics();
            graphics.DrawRectangle(Pens.Black, rectangle);
        }

        public void DrawEllipse(Rectangle rectangle)
        {
            Graphics graphics = drawingPanel.CreateGraphics();
            graphics.DrawEllipse(Pens.Black, rectangle);
        }
    }
}
