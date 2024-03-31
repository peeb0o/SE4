using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class CommandParser
    {
        private ShapeFactory shapeFactory;

        public CommandParser(ShapeFactory factory)
        {
            shapeFactory = factory;
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower();

            //parse commands using if statements

            //if drawline
            if (commandType == "drawline")
            {
                if (parts.Length >= 3)
                {
                    string[] startPoint = parts[1].Split(',');
                    string[] endPoint = parts[2].Split(',');

                    if (startPoint.Length == 2 && endPoint.Length == 2 &&
                        int.TryParse(startPoint[0], out int startX) &&
                        int.TryParse(startPoint[1], out int startY) &&
                        int.TryParse(endPoint[0], out int endX) &&
                        int.TryParse(endPoint[1], out int endY))
                    {
                        shapeFactory.DrawLine(new Point(startX, startY), new Point(endX, endY));
                    }
                }
            }
        }
    }
}
