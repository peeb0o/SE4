using SE4.Exceptions;
using SE4.Utilities;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE4.Service
{
    public class VariableCommand : Command
    {
        private VariableManager variableManager;
        
        public VariableCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        public override void Execute(ShapeFactory shapeFactory, string[] parameters, bool syntaxCheck)
        {
            try
            {
                ProcessVarCommand(shapeFactory, parameters, syntaxCheck);
            }
            catch (InvalidParameterCountException ex)
            {
                throw ex;
            }
            catch(CommandException ex)
            {
                throw ex;
            }
        }

        private void ProcessVarCommand(ShapeFactory shapefactory, string[] parameters, bool syntaxCheck)
        {
            try
            {
                //Variable name will always be second element of array when using var command
                string variableName = parameters[1].Trim().ToLower();
                //Set value to zero by default 
                int variableValue = 0;
                //Check to see correct number of parameters passed in command will always either be two or four
                if (parameters.Length != 2 && parameters.Length != 4)
                {
                    throw new InvalidParameterCountException("Invalid number of parameters passed");
                }

                //if parameters equal two then variable has been declared with no value
                if (parameters.Length == 2)
                {
                    //Validation on variable name checking whitespace and matching regex pattern
                    if (string.IsNullOrWhiteSpace(variableName) || !VariableValidation.IsValidVariableName(variableName))
                    {
                        throw new CommandException("Invalid Variable Name");
                    }

                    //Check to see if variable exists already
                    if (variableManager.VariableExists(variableName))
                    {
                        throw new CommandException("Variable with this name already exists");
                    }

                    ProcessCommands(variableName, variableValue, syntaxCheck);
                }
                // four elements in array means value has been set 
                else 
                {
                    //Tryparse variable value throw error if unable
                    if(!int.TryParse(parameters[3].Trim(), out variableValue))
                    {
                        throw new CommandException($"Invalid variable value passed + '{variableValue}'");
                    }

                    if (variableManager.VariableExists(variableName))
                    {
                        throw new CommandException($"Variable: '{variableName}' already exists");
                    }

                    if (!VariableValidation.IsValidVariableName(variableName))
                    {
                        throw new CommandException($"Invalid Variable Name: '{variableName}'");
                    }

                    ProcessCommands(variableName, variableValue, syntaxCheck);
                }
            }
            catch(Exception ex)
            {
                //Catch any other unecpected exceptiona and write to console
                Console.WriteLine(ex);
                //Throw to outer catch in execute method, won't write to panel otherwise
                throw;
            }
        }

        public void ProcessCommands(string variableName, int variableValue, bool syntaxCheck)
        {
            if(!syntaxCheck)
            variableManager.AddVariable(variableName, variableValue);
        }
    }
}
