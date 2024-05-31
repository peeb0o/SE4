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
    /// <summary>
    /// Class for parsing variable command from CommandParser
    /// </summary>
    public class VariableCommand : Command
    {
        private VariableManager variableManager;

        /// <summary>
        /// Initialises instance of CircleCommand class 
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables, used to manage the adding of new variables and also checking for existing variables. </param>
        public VariableCommand(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Executes the processing of variables using the paramets passed. 
        /// This method passes the params to the ProcessVarCommand which handles the logic.
        /// </summary>
        /// <param name="shapeFactory"> Instance used to manage drawing of shapes. </param>
        /// <param name="parameters"> A string array of the command parameters to be parsed. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
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

        /// <summary>
        /// Method for processing the command passed and validating before passing values to ProcessCommands method.
        /// Method checks to see if a value has been assigned or not in the command and sets to 0 by default if no value is explicitly set.
        /// Variable name is checked to see if it already exists and an exception is thrown if so and 
        /// variable name is checked against regex using the VariableValidation class to see if it has a valid name.
        /// </summary>
        /// <param name="shapefactory"> Instance used to manage drawing of shapes.</param>
        /// <param name="parameters"> A string array of the command parameters to be parsed. </param>
        /// <param name="syntaxCheck"> Boolean value indicating whether the program is in syntax check mode or not. If yes then the drawing operation is not carried out. </param>
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

        /// <summary>
        /// Method for processing the variable name and value once the values have been validated. 
        /// </summary>
        /// <param name="variableName"> String for the variable name to be added to the variable manager. </param>
        /// <param name="variableValue"> Integer holding value of the variable to be added to variable manager. </param>
        /// <param name="syntaxCheck"> Boolean checking if program was run in syntax check mode and not executing if so. </param>
        public void ProcessCommands(string variableName, int variableValue, bool syntaxCheck)
        {
            if(!syntaxCheck)
            variableManager.AddVariable(variableName, variableValue);
        }
    }
}
