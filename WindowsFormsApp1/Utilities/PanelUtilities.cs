using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SE4
{
    /// <summary>
    /// Class for various utility functions
    /// </summary>
    public static class PanelUtilities
    {

        private static List<(string message, int lineNumber)> errorMessages = new List<(string, int)>();

        /// <summary>
        /// Method which adds error messages and current line number to the errorMessages list. 
        /// </summary>
        /// <param name="message"> The message passed when the exception was thrown. </param>
        /// <param name="lineNumber"> Current line number of which the error was caught. </param>
        public static void AddErrorMessage(string message, int lineNumber)
        {
            errorMessages.Add((message, lineNumber));
        }

        /// <summary>
        /// Clears the list of errormessages. 
        /// </summary>
        public static void ClearErrorMessages()
        {
            errorMessages.Clear();
        }

        /// <summary>
        /// Method for writing the error message to the drawpanel. Uses the sample text to gauge the line height and then appends this to the 
        /// yposition so consecutive error messages display in a list and not overlapping. 
        /// </summary>
        /// <param name="panel"> The panel for the messages to be displayed on. </param>
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
