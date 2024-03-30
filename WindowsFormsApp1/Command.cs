using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public abstract class Command
    {
        public abstract void Execute(ShapeFactory shapeManager, string[] parameters);
    }
}