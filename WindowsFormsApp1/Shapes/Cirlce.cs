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
        public int radius { get; private set; }
        public bool fill { get; private set; }

        public Circle(Color colour, int x, int y, int radius, bool fill) : base(colour, x, y)
        {
            this.radius = radius;
            this.fill = fill;
        }

        public bool IsFilled()
        {
            return fill;
        }

        public override void draw(Graphics g)
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

