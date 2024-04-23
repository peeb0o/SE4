using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    public class Rectangle : Shape
    {
        private int width, height;

        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.x = x;
            this.y = y;
            this.width = width; 
            this.height = height;
        }

        public override double calcArea()
        {
            return width * height;
        }

        public override double calcPerimeter()
        {
            return 2 * (width + height);
        }

        public override void draw(Graphics g)
        {
            System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
            g.DrawRectangle(p, x, y, width, height);
            base.draw(g);
        }

        public override string ToString()
        {
            return ""; //TODO don't return null fix later
        }
    }
}
