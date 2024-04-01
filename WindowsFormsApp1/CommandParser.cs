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
        private DrawLineCommand drawLineCommand;

        public CommandParser(ShapeFactory factory)
        {
            shapeFactory = factory;
            drawLineCommand = new DrawLineCommand();
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();

            //parse commands using if statements
            if (commandType == "drawline")
            {
                drawLineCommand.Execute(shapeFactory, parts);
            }
        }
    }
}
