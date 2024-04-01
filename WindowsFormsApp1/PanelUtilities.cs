using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SE4
{
    class PanelUtilities
    {

        public static void WriteToPanel(Panel panel, string message)
        {
            Graphics g = panel.CreateGraphics();

            g.Clear(panel.BackColor);

            g.DrawString(message, SystemFonts.DefaultFont, Brushes.Black, new Point(10, 10));
        }
    }
}
