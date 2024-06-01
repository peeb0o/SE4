using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// The class for the Triangle shape.
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Gets or sets the sidelength of the trianlge.
        /// </summary>
        public int sideLength { get; private set; }
        private Boolean fill;

        /// <summary>
        /// Initialises an instance of the trianlge class.
        /// </summary>
        /// <param name="colour"> Colour of the triangle. </param>
        /// <param name="x"> X coordinate of the triangle. </param>
        /// <param name="y"> Y coordinate of the triangle. </param>
        /// <param name="sideLength"> Sidelength of the equilateral triangle. </param>
        /// <param name="fill"> Fill status of triangle. </param>
        public Triangle(Color colour, int x, int y, int sideLength, bool fill) : base(colour, x, y)
        {
            this.x = x;
            this.y = y;
            this.sideLength = sideLength;
            this.fill = fill;
        }

        /// <summary>
        /// Method for drawing the triangle shape using the passed graphics object. 
        /// An equilateral triangle will be drawn using the given sidelength. 
        /// </summary>
        /// <param name="g"> Graphics object used to draw the triangle. </param>
        public override void Draw(Graphics g)
        {
            
            Point[] points = new Point[3];
            points[0] = new Point(x, y + sideLength); // Bottom-left corner
            points[1] = new Point(x + sideLength, y + sideLength); // Bottom-right corner
            points[2] = new Point(x + sideLength / 2, y); // Top corner

            if (fill)
            {
                SolidBrush b = new SolidBrush(colour);
                g.FillPolygon(b, points);
            }
            else
            {
                System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
                g.DrawPolygon(p, points);
            }
        }
    }
}
