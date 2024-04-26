using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class Triangle : Shape
    {
        private int sideLength;
        private Boolean fill;

        public Triangle(Color colour, int x, int y, int sideLength, bool fill) : base(colour, x, y)
        {
            this.x = x;
            this.y = y;
            this.sideLength = sideLength;
            this.fill = fill;
        }

        public override void draw(Graphics g)
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

            base.draw(g);
        }

        /*public override double calcArea()
        {
            return sideLength;
        }

        public override double calcPerimeter()
        {
            return sideLength;
        }*/

        public override string ToString()
        {
            return ""; //TODO don't return null fix later
        }
    }
}
