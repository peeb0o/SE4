using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Service
{
    public class EqualsOperatorHandler
    {
        private VariableManager variableManager;

        public EqualsOperatorHandler(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

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

        public bool HandleEqualsOperation(string command, string operatorSymbol)
        {
            string[] parts = command.ToLower().Split(new[] { operatorSymbol }, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new CommandException($"Invalid syntax for '{operatorSymbol}' comparison.");
            }

            string leftOperand = parts[0].Trim();
            string rightOperand = parts[1].Trim();

            int leftValue = GetValue(leftOperand);
            int rightValue = GetValue(rightOperand);

            bool result;
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
