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
        public Panel drawPanel;
        private List<Shape> shapes = new List<Shape>();
        private Bitmap drawBitmap;

        public ShapeFactory(Panel panel)
        {
            drawPanel = panel;
            drawBitmap = new Bitmap(panel.Width, panel.Height);
            drawPanel.Paint += DrawPanel_Paint;
        }
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Draw shapes onto the panel when it is painted
            e.Graphics.DrawImage(drawBitmap, Point.Empty);
        }

        public void ExecuteCommand(Command command, string[] parameters)
        {
            command.Execute(this, parameters);
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            RedrawBitmap();
        }

        public void RemoveShape(Shape shape)
        {
            shapes.Remove(shape);
            RedrawBitmap();
        }

        public void ClearShapes()
        {
            shapes.Clear();
            RedrawBitmap();
        }

        public void RedrawBitmap()
        {
            using (Graphics graphics = Graphics.FromImage(drawBitmap))
            {
                foreach (var shape in shapes)
                {
                    shape.draw(graphics);
                }
            }

            drawPanel.Invalidate();
        }
    }
}
