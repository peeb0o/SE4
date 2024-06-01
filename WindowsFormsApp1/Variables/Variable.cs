using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Variables
{
    /// <summary>
    /// Class which represents variables and their respective values. 
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Gets name of the variable.
        /// </summary>
        public string name { get; }

        /// <summary>
        /// Gets or sets value of the variable. 
        /// </summary>
        public int value { get; set; }

        /// <summary>
        /// Initialises an instance of the variable class. 
        /// </summary>
        /// <param name="name"> Name of the variable. </param>
        /// <param name="value"> Value of the variable. </param>
        public Variable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
