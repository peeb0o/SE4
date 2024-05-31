using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// Class for parsing the pen command passed from CommandClass
    /// </summary>
    public class PenCommand : Command
    {
        Pen pen = new Pen();

        /// <summary>
        /// Executes the pen colour command based on the parameter passed 
        /// </summary>
        /// <param name="shapeFactory"> Instance used to set the pen colour. </param>
        /// <param name="parameters"> A string array containing the command parameters. Second element being the desired colour. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed for pen command. Syntax: pen <colour>");
            }

            Color colour;

            string colourString = parameters[1];

            colour = Color.FromName(colourString);

            //If not a known colour default to black and let user know. 
            if (!colour.IsKnownColor)
            {
                colour = Color.Black;
                shapeFactory.SetPenColour(colour);
                throw new InvalidColourException("Invalid colour passed, setting to default colour");
            }
            
            //Set colour
            if(!syntaxCheck)
            shapeFactory.SetPenColour(colour);
            
        }             
    } 
}


