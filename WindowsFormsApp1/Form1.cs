using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4
{
    public partial class Form1 : Form
    {
        private ShapeFactory shapeManager;
        private CommandParser commandParser;

        public Form1()
        {
            InitializeComponent();

            // Initialize the shape manager with the drawing panel
            shapeManager = new ShapeFactory(drawPanel);
            commandParser = new CommandParser(shapeManager);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void runButtonClicked(object sender, EventArgs e)
        {
            string[] commands = singleCommandTextBox.Lines;

            foreach (string command in commands)
            {
                if(!string.IsNullOrWhiteSpace(command))
                {
                    commandParser.ParseCommand(command);
                    Console.WriteLine("Parser called");
                }    
            }
        }
    }
}
