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


        public ShapeFactory(Panel panel)
        {
            drawPanel = panel;
            drawPanel.Paint += DrawPanel_Paint;
        }
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Draw shapes onto the panel when it is painted
            DrawShapes(e.Graphics);
        }

        public void ExecuteCommand(Command command, string[] parameters)
        {
            command.Execute(this, parameters);
            drawPanel.Invalidate(); // Request a repaint of the panel
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            drawPanel.Invalidate(); // Request a repaint of the panel
        }

        public void RemoveShape(Shape shape)
        {
            shapes.Remove(shape);
            drawPanel.Invalidate(); // Request a repaint of the panel
        }

        public void ClearShapes()
        {
            shapes.Clear();
            drawPanel.Invalidate(); // Request a repaint of the panel
        }

        public void DrawShapes(Graphics graphics)
        {
            foreach (var shape in shapes)
            {
                shape.draw(graphics);
            }
        }
    }
}
