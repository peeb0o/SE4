using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Service
{
    public class StoredProgram : Command
    {
        private VariableManager variableManager;

        public StoredProgram()
        {
            variableManager = new VariableManager();
        }

        public override void Execute(ShapeFactory shapeFactory, string[] parameters)
        {
            //if parameters are < 4 then var has not been initialised with value
            if (parameters.Length < 4)
            {
                //set value to 0 in this case
                string variableName = parameters[1].Trim().ToLower();
                int variableValue = 0;
                ProcessCommands(variableName, variableValue);
            }
            else
            {
                //otherwise set value to int passed in command
                string variableName = parameters[1].Trim().ToLower();
                int variableValue = int.Parse(parameters[3].Trim());
                ProcessCommands(variableName, variableValue);
            }
        }

        public void ProcessCommands(string variableName, int variableValue)
        {
            variableManager.AddVariable(variableName, variableValue);
        }
    }
}
