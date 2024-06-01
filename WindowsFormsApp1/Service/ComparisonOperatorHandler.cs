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
    /// Class which handles comparison operations to be used in if and loop statements
    /// </summary>
    public class ComparisonOperatorHandler
    {
        private VariableManager variableManager;

        /// <summary>
        /// Initialises an instance of the ComparisonOperatorHandler class
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables. In this case to be able to handle variables as part of the comparison operation. </param>
        public ComparisonOperatorHandler(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        /// <summary>
        /// Method which performs the comparison operation. Logic is similar to other operator classes with slight differences as there are more possible operators here so 
        /// a loop through the operators array is performed to find the operator before extracting the values to be compared. 
        /// </summary>
        /// <param name="command"> A string containing the command in which the comparison operation is to be performed. </param>
        /// <returns> Returns a boolean type in which true is returned if the result of the comparison is true and false if not. </returns>
        public bool TryHandleComparisonOperator(string command)
        {
            //Possible operators
            string[] operators = new string[] { "<", ">", "=>", "=<" };
            int operatorIndex = -1;
            string comparisonOperator = null;

            //Find index of operator passed
            foreach (var op in operators)
            {
                operatorIndex = command.IndexOf(op);

                if (operatorIndex != -1)
                {
                    comparisonOperator = op;
                    break;
                }
            }

            if (operatorIndex != -1)
            {
                // Extract the left and right operands
                string leftOperand = command.Substring(0, operatorIndex).Trim().ToLower();
                string rightOperand = command.Substring(operatorIndex + comparisonOperator.Length).Trim().ToLower();

                // Evaluate operands
                int value1 = GetValue(leftOperand);
                int value2 = GetValue(rightOperand);

                // Perform the comparison
                bool result;
                switch (comparisonOperator)
                {
                    case "<":
                        result = value1 < value2;
                        break;
                    case ">":
                        result = value1 > value2;
                        break;
                    case "<=":
                        result = value1 <= value2;
                        break;
                    case ">=":
                        result = value1 >= value2;
                        break;
                    default:
                        //Return false if evaluation 
                        return false;
                }

                // Return the result of the comparison
                Console.WriteLine($"Comparison result: {result}");
                return result;
            }

            return false;
        }

        /// <summary>
        /// Method for retrieving the value of passed operand. Checks if it is a variable and returns value 
        /// otherwise returns the literal value or throws an exception for invalid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns> Returns an integer value for the passed operand. Value is checked for it's variable value and returned.
        /// If can't find variable then it is parsed as a literal integer otherwise an exception is thrown.
        /// </returns>
        public int GetValue(string value)
        {
            //Try find variable value
            if (variableManager.VariableExists(value))
            {
                return variableManager.GetVariableValue(value);
            }

            //Try parse int
            if(int.TryParse(value, out int newValue))
            {
                return newValue;
            }

            throw new CommandException($"Invalid value: {value}");
        }
        
    }
}
