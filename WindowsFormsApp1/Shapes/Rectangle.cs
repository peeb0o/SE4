﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    public class Rectangle : Shape
    {
        public int width { get; private set; }
        public int height { get; private set; }
        private Boolean fill;

        public Rectangle(Color colour, int x, int y, int width, int height, bool fill) : base(colour, x, y)
        {
            this.x = x;
            this.y = y;
            this.width = width; 
            this.height = height;
            this.fill = fill;
        }

        public override void draw(Graphics g)
        {
            if (fill)
            {
                SolidBrush b = new SolidBrush(colour);
                g.FillRectangle(b, x, y, width, height);
            }
            else
            {
                System.Drawing.Pen p = new System.Drawing.Pen(colour, 2);
                g.DrawRectangle(p, x, y, width, height);
            }
        }
    }
}
