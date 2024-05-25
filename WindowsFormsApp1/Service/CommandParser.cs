using SE4.Exceptions;
using SE4.Service;
using SE4.Utilities;
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
        private ArithmeticOperatorHandler operatorHandler;

        public CommandParser(ShapeFactory factory, VariableManager variableManager)
        {
            this.shapeFactory = factory;
            this.variableManager = variableManager;
            drawToCommand = new DrawToCommand(variableManager);
            penCommand = new PenCommand();
            circleCommand = new CircleCommand(variableManager);
            rectangleCommand = new RectangleCommand(variableManager);
            triangleCommand = new TriangleCommand(variableManager);
            fillCommand = new FillCommand();
            storedProgram = new StoredProgram(variableManager);
            operatorHandler = new ArithmeticOperatorHandler(variableManager, shapeFactory);
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();

            //parse commands using if statements

            try
            {

                if (command.Contains('+') || command.Contains('-') && command.Contains("="))
                {
                    try
                    {
                        if (operatorHandler.HandleArithmeticOperators(command))
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


                /* if (commandType == "var" && !commandType.Contains("="))
                 {
                     storedProgram.Execute(shapeFactory, parts);
                 }*/


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
                            string variableName = assignment[0].Trim().ToLower();
                            string valueString = assignment[1].Trim();

                            //Validating variable name using utility class regex pattern
                            if (!VariableValidation.IsValidVariableName(variableName))
                            {
                                PanelUtilities.WriteToPanel(shapeFactory.drawPanel, "Invalid variable name given: " + variableName);
                                break;
                            }

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
                            throw new InvalidParameterCountException("Invalid number of parameters in command");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                PanelUtilities.WriteToPanel(shapeFactory.drawPanel, ex.Message);
                throw;
            }
        }
    }   
}
