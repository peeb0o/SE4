using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// The abstract base class for shapes. 
    /// </summary>
    public abstract class Shape : IShapes
    {
        /// <summary>
        /// Colour of the shape.
        /// </summary>
        protected Color colour;

        /// <summary>
        /// X and Y coordinates of the shape.
        /// </summary>
        protected int x, y;

        /// <summary>
        /// Gets the X coordinate of the shape.
        /// </summary>
        public int X { get { return x; } }

        /// <summary>
        /// Gets the Y coordinate of the shape.
        /// </summary>
        public int Y { get { return y; } }

        /// <summary>
        /// Initialises a new instance of the Shape class.
        /// </summary>
        /// <param name="colour"> Colour of the shape. </param>
        /// <param name="x"> X coordinate of the shape. </param>
        /// <param name="y"> Y coordinate of the shape. </param>
        public Shape(Color colour, int x, int y)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Draws the shape using the passed graphics object.
        /// </summary>
        /// <param name="g"> Graphics object used to draw the shape. </param>
        public abstract void Draw(Graphics g);
        
        /// <summary>
        /// Sets the colour of the shape.
        /// </summary>
        /// <param name="colour"></param>
        public void SetColour(Color colour)
        {
            this.colour = colour;
        }
    }
}
