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
        private Pen pen = new Pen();
        public int penX { get; private set; }
        public int penY { get; private set; }
        public Color penColor { get; private set; } = Color.Black;

        public bool fill { get; private set; } = false;

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

            pen.Draw(e.Graphics, penX, penY);
        }

        public void ExecuteCommand(Command command, string[] parameters)
        {
            command.Execute(this, parameters);
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            RedrawBitmap();
            drawPanel.Refresh();
        }

        public void Clear()
        {
            shapes.Clear();
            
            using (Graphics graphics = Graphics.FromImage(drawBitmap))
            {
                graphics.Clear(SystemColors.ButtonShadow);
            }

            drawPanel.Refresh();
        }

        public void Reset()
        {
            int x = 0;
            int y = 0;
            penX = x;
            penY = y;
            drawPanel.Refresh();
        }

        public void DrawTo(int startX, int startY, int endX, int endY)
        {
            using (Graphics graphics = Graphics.FromImage(drawBitmap))
            {
                graphics.DrawLine(Pens.Black, startX, startY, endX, endY);
            }
        }

        public void MovePen(int x, int y)
        {
            penX = x;
            penY = y;
            drawPanel.Refresh(); 
        }

        public void SetPenColour(Color newColor)
        {
            penColor = newColor;
        }

        public Color GetPenColour()
        {
            return penColor;
        }

        public void SetFillValue(Boolean fillSetting)
        {
            fill = fillSetting;
        }

        public Boolean GetFill()
        {
            return fill;
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
        }
    }
}
