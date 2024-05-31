using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    /// <summary>
    /// The base command class for all command types inheriting from this class.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Executes a command with specified parameters passed.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to draw shapes onto the panel. </param>
        /// <param name="parameters"> A string array containing command parameters. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
        public abstract void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck);
    }
}