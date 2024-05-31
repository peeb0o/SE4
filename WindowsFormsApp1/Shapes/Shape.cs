using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public abstract class Shape : Shapes
    {

        protected Color colour;
        protected int x, y;
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public Shape(Color colour, int x, int y)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
        }

        public abstract void draw(Graphics g);
        
        public void SetColour(Color colour)
        {
            this.colour = colour;
        }
    }
}
