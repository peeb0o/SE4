using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Service
{
    /// <summary>
    /// Class which handles equality operations, specifically equal to and not equal to
    /// </summary>
    public class EqualsOperatorHandler
    {
        private VariableManager variableManager;

        /// <summary>
        /// Initialises an instance of the EqualsOperatorHandler class
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables. In this case to be able to handle variables as part of the equality operation. </param>
        public EqualsOperatorHandler(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Method which checks which operators are passed and then passes command and operator to HandleEqualsOperation method
        /// </summary>
        /// <param name="command"></param>
        /// <returns> Returns a boolean of true if the HandleEqualsOperation method returns true and false if not. Can return either as the operation can be either
        /// explicitly. </returns>
        public bool TryHandleEqualsOperator(string command)
        {
            if (command.Contains("=="))
            {
                return HandleEqualsOperation(command, "==");
            }
            else if (command.Contains("!=")){
                return HandleEqualsOperation(command, "!=");
            }
            return false;
        }

        /// <summary>
        /// Method which actually performs the check to see if the values are equal or not.
        /// </summary>
        /// <param name="command"> String which contains the command with the operation on. Will contain the condition explicitly written as 
        /// any keywords are removed before attempted operation is performed. </param>
        /// <param name="operatorSymbol"> the string containing the exact operation being attempted. </param>
        /// <returns> Returns the boolean result of the operation. </returns>
        public bool HandleEqualsOperation(string command, string operatorSymbol)
        {
            //Split the command into parts based on specific operator symbol passed
            string[] parts = command.ToLower().Split(new[] { operatorSymbol }, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new CommandException($"Invalid syntax for '{operatorSymbol}' comparison.");
            }

            //String will be a == b before split and a,b after 
            string leftOperand = parts[0].Trim();
            string rightOperand = parts[1].Trim();

            //Literal or variable
            int leftValue = GetValue(leftOperand);
            int rightValue = GetValue(rightOperand);

            bool result;
            //Perform operation and return result
            if (operatorSymbol == "==")
            {
                result = leftValue == rightValue;
                Console.WriteLine($"Equality comparison (==) is {result}.");
            }
            else // operatorSymbol == "!="
            {
                result = leftValue != rightValue;
                Console.WriteLine($"Inequality comparison (!=) is {result}.");
            }

            return result;
        }

        /// <summary>
        /// Method for retrieving the value of passed operand. Checks if it is a variable and returns value 
        /// otherwise returns the literal value or throws an exception for invalid.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns> Returns an integer value for the passed operand. Value is checked for it's variable value and returned.
        /// If can't find variable then it is parsed as a literal integer otherwise an exception is thrown.
        /// </returns>
        private int GetValue(string operand)
        {
            if (int.TryParse(operand, out int value))
            {
                return value;
            }

            if (variableManager.VariableExists(operand))
            {
                return variableManager.GetVariableValue(operand);
            }

            throw new CommandException($"Invalid operand: {operand}");
        }
    }
}
