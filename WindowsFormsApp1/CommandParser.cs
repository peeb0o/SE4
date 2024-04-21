using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4
{
    public class CommandParser
    {
        private ShapeFactory shapeFactory;
        private DrawToCommand drawToCommand;
        private PenCommand penCommand;

        public CommandParser(ShapeFactory factory)
        {
            this.shapeFactory = factory;
            drawToCommand = new DrawToCommand();
            penCommand = new PenCommand();
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();

            //parse commands using if statements
            if (parts.Length >= 1)
            {

                if (commandType == "drawto")
                {
                    drawToCommand.Execute(shapeFactory, parts);
                }

                else if (commandType == "moveto")
                {
                    penCommand.Execute(shapeFactory, parts);
                }

                else if (commandType == "clear")
                {
                    shapeFactory.Clear();
                }

                else if (commandType == "reset")
                {
                    shapeFactory.Reset();
                }
                else
                {
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid Command");                 
                }
            }          
        }
    }
}
