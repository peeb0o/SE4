using SE4.Exceptions;
using SE4.Variables;
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
        private VariableManager variableManager;
        private int lineNumber = 1;
        
        /// <summary>
        /// Initialises an instance of the Form1 class
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // Initialize the shape manager with the drawing panel
            shapeManager = new ShapeFactory(drawPanel);
            variableManager = VariableManager.Instance;
            commandParser = new CommandParser(shapeManager, variableManager, lineNumber);
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunButtonClicked(object sender, EventArgs e)
        {
            // if to check for commands in singleline textbox
            if (!string.IsNullOrWhiteSpace(singleCommandTextBox.Text))
            {
                string command = singleCommandTextBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(command))
                {
                    try
                    {
                        commandParser.ParseCommand(command, lineNumber);
                        Console.WriteLine("Parser called"); // for debugging purposes - remove later
                    }
                    catch (InvalidParameterCountException ex)
                    {
                        PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    catch(CommandException ex)
                    {
                        PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

            }

            if (!string.IsNullOrWhiteSpace(multiLineTextBox.Text))
            {
                string[] commands = multiLineTextBox.Lines;

                foreach (string command in commands)
                {
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        try
                        {
                            commandParser.ParseCommand(command, lineNumber);
                            Console.WriteLine("Parser called"); // for debugging purposes - remove later
                        }
                        catch (InvalidParameterCountException ex)
                        {
                            PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                            PanelUtilities.WriteToPanel(drawPanel);
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (CommandException ex)
                        {
                            PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                            PanelUtilities.WriteToPanel(drawPanel);
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                    lineNumber++;
                }
            }
            lineNumber = 1;
        }

        private void SingleCommandTextBoxEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string command = singleCommandTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        commandParser.ParseCommand(command, lineNumber);
                        Console.WriteLine("Parser called"); // for debugging purposes - remove later
                    }
                    catch (InvalidParameterCountException ex)
                    {
                        PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    catch (CommandException ex)
                    {
                        PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                singleCommandTextBox.Clear();

                if (runCommandEntered == true)
                {
                    string[] commands = multiLineTextBox.Lines;

                    foreach (string cmd in commands)
                    {
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            try
                            {
                                commandParser.ParseCommand(cmd, lineNumber);
                                lineNumber++;
                                Console.WriteLine("Parser called"); // for debugging purposes - remove later
                            }
                            catch (InvalidParameterCountException ex)
                            {
                                lineNumber++;
                                PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                                PanelUtilities.WriteToPanel(drawPanel);
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            catch (CommandException ex)
                            {
                                lineNumber++;
                                PanelUtilities.AddErrorMessage($"{ex.Message}" + " on ", lineNumber);
                                PanelUtilities.WriteToPanel(drawPanel);
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                    }
                    singleCommandTextBox.Clear();
                }
            }
            lineNumber = 1;
        }

        private void SingleCommandTextBoxRunCommand(object sender, EventArgs e)
        {
            //string command = singleCommandTextBox.Text.Trim();
            if (singleCommandTextBox.Text.Trim().ToLower().Equals("run"))
            {
                runCommandEntered = true;
            }
        }

        private void SaveButtonClicked(object sender, EventArgs e)
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

        private void SyntaxButtonClicked(object sender, EventArgs e)
        {
            string[] commands = multiLineTextBox.Lines;

            commandParser.syntaxCheck = true;
            foreach (string cmd in commands)
            {
                if (!string.IsNullOrWhiteSpace(cmd))
                {
                    try
                    {
                        commandParser.ParseCommand(cmd, lineNumber);
                        
                        PanelUtilities.AddErrorMessage("Syntax check complete no errors found", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                    }
                    catch (Exception ex)
                    {
                        PanelUtilities.AddErrorMessage($"Syntax error found: {ex.Message} on:", lineNumber);
                        PanelUtilities.WriteToPanel(drawPanel);
                    }
                }
                lineNumber++;
            }
            lineNumber = 1;
            commandParser.syntaxCheck = false;
        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
