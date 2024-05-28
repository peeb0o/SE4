using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Service
{
    public class ArithmeticOperatorHandler
    {
        private VariableManager variableManager;
        private ShapeFactory shapeFactory;

        public ArithmeticOperatorHandler(VariableManager variableManager, ShapeFactory shapeFactory)
        {
            this.variableManager = variableManager;
            this.shapeFactory = shapeFactory;
        }

        public bool HandleArithmeticOperators(string command)
        {
            try
            {
                string[] assignmentParts = command.ToLower().Split('=');
               
                if (assignmentParts.Length != 2)
                {
                    throw new CommandException("Invalid Arithmetic Command");
                }

                // Trim whitespace from each part
                string variableNameWithVar = assignmentParts[0].Trim();
                string arithmeticExpression = assignmentParts[1].Trim();

                //Remove var from start of string with variabla name
                string variableName = RemoveVarKeyword(variableNameWithVar);

                // Find the index of the operator (+ or -) in the arithmetic expression
                int operatorIndex = arithmeticExpression.IndexOfAny(new char[] { '+', '-' });

                if (operatorIndex != -1)
                {
                    // Extract the left and right operands
                    string leftValue = arithmeticExpression.Substring(0, operatorIndex).Trim();
                    string rightValue = arithmeticExpression.Substring(operatorIndex + 1).Trim();

                    // Get operand values or integer values
                    int value1;
                    int value2;
                    
                    //If command is using variables then value for exact variable will be fetched, otherwise the hard integer value passed is used
                    if (!int.TryParse(leftValue, out value1))
                    {
                        value1 = variableManager.GetVariableValue(leftValue);
                        bool leftValueValid = int.TryParse(variableManager.GetVariableValue(leftValue).ToString(), out value1);
                        // Check if operand value is valid
                        if (!leftValueValid)
                        {
                            throw new InvalidOperationException("Invalid value(s)");
                        }
                    }

                    if (!int.TryParse(rightValue, out value2))
                    {
                        value2 = variableManager.GetVariableValue(rightValue);
                        bool rightValueValid = int.TryParse(variableManager.GetVariableValue(rightValue).ToString(), out value2);
                        // Check if operand value is valid
                        if (!rightValueValid)
                        {
                            throw new InvalidOperationException("Invalid value(s)");
                        }
                    }

                    // Perform the operation, check if it's an addition or subtraction operation
                    int result;
                    if (arithmeticExpression[operatorIndex] == '+')
                    {
                        result = value1 + value2;
                    }
                    else
                    {
                        result = value1 - value2;
                    }

                    // Update the variable value
                    variableManager.UpdateVariable(variableName, result);

                    //This is just for my testing atm
                    Console.WriteLine($"Result of arithmetic operation: {result}");
                }

                return true;
            }
            catch (CommandException ex)
            {
                
                //throw to outer try catch to break from parsing should exception be thrown in arithmetic logic
                throw ex;
            }
            catch(InvalidOperationException ex)
            {
                
                //throw to outer try catch to break from parsing should exception be thrown in arithmetic logic
                throw ex;
            }
        }

        private string RemoveVarKeyword(string variableNameWithVar)
        {
            // Check if the variable name starts with "var"
            if (variableNameWithVar.StartsWith("var "))
            {
                // Remove "var " from the variable name
                return variableNameWithVar.Substring(4);
            }
            else
            {
                // Return the original variable name
                return variableNameWithVar;
            }
        }
    }
}
