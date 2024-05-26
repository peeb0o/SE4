using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Service
{
    public class ComparisonOperatorHandler
    {
        private VariableManager variableManager;

        public ComparisonOperatorHandler(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        public bool TryHandleComparisonOperator(string command)
        {
            string[] operators = new string[] { "<", ">", "=>", "=<" };
            int operatorIndex = -1;
            string comparisonOperator = null;

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
                int value1 = Evaluate(leftOperand);
                int value2 = Evaluate(rightOperand);

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
                        //throw exceptipon
                        return false;
                }

                // Return the result of the comparison
                Console.WriteLine($"Comparison result: {result}");
                return result;
            }

            return false;
        }

        public int Evaluate(string value)
        {
            if (variableManager.VariableExists(value))
            {
                return variableManager.GetVariableValue(value);
            }

            if(int.TryParse(value, out int newValue))
            {
                return newValue;
            }

            //throw some exception;
            return 0;
        }
        
    }
}
