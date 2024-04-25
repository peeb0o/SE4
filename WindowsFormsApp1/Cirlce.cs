using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class Circle : Shape
    {
        int radius;

        public Circle() : base()
        {

        }
        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {

            this.radius = radius; 
        }

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(colour, list[0], list[1]);
            this.radius = list[2];
        }

        public override void draw(Graphics g)
        {

            System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);   
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
            base.draw(g);
        }

        /*public override double calcArea()
        {
            return Math.PI * (radius ^ 2);
        }

        public override double calcPerimeter()
        {
            return 2 * Math.PI * radius;
        }*/

        public override string ToString()
        {
            return null; //TODO don't return null fix later
        }
    }
}

