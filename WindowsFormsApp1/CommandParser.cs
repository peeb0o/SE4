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
        private CircleCommand circleCommand;
        private RectangleCommand rectangleCommand;
        private TriangleCommand triangleCommand;
        private FillCommand fillCommand;

        public CommandParser(ShapeFactory factory)
        {
            this.shapeFactory = factory;
            drawToCommand = new DrawToCommand();
            penCommand = new PenCommand();
            circleCommand = new CircleCommand();
            rectangleCommand = new RectangleCommand();
            triangleCommand = new TriangleCommand();
            fillCommand = new FillCommand();
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

                else if (commandType == "pen")
                {
                    penCommand.Execute(shapeFactory, parts);
                }

                else if (commandType == "fill")
                {
                    fillCommand.Execute(shapeFactory, parts);
                }

                else if (commandType == "circle")
                {
                    circleCommand.Execute(shapeFactory, parts);
                }

                else if (commandType == "rectangle")
                {
                    rectangleCommand.Execute(shapeFactory, parts);
                }
                
                else if(commandType == "triangle")
                {
                    triangleCommand.Execute(shapeFactory, parts);
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
