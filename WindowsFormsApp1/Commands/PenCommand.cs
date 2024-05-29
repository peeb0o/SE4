using SE4.Exceptions;
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

        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidParameterCountException("Invalid number of parameters passed for pen command. Syntax: pen <colour>");
            }
            
            Color colour;

            string colourString = parameters[1];

            
            colour = Color.FromName(colourString);

            if (!colour.IsKnownColor)
            {
                colour = Color.Black;
                shapeFactory.SetPenColour(colour);
                throw new InvalidColourException("Invalid colour passed, setting to default colour");
            }
            
            if(!syntaxCheck)
            shapeFactory.SetPenColour(colour);
            
        }             
    } 
}


