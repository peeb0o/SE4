using SE4.Commands;
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
    /// <summary>
    /// Parses and executes commands input by user
    /// </summary>
    public class CommandParser
    {
        private ShapeFactory shapeFactory;
        private DrawToCommand drawToCommand;
        private PenCommand penCommand;
        private CircleCommand circleCommand;
        private RectangleCommand rectangleCommand;
        private TriangleCommand triangleCommand;
        private FillCommand fillCommand;
        private VariableCommand variableCommand;
        private VariableManager variableManager;
        private ArithmeticOperatorHandler operatorHandler;
        private ComparisonOperatorHandler comparisonHandler;
        private EqualsOperatorHandler equalsHandler;
        private FlashingCommand flashCommand;
        private FlashingCommandStop flashStop;
        private MoveToCommand moveToCommand;
        private List<string> loopCommands;
        private string loopCondition;
        private bool isInLoop = false;
        private List<string> ifCommands;
        private bool isInIf = false;
        private string ifCondition;
        private int lineNumber;
        /// <summary>
        /// Gets or sets a value which indicates whethere or not the program is running in syntax mode where drawing is disabled.
        /// </summary>
        public bool syntaxCheck { get; set; } = false;

        /// <summary>
        /// Initialises an instance of command parser class
        /// </summary>
        /// <param name="factory"> Instance which is used to manage the drawing of shapes onto the panel. </param>
        /// <param name="variableManager"> Instance used to manage variables delcared. </param>
        /// <param name="lineNumber"> Current line number of a command being parsed - used for error reporting. </param>
        public CommandParser(ShapeFactory factory, VariableManager variableManager, int lineNumber)
        {
            this.lineNumber = lineNumber;
            shapeFactory = factory;
            this.variableManager = variableManager;
            drawToCommand = new DrawToCommand(variableManager);
            penCommand = new PenCommand();
            circleCommand = new CircleCommand(variableManager);
            rectangleCommand = new RectangleCommand(variableManager);
            triangleCommand = new TriangleCommand(variableManager);
            fillCommand = new FillCommand();
            variableCommand = new VariableCommand(variableManager);
            operatorHandler = new ArithmeticOperatorHandler(variableManager, shapeFactory);
            comparisonHandler = new ComparisonOperatorHandler(variableManager);
            equalsHandler = new EqualsOperatorHandler(variableManager);
            moveToCommand = new MoveToCommand(variableManager);
            flashCommand = new FlashingCommand();
            flashStop = new FlashingCommandStop();
            loopCommands = new List<string>();
            ifCommands = new List<string>();
        }

        /// <summary>
        /// Parses command from either single line command box or multiline command box
        /// Checks if in loop or if statement then looks for operation for variable declaration before entering main switch statement
        /// </summary>
        /// <param name="command"> The command which is passed from either textbox to be parsed. </param>
        /// <param name="lineNumber"> Linenumber of command currently being parsed, used for reporting errors. </param>
        public void ParseCommand(string command, int lineNumber)
        {
            //split command
            string[] parts = command.Split(' ');

            //Will parse command based on the first part that is read 
            string commandType = parts[0].ToLower().Trim();

            //Are we in a loop
            if (isInLoop)
            {
                //If end loop then execute contents until condition is met
                if (command.Trim().ToLower() == "endloop")
                {
                    // Execute the loop 
                    ExecuteLoop(lineNumber);
                    isInLoop = false;
                    return;
                }
                else
                {
                    // Collect loop commands
                    loopCommands.Add(command);
                }
            }

            //Single line if statement
            if (isInIf && command.Trim().ToLower() != "endif")
            {
                isInIf = false;
                this.ParseCommand(command, lineNumber);

                return;
            }
            //If block
            else if(isInIf && command.Trim().ToLower() == "endif")
            {
                isInIf = false;

                //Evaluate the condition of the if 
                if (EvaluateCondition(ifCondition))
                {
                    //Loop through each command in if block
                    foreach (var cmd in ifCommands)
                    {
                        //parse
                        this.ParseCommand(cmd, lineNumber);
                    }

                    return;
                }
            }

            try
            {
                //Check if command is trying an operation
                if (command.ToLower().Contains('+') || command.ToLower().Contains('-'))
                {
                    try
                    {
                        //Handle variable arithmetic operation
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

                //Check command type, default allows for variables to be declared without var keyword
                switch (commandType)
                {
                    case "flash":
                        flashCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "stopflash":
                        flashStop.Execute(shapeFactory, parts, syntaxCheck);
                        break;
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
                        variableCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "drawto":
                        drawToCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "moveto":
                        moveToCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "pen":
                        penCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "fill":
                        fillCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "circle":
                        circleCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "rectangle":
                        rectangleCommand.Execute(shapeFactory, parts, syntaxCheck);
                        break;
                    case "triangle":
                        triangleCommand.Execute(shapeFactory, parts, syntaxCheck);
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
                //Throw to top layer
                throw ex;
            }
        }

        /// <summary>
        /// Method for handling default case in the switch statement. Should always be a variable declaration otherwise invalid command passed.
        /// </summary>
        /// <param name="command"> Command to be checked, should be a variable declared without var keyword. </param>
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
                    PanelUtilities.AddErrorMessage("Invalid variable name given: " + variableName, lineNumber);
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel);
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
                    PanelUtilities.AddErrorMessage("Invalid variable value: " + valueString, lineNumber);
                    PanelUtilities.WriteToPanel(shapeFactory.drawPanel);
                }
            }
            else
            {
                // Invalid command, write to panel
                if (!command.Trim().ToLower().Equals("endif"))
                throw new InvalidParameterCountException("Invalid command entered");
            }
        }

        /// <summary>
        /// Method which executes the commands collected in the loop block passed by user
        /// Will continue to loop through the commands in the loop block until the EvaluateCondition method returns false
        /// </summary>
        /// <param name="lineNumber"> Needed as we are calling ParseCommand method which takes linenumber as a a param. </param>
        private void ExecuteLoop(int lineNumber)
        {
            // Create a copy of loopCommands (needed or invalid operation exception for iterating over original as original continues to collect commands while looping back through)
            List<string> loopCommandsCopy = new List<string>(loopCommands); 

            //While condition is met keep looping through 
            while (EvaluateCondition(loopCondition))
            {
                foreach (var cmd in loopCommandsCopy)
                {
                    Console.WriteLine($"Executing command: {cmd}"); // Debugging statement remove later
                    ParseCommand(cmd, lineNumber);
                }
                // Re-evaluate the loop condition after executing commands once condition is false break and endloop
                if (!EvaluateCondition(loopCondition))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Method for checking the condition and then returns true or false for if the condition is met.
        /// </summary>
        /// <param name="condition"> The string for which the evaluation is checked against. </param>
        /// <returns> If the condition evaluation is true then returns true otherwise false. </returns>
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
