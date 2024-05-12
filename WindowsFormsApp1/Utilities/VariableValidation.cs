using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE4.Utilities
{
    public static class VariableValidation
    {
        public static bool IsValidVariableName(string variableName)
        {
            // Define a regex pattern for valid variable names
            string pattern = @"^[a-zA-Z_]\w*$";

            // Create a regex object with the pattern
            Regex regex = new Regex(pattern);

            // Us IsMatch method to check if the variable name matches the pattern
            return regex.IsMatch(variableName);
        }
    }
}
