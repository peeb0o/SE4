using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Variables
{
    public class VariableManager
    {
        private List<Variable> variables;
        private static VariableManager instance;
        private static readonly object padlock = new object();

        private VariableManager()
        {
            variables = new List<Variable>();
        }

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

        public void UpdateVariable(string name, int value)
        {
            var variableExists = variables.FirstOrDefault(v => v.Name == name);
            if (variableExists != null)
            {
                variableExists.Value = value;
            }
            else
            {
                variables.Add(new Variable(name, value));
            }
        }

        public int GetVariableValue(string name)
        {
            var variable = variables.FirstOrDefault(v => v.Name == name);
            if (variable != null)
            {
                return variable.Value;
            }

            return 0;
        }

        public bool VariableExists(string name)
        {
            return variables.Any(v => v.Name == name);
        }

        public void VariablesClear()
        {
            variables.Clear();
        }
    }
}
