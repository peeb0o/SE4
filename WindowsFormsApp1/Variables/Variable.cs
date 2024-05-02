using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE4.Variables
{
    public class Variable
    {
        public string Name { get; }
        public int Value { get; set; }

        public Variable(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
