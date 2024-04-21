using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    class Pen
    {
        public void Draw(Graphics graphics, int penX, int penY)
        {
            // Draw the pen at the specified position
            graphics.FillEllipse(Brushes.Red, penX, penY, 5, 5);
        }
    }
}
