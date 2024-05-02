using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class PenCommand : Command
    {
        Pen pen = new Pen();

        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length == 2)
            {
                string colorString = parameters[1];
                Color color;

                try
                {
                    string[] coordinates = parameters[1].Split(',');

                    if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
                    {
                        shapeFactory.MovePen(x, y);
                    }
                    else
                    {
                        try
                        {
                            color = Color.FromName(colorString);
                        }
                        catch
                        {
                            color = Color.Black;
                        }

                        shapeFactory.SetPenColour(color);
                    }

                }
                catch (Exception)

                {
                    Console.WriteLine("Not enough params for move pen");
                }       
            }
        }
    }
}
