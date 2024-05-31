using SE4.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Commands
{
    /// <summary>
    /// Class which allows flashstop command for stopping the flashing colours.
    /// </summary>
    public class FlashingCommandStop : Command
    {

        /// <summary>
        /// Executes the parameters in this case only one in length just flashstop and executes StopFlash method.
        /// </summary>
        /// <param name="shapeFactory"></param>
        /// <param name="parameters"></param>
        /// <param name="syntaxCheck"></param>
        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            //Must be one only
            if (parameters.Length != 1)
            {
                throw new InvalidParameterCountException("Command for stop flash should be single command only: <stopflash>");
            }

            //Stop flashing
            if(!syntaxCheck)
            shapeFactory.StopFlash();
        }
    }
}
