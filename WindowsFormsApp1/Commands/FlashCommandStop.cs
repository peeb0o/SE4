using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    public class FlashCommandStop : Command
    {
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new InvalidParameterCountException("Command for stop flash should be single command only");
            }
            shapeFactory.StopFlash();
        }

        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            throw new NotImplementedException();
        }
    }
}
