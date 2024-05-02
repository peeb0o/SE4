using SE4.Service;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4
{
    public class CommandParser
    {
        private ShapeFactory shapeFactory;
        private DrawToCommand drawToCommand;
        private PenCommand penCommand;
        private CircleCommand circleCommand;
        private RectangleCommand rectangleCommand;
        private TriangleCommand triangleCommand;
        private FillCommand fillCommand;
        private StoredProgram storedProgram;
        private VariableManager variableManager;

        public CommandParser(ShapeFactory factory)
        {
            this.shapeFactory = factory;
            drawToCommand = new DrawToCommand();
            penCommand = new PenCommand();
            variableManager = new VariableManager();
            circleCommand = new CircleCommand(variableManager);
            rectangleCommand = new RectangleCommand();
            triangleCommand = new TriangleCommand();
            fillCommand = new FillCommand();
            storedProgram = new StoredProgram();
            
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();

            //parse commands using if statements

            /*
             * if commandtype == loop
             *      figure out num of iterations through loop
             *      int x = num of iterations 
             *      string[] array = commands to be looped
             *      
             *     for number of iterations {
             *     
             *      foreach (string command in commands)
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    this.ParseCommand(command);
                    Console.WriteLine("Parser called"); // for debugging purposes - remove later
                }
            }

            }
             */

            switch (commandType)
            {
                case "var":
                    storedProgram.Execute(shapeFactory, parts);
                    break;
                case "drawto":
                    drawToCommand.Execute(shapeFactory, parts);
                    break;
                case "moveto":
                case "pen":
                    penCommand.Execute(shapeFactory, parts);
                    break;
                case "fill":
                    fillCommand.Execute(shapeFactory, parts);
                    break;
                case "circle":
                    circleCommand.Execute(shapeFactory, parts);
                    break;
                case "rectangle":
                    rectangleCommand.Execute(shapeFactory, parts);
                    break;
                case "triangle":
                    triangleCommand.Execute(shapeFactory, parts);
                    break;
                case "clear":
                    shapeFactory.Clear();
                    break;
                case "reset":
                    shapeFactory.Reset();
                    break;
                default:
                    if (command.Contains("="))
                    {
                        // Split the command into variable name and value
                        string[] assignment = command.Split('=');
                        string variableName = assignment[0].Trim();
                        string valueString = assignment[1].Trim();

                        // Parse the value string to determine the variable value
                        if (int.TryParse(valueString, out int variableValue))
                        {
                            // Set the variable value using the VariableManager
                            variableManager.UpdateVariable(variableName, variableValue);
                        }
                        else
                        {
                            // Invalid value string, write to panel
                            PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid variable value: " + valueString);
                        }
                    }
                    else
                    {
                        // Invalid command, write to panel
                        PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid Command");
                    }
                    break;

            }
        }
    }   
}
