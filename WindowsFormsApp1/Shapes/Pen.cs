using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SE4
{
    public class Pen
    {
        public void Draw(Color c, Graphics graphics, int penX, int penY)
        {
            // Draw the pen at the specified position
            SolidBrush b = new SolidBrush(c);
            graphics.FillEllipse(b, penX, penY, 5, 5);
        }
    }
}
