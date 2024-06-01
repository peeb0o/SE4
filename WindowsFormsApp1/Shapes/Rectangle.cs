using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    /// <summary>
    /// The class for the Rectangle shape.
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// Gets or sets the width value of the rectangle.
        /// </summary>
        public int width { get; private set; }

        /// <summary>
        /// Gets or sets the height value of the rectangle. 
        /// </summary>
        public int height { get; private set; }
        private Boolean fill;

        /// <summary>
        /// Initialises a new instance of the Rectangle class.
        /// </summary>
        /// <param name="colour"> The colour of the rectangle. </param>
        /// <param name="x"> X coordinate of the rectangle. </param>
        /// <param name="y"> Y coordinate of the rectangle. </param>
        /// <param name="width"> Width of the rectangle. </param>
        /// <param name="height"> Height of the rectangle. </param>
        /// <param name="fill"> Fill value of the rectangle. </param>
        public Rectangle(Color colour, int x, int y, int width, int height, bool fill) : base(colour, x, y)
        {
            this.x = x;
            this.y = y;
            this.width = width; 
            this.height = height;
            this.fill = fill;
        }

        /// <summary>
        /// Method which draws the rectangle shape using the passed graphics object.
        /// Checks if fill value is true and draws a filled rectangle if so. 
        /// </summary>
        /// <param name="g"> Graphics object used to draw the rectangle. </param>
        public override void Draw(Graphics g)
        {
            if (fill)
            {
                SolidBrush b = new SolidBrush(colour);
                g.FillRectangle(b, x, y, width, height);
            }
            else
            {
                System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
                g.DrawRectangle(p, x, y, width, height);
            }
        }
    }
}
