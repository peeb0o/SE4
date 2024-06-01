using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    /// <summary>
    /// Class for the pen marker
    /// </summary>
    public class Pen
    {
        /// <summary>
        /// Draws the pen marker shape
        /// </summary>
        /// <param name="c"> Colour of the pen icon. </param>
        /// <param name="graphics"> Graphics object used to draw the shape onto the panel. </param>
        /// <param name="penX"> Pens x coordinate on the panel. </param>
        /// <param name="penY"> Pens y coordinate on the panel. </param>
        public void Draw(Color c, Graphics graphics, int penX, int penY)
        {
            // Draw the pen at the specified position
            SolidBrush b = new SolidBrush(c);
            graphics.FillEllipse(b, penX, penY, 5, 5);
        }
    }
}
