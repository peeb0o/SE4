using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    public class FlashingCommand : Command
    {
        private readonly Dictionary<string, Color> colorMap = new Dictionary<string, Color>
    {
        {"red", Color.Red},
        {"green", Color.Green},
        {"blue", Color.Blue},
        {"yellow", Color.Yellow},
        {"black", Color.Black},
        {"white", Color.White},
    };

        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters for flash command. Syntax: flash <color1color2>");
            }

            Color[] colours = ExtractColors(parameters[1]);

            if(!syntaxCheck)
            shapeFactory.StartFlash(colours);
        }

        private Color[] ExtractColors(string combinedColours)
        {
            List<Color> colours = new List<Color>();

            foreach (var entry in colorMap)
            {
                if (combinedColours.Contains(entry.Key))
                {
                    colours.Add(entry.Value);
                    combinedColours = combinedColours.Replace(entry.Key, "");
                }
                else
                {
                    throw new CommandException("Invalid value passed, please pass a valid colour");
                }
            }

            return colours.ToArray();
        }
    }
}
