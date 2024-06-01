using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    /// <summary>
    /// Class for the line shape. 
    /// </summary>
    public class Line : Shape
    {
        private int startX, startY, endX, endY;

        /// <summary>
        /// Initialises an instance of the Line class.
        /// </summary>
        /// <param name="colour"> Desired colour of the line. </param>
        /// <param name="startX"> Starting X coordinate of the line. </param>
        /// <param name="startY"> Starting Y coordinate of the line.  </param>
        /// <param name="endX"> Ending X coordinate of the line. </param>
        /// <param name="endY"> Ending Y coordinate of the line. </param>
        public Line(Color colour, int startX, int startY, int endX, int endY) : base(colour, endX, endY)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }

        /// <summary>
        /// Draws the line shape using the passed graphics method. 
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
             System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
             g.DrawLine(p, startX, startY, endX, endY); 
        }
    }
}
