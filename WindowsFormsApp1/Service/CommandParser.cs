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
        private ComparisonOperatorHandler comparisonHandler;
        private EqualsOperatorHandler equalsHandler;
        private List<string> loopCommands;
        private string loopCondition;
        private bool isInLoop = false;
        private List<string> ifCommands;
        private bool isInIf = false;
        private string ifCondition;

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
            comparisonHandler = new ComparisonOperatorHandler(variableManager);
            equalsHandler = new EqualsOperatorHandler(variableManager);
            this.loopCommands = new List<string>();
            this.ifCommands = new List<string>();
        }

        public void ParseCommand(string command)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();


            if (isInLoop)
            {
                if (command.Trim().ToLower() == "endloop")
                {
                    // Execute the loop
                    ExecuteLoop();
                    isInLoop = false;
                    return;
                }
                else
                {
                    // Collect loop commands
                    loopCommands.Add(command);
                }
                
            }

            if (isInIf)
            {
                if(command.Trim().ToLower() == "endif")
                {
                    //execute if block
                    /*ExecuteIf();*/
                    isInIf = false;

                    if (EvaluateCondition(ifCondition))
                    {
                        foreach (var cmd in ifCommands)
                        {
                            this.ParseCommand(cmd);
                        }
                    }
                }
                else
                {
                    //Collect if block commands
                    ifCommands.Add(command);
                }
                return;
            }

            //parse commands using if statements

            //Check if command is trying an operation
            try
            {

                if (command.ToLower().Contains('+') || command.ToLower().Contains('-')) //&& command.Contains("=")
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

                if (command.ToLower().Contains("==") || command.ToLower().Contains("!=")) 
                {
                    try
                    {
                        if (equalsHandler.TryHandleEqualsOperator(command))
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //Check command type, default allows for variables to be declared without var keyword
                switch (commandType)
                {
                    case "loop":
                        loopCondition = command.Substring(command.IndexOf(' ') + 1).Trim();
                        isInLoop = true;
                        loopCommands.Clear();
                        break;
                    case "if":
                        ifCondition = command.Substring(command.IndexOf(' ') + 1).Trim();
                        isInIf = true;
                        ifCommands.Clear();
                        break;
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
                        HandleDefaultCase(command);
                        break;
                }
            }
            catch (Exception ex)
            {
                PanelUtilities.WriteToPanel(shapeFactory.drawPanel, ex.Message);
                throw;
            }
        }

        private void HandleDefaultCase(string command)
        {
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
                    return;
                    //break leave to test;
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
        }

        private void ExecuteLoop()
        {
            List<string> loopCommandsCopy = new List<string>(loopCommands); // Create a copy of loopCommands (needed or invalid operation exception for iterating over original)

            while (EvaluateCondition(loopCondition))
            {
                foreach (var cmd in loopCommandsCopy)
                {
                    Console.WriteLine($"Executing command: {cmd}"); // Debugging statement
                    ParseCommand(cmd);
                }
                // Re-evaluate the loop condition after executing commands
                if (!EvaluateCondition(loopCondition))
                {
                    break;
                }
            }
        }

        private bool EvaluateCondition(string condition)
        {
            if (condition.Contains("==") || condition.Contains("!="))
            {
                return equalsHandler.TryHandleEqualsOperator(condition);
            }

            else if (condition.Contains('<') || condition.Contains('>'))
            {
                return comparisonHandler.TryHandleComparisonOperator(condition);
            }

            else if (condition.Contains('+') || condition.Contains('-'))
            {
                return operatorHandler.HandleArithmeticOperators(condition);
            }
            return false;
        }
    }   
}
