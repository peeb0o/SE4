using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE4.Utilities
{
    /// <summary>
    /// Class which serves to validate variable names. 
    /// </summary>
    public static class VariableValidation
    {
        /// <summary>
        /// Method which checks if a variable name is valid using a regular expression. 
        /// </summary>
        /// <param name="variableName"> The variable name to be validated. </param>
        /// <returns> Returns a boolean value of true if the variable name is valid and false otherwise. </returns>
        public static bool IsValidVariableName(string variableName)
        {
            // Define a regex pattern for valid variable names 
            // Validates that the name contains only upper or lower case alphabetical characters and underscores
            string pattern = @"^\w*$";
            // Create a regex object with the pattern
            Regex regex = new Regex(pattern);

            // Us IsMatch method to check if the variable name matches the pattern
            return regex.IsMatch(variableName);
        }
    }
}
