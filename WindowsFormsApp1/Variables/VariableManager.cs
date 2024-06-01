using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Variables
{
    /// <summary>
    /// Class which manages variables across the project. 
    /// </summary>
    public class VariableManager
    {
        private List<Variable> variables;
        private static VariableManager instance;
        private static readonly object padlock = new object();

        private VariableManager()
        {
            variables = new List<Variable>();
        }

        /// <summary>
        /// Initialises the singleton instance of the VariableManager class, ensures that only a single instance of the class is used.
        /// Calls the private constructor which creates the list of variables to be managed.
        /// </summary>
        public static VariableManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new VariableManager();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Adds variables to the list of variables.
        /// </summary>
        /// <param name="name"> Name of the variable to be added. </param>
        /// <param name="value"> Value of the variable to be added. </param>
        public void AddVariable(string name, int value)
        {
            if (this.VariableExists(name))
            {
                UpdateVariable(name, value);
            }
            else
            {
                variables.Add(new Variable(name, value));
            }
        }

        /// <summary>
        /// Updates existing variables with the desired new value or adds it if it doesn't exist. 
        /// </summary>
        /// <param name="name"> Name of the variable to update/add. </param>
        /// <param name="value"> Value of the variable to update/add. </param>
        public void UpdateVariable(string name, int value)
        {
            var variableExists = variables.FirstOrDefault(v => v.name == name);
            if (variableExists != null)
            {
                variableExists.value = value;
            }
            else
            {
                variables.Add(new Variable(name, value));
            }
        }

        /// <summary>
        /// Returns the value of the variable being checked. Uses lambda to find matching name. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Returns the integer value of the variable that was checked. </returns>
        public int GetVariableValue(string name)
        {
            var variable = variables.FirstOrDefault(v => v.name == name);
            if (variable != null)
            {
                return variable.value;
            }
            
            return 0;
        }

        /// <summary>
        /// Method which checks if the variable being passed exists already. 
        /// </summary>
        /// <param name="name"> The name of the variable being checked. </param>
        /// <returns> Returns a boolean value true if the variable exists and false if not. </returns>
        public bool VariableExists(string name)
        {
            return variables.Any(v => v.name == name);
        }

        /// <summary>
        /// Method which clears the variables list. 
        /// </summary>
        public void VariablesClear()
        {
            variables.Clear();
        }
    }
}
