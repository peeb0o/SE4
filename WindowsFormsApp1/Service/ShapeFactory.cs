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
    /// <summary>
    /// Class which manages and draws shapes on the drawpanel among other functions such as flashing colours and managing pen position.
    /// </summary>
    public class ShapeFactory
    {
        /// <summary>
        /// Sets the panel used for all drawing operations to be displayed on.
        /// </summary>
        public Panel drawPanel;
        /// <summary>
        /// The list of shapes that have been added to the shapefactory
        /// </summary>
        public List<Shape> shapes = new List<Shape>();
        private Bitmap drawBitmap;
        private Bitmap flashingBitmap;
        private Pen pen = new Pen();
        /// <summary>
        /// Sets or gets the x axis position of the pen marker. 
        /// </summary>
        public int penX { get; private set; }
        /// <summary>
        /// Sets or gets the y axis position of the pen marker. 
        /// </summary>
        public int penY { get; private set; }
        /// <summary>
        /// Sets of gets the current color of the pen marker, defaults to black. 
        /// </summary>
        public Color penColor { get; private set; } = Color.Black;
        /// <summary>
        /// Sets or gets the current fill option value, defaults to false/off.
        /// </summary>
        public bool fill { get; private set; } = false;
        private Thread flashingThread;
        private bool flashing;
        public Color[] flashingColours { get; private set; } // for testing
        private int flashingInterval = 500; //half a second per flash

        /// <summary>
        /// Initialises a new instance of the ShapeFactory class
        /// </summary>
        /// <param name="panel"> The panel where which the shapes will be drawn. </param>
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

        /// <summary>
        /// Adds the passed shape to the list of shapes and calls RedrawBitmap method
        /// </summary>
        /// <param name="shape"> The shape to be drawn. </param>
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            RedrawBitmap();
            drawPanel.Refresh();
        }

        /// <summary>
        /// Starts a new thread and passes the array of colours to be used for the flashing cycle.
        /// </summary>
        /// <param name="colours"> The array of colours to be passed to the FlashColours method. </param>
        public void StartFlash(Color[] colours)
        {
            flashingColours = colours;
            flashing = true;
            flashingThread = new Thread(new ThreadStart(FlashColours));
            flashingThread.Start();
        }

        /// <summary>
        /// Stops the current flashing operation and kills the thread.
        /// </summary>
        public void StopFlash()
        {
            flashing = false;
            if (flashingThread.IsAlive)
            {
                //test this out
                flashingThread.Abort();
            }
        }

        /// <summary>
        /// Uses the second bitmap to change the colour of the shapes drawn so that they flash, cycling through the index of the colour array containing
        /// the desired colours. 
        /// </summary>
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
                        shape.Draw(g);
                    }
                }

                drawPanel.Invoke(new Action(() => drawPanel.Refresh()));

                index = (index + 1) % flashingColours.Length;
                Thread.Sleep(flashingInterval);
            }
        }

        /// <summary>
        /// Clears the draw panel and error messages. 
        /// </summary>
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

        /// <summary>
        /// Resets the position of the pen marker and sets its colour to default.
        /// </summary>
        public void Reset()
        {
            int x = 0;
            int y = 0;
            penX = x;
            penY = y;
            drawPanel.Refresh();
            SetPenColour(Color.Black);
        }

        /// <summary>
        /// Sets the position of the pen on the panel. 
        /// </summary>
        /// <param name="x"> The desired x coordinate for the pen. </param>
        /// <param name="y"> The desired y coordinate for the pen. </param>
        public void MovePen(int x, int y)
        {
            penX = x;
            penY = y;
            drawPanel.Refresh(); 
        }

        /// <summary>
        /// Sets the pen colour to the passed colour.
        /// </summary>
        /// <param name="newColor"> The new colour for the pen to be set to. </param>
        public void SetPenColour(Color newColor)
        {
            penColor = newColor;
            drawPanel.Refresh();
        }

        /// <summary>
        /// Returns current pen colour.
        /// </summary>
        /// <returns> Returns current pen colour. </returns>
        public Color GetPenColour()
        {
            return penColor;
        }
        
        /// <summary>
        /// Sets the fill value to either on or off. 
        /// </summary>
        /// <param name="fillSetting"> Boolean with the desired setting for the fill value. </param>
        public void SetFillValue(Boolean fillSetting)
        {
            fill = fillSetting;
        }

        /// <summary>
        /// Gets the current fill value.
        /// </summary>
        /// <returns> Returns a boolean value with the current fill setting. </returns>
        public Boolean GetFill()
        {
            return fill;
        }

        /// <summary>
        /// Redraws both bitmaps and actually draws the shapes. 
        /// </summary>
        public void RedrawBitmap()
        {
            using (Graphics graphics = Graphics.FromImage(drawBitmap))
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(graphics);
                }
            }

            using (Graphics graphics = Graphics.FromImage(flashingBitmap))
            {
                graphics.Clear(SystemColors.ButtonShadow);
                foreach (var shape in shapes)
                {
                    shape.Draw(graphics);
                }
            }
        }
    }
}
