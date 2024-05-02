using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    public class DrawTo : Shape
    {
        private int startX, startY, endX, endY;
        public DrawTo(Color colour, int startX, int startY, int endX, int endY) : base(colour, endX, endY)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }

        public override void draw(Graphics g)
        {
             System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
             g.DrawLine(p, startX, startY, endX, endY);
            
             base.draw(g);
        }

        public override string ToString()
        {
            return ""; //TODO don't return null fix later
        }
    }
}
