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
        private bool runCommandEntered = false;

        public Form1()
        {
            InitializeComponent();

            // Initialize the shape manager with the drawing panel
            shapeManager = new ShapeFactory(drawPanel);
            commandParser = new CommandParser(shapeManager);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void runButtonClicked(object sender, EventArgs e)
        {
            // if to check for commands in singleline textbox
            if (!string.IsNullOrWhiteSpace(singleCommandTextBox.Text))
            {
                string command = singleCommandTextBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(command))
                {
                    commandParser.ParseCommand(command);
                    Console.WriteLine("Parser called"); // for debugging purposes - remove later
                }

            }

            if (!string.IsNullOrWhiteSpace(multiLineTextBox.Text))
            {
                string[] commands = multiLineTextBox.Lines;

                foreach (string command in commands)
                {
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        commandParser.ParseCommand(command);
                        Console.WriteLine("Parser called"); // for debugging purposes - remove later
                    }
                }
            }
        }

        private void singleCommandTextBoxEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string command = singleCommandTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(command))
                {
                    commandParser.ParseCommand(command);
                    Console.WriteLine("Parser called"); // for debugging purposes - remove later
                }
                singleCommandTextBox.Clear();

                if (runCommandEntered == true)
                {
                    string[] commands = multiLineTextBox.Lines;

                    foreach (string cmd in commands)
                    {
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            commandParser.ParseCommand(cmd);
                            Console.WriteLine("Parser called"); // for debugging purposes - remove later
                        }
                    }
                    singleCommandTextBox.Clear();
                }
            }
        }

        private void singleCommandTextBoxRunCommand(object sender, EventArgs e)
        {
            //string command = singleCommandTextBox.Text.Trim();
            if (singleCommandTextBox.Text.Trim().ToLower().Equals("run"))
            {
                runCommandEntered = true;
            }
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.Title = "Save File";
            save.InitialDirectory = @"C:\Documents";
            save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            save.DefaultExt = "txt";
            save.AddExtension = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(save.FileName.ToString());
                file.WriteLine(multiLineTextBox.Text);
                file.Close();
            }
        }

        private void loadButtonClicked(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();

            load.Title = "Load File";
            load.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (load.ShowDialog() == DialogResult.OK)
            {
                string file = load.FileName;
                string text = System.IO.File.ReadAllText(file);
                multiLineTextBox.Text = text;
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
