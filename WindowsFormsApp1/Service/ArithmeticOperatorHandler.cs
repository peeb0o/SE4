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
    /// Class which handles arithmetic operations, specifically addition and subtraction
    /// </summary>
    public class ArithmeticOperatorHandler
    {
        private VariableManager variableManager;
        private ShapeFactory shapeFactory;

        /// <summary>
        /// Initialises an instance of the ArithmeticOperatorHandler class
        /// </summary>
        /// <param name="variableManager"> Instance used to manage variables. In this case to be able to handle variables as part of the arithmetic operation. </param>
        /// <param name="shapeFactory"> Instance used to handle drawing of shapes. </param>
        public ArithmeticOperatorHandler(VariableManager variableManager, ShapeFactory shapeFactory)
        {
            this.variableManager = variableManager;
            this.shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Method which performs the arithmetic operation based on the command passed.
        /// Command is split on the equals and using an index the operator is found and 
        /// left and right operands are checked. Values are checked to see if they are
        /// variables or literals before actually performing the operation. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns> Returns a boolean value of true if operation was carried out or an exception is thrown if failed explicitly. Different to other operator handlers as they can
        /// return either true or false but integers can't explicitly be false as a return type. </returns>
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

        /// <summary>
        /// Method which removes the var keyword from the beginning of the command if present. 
        /// This allows for the handler to work for variable declarations with and without
        /// the var keyword. 
        /// </summary>
        /// <param name="variableNameWithVar"></param>
        /// <returns> Returns a string which either removed the var keyword or left it as it was. </returns>
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
