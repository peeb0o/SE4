using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4
{
    public class FillCommand : Command
    {
        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            if (parameters.Length == 2)
            {
                string option = parameters[1];

                if (parameters[1].ToLower() == "on")
                {
                    shapeFactory.SetFillValue(true);
                }
                else if (parameters[1].ToLower() == "off")
                {
                    shapeFactory.SetFillValue(false);
                }
            }
        }
    }
}
