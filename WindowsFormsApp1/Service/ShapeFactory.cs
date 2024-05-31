using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4
{
    public class ShapeFactory
    {
        public Panel drawPanel;
        public List<Shape> shapes = new List<Shape>();
        private Bitmap drawBitmap;
        private Bitmap flashingBitmap;
        private Pen pen = new Pen();
        public int penX { get; private set; }
        public int penY { get; private set; }
        public Color penColor { get; private set; } = Color.Black;
        public bool fill { get; private set; } = false;
        private Thread flashingThread;
        private bool flashing;
        private Color[] flashingColours;
        private int flashingInterval = 500; //half a second per flash

        public ShapeFactory(Panel panel)
        {
            drawPanel = panel;
            drawBitmap = new Bitmap(panel.Width, panel.Height);
            flashingBitmap = new Bitmap(panel.Width, panel.Height);
            drawPanel.Paint += DrawPanel_Paint;
        }
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Draw shapes onto the panel when it is painted
            e.Graphics.DrawImageUnscaled(flashingBitmap, Point.Empty);
            
            pen.Draw(penColor, e.Graphics, penX, penY);
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            RedrawBitmap();
            drawPanel.Refresh();
        }

        public void StartFlash(Color[] colours)
        {
            flashingColours = colours;
            flashing = true;
            flashingThread = new Thread(new ThreadStart(FlashColours));
            flashingThread.Start();
        }

        public void StopFlash()
        {
            flashing = false;
            if (flashingThread.IsAlive)
            {
                //test this out
                flashingThread.Abort();
            }
        }

        private void FlashColours()
        {
            int index = 0;
            while (flashing)
            {
                using (Graphics g = Graphics.FromImage(flashingBitmap))
                {
                    g.Clear(SystemColors.ButtonShadow);
                    foreach (var shape in shapes)
                    {
                        shape.SetColour(flashingColours[index]);
                        shape.draw(g);
                    }
                }

                drawPanel.Invoke(new Action(() => drawPanel.Refresh()));

                index = (index + 1) % flashingColours.Length;
                Thread.Sleep(flashingInterval);
            }
        }

        public void Clear()
        {
            shapes.Clear();
            
            using (Graphics graphics = Graphics.FromImage(drawBitmap))
            {
                graphics.Clear(SystemColors.ButtonShadow);
            }

            using (Graphics g = Graphics.FromImage(flashingBitmap))
            {
                g.Clear(SystemColors.ButtonShadow);
            }

            PanelUtilities.ClearErrorMessages();
            drawPanel.Refresh();
        }

        public void Reset()
        {
            int x = 0;
            int y = 0;
            penX = x;
            penY = y;
            drawPanel.Refresh();
            SetPenColour(Color.Black);
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
            drawPanel.Refresh();
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

            using (Graphics graphics = Graphics.FromImage(flashingBitmap))
            {
                graphics.Clear(SystemColors.ButtonShadow);
                foreach (var shape in shapes)
                {
                    shape.draw(graphics);
                }
            }
        }
    }
}
