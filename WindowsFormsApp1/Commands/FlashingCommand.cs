using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    /// <summary>
    /// Class for parsing flash command sent from CommandParser
    /// </summary>
    public class FlashingCommand : Command
    {
        private readonly Dictionary<string, Color> colourMap = new Dictionary<string, Color>
    {
        {"red", Color.Red},
        {"green", Color.Green},
        {"blue", Color.Blue},
        {"yellow", Color.Yellow},
        {"black", Color.Black},
        {"white", Color.White},
    };

        /// <summary>
        /// Executes the flash command using the parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to start the flashing thread. </param>
        /// <param name="parameters"> A string array which contains the parameters to be parsed. </param>
        /// <param name="syntaxCheck"> Boolean value checking if program is being run in syntax mode, if true then the flashing operation is not started. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters for flash command. Syntax: flash <color1color2>");
            }

            //Extract the colours sent 
            Color[] colours = ExtractColors(parameters[1]);

            if(!syntaxCheck)
            shapeFactory.StartFlash(colours);
        }

        /// <summary>
        /// Method which extracts the colours passed in the parameters to the execute method 
        /// Colours are passed combined so the loop looks for an entry matching in the dictionary colourMap and then replaces it with an empty string.
        /// This is repeated until no colours are left. 
        /// </summary>
        /// <param name="combinedColours"></param>
        /// <returns> Returns the array of colours to be sent to the StartFlash method. </returns>
        private Color[] ExtractColors(string combinedColours)
        {
            List<Color> colours = new List<Color>();
            
            //Loop through dictionary
            int maxColourCount = 3;
            foreach (var entry in colourMap)
            {
                //Check for matches and replace string with empty string 
                //Repeat until no matches left
                if (combinedColours.Contains(entry.Key))
                {
                    colours.Add(entry.Value);
                    combinedColours = combinedColours.Replace(entry.Key, "");
                }
                
                if(colours.Count.Equals(maxColourCount))
                {
                    throw new InvalidParameterCountException("Too many colours passed. Syntax: flash <color1color2>");
                }
            }

            if (combinedColours.Trim().Length > 0)
            {
                throw new CommandException("Invalid value passed, please pass a valid color");
            }

            return colours.ToArray();
        }
    }
}
