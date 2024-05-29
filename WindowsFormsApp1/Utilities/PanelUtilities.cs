using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SE4
{
    public static class PanelUtilities
    {

        private static List<(string message, int lineNumber)> errorMessages = new List<(string, int)>();

        public static void AddErrorMessage(string message, int lineNumber)
        {
            errorMessages.Add((message, lineNumber));
        }

        public static void ClearErrorMessages()
        {
            errorMessages.Clear();
        }

        public static void WriteToPanel(Panel panel)
        {
            Graphics g = panel.CreateGraphics();

            int lineHeight = TextRenderer.MeasureText("Sample", SystemFonts.DefaultFont).Height;
            int yPosition = 10;

            foreach (var error in errorMessages)
            {
                g.DrawString($"{error.message} (Line: {error.lineNumber})", SystemFonts.DefaultFont, Brushes.Black, new Point(10, yPosition));
                yPosition += lineHeight + 5;
            }
        }
    }
}
