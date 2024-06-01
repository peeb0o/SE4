using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// The class for the circle shape. 
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        public int radius { get; private set; }

        /// <summary>
        /// Gets or sets the current fill value for the circle.
        /// </summary>
        public bool fill { get; private set; }

        /// <summary>
        /// Initialises a new instance of the Circle class. 
        /// </summary>
        /// <param name="colour"> The colour for the circle. </param>
        /// <param name="x"> X coordinate of the circle. </param>
        /// <param name="y"> Y coordinate of the circle. </param>
        /// <param name="radius"> Radius of the circle. </param>
        /// <param name="fill"> Fill value. </param>
        public Circle(Color colour, int x, int y, int radius, bool fill) : base(colour, x, y)
        {
            this.radius = radius;
            this.fill = fill;
        }

        /// <summary>
        /// Returns current fill value setting for the circle. 
        /// </summary>
        /// <returns></returns>
        public bool IsFilled()
        {
            return fill;
        }

        /// <summary>
        /// Method which draws the circle using the passed graphics object. Checks if fill value is on and draws a 
        /// filled circle if so, otherwise a regular circle is drawn. 
        /// </summary>
        /// <param name="g"> Graphics object used to draw the circle. </param>
        public override void Draw(Graphics g)
        {
            if (fill)
            {
                SolidBrush b = new SolidBrush(colour);
                g.FillEllipse(b, x, y, radius * 2, radius * 2);
            }
            else 
            {
            System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
            }        
        }
    }
}

