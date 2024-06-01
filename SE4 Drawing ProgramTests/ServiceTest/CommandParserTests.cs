using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4.Tests
{
    /// <summary>
    /// Class testing the CommandParser
    /// </summary>
    [TestClass()]
    public class CommandParserTests
    {
        private CommandParser commandParser;
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private int lineNumber = 0; // add logic later
        
        /// <summary>
        /// Initialises the classes needed for testing.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            commandParser = new CommandParser(shapeFactory, variableManager, lineNumber);
            variableManager.VariablesClear();
        }

        /// <summary>
        /// Tests drawto.execute with a valid command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_DrawTo_Success()
        {
            //Setup
            string command = "drawto 100,100";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(100, shapeFactory.penY);
        }

        /// <summary>
        /// Tests moveto with a valid command.
        /// </summary>
        [TestMethod()]
        public void ParseCommand_MoveTo_Success()
        {
            //Setup
            string command = "moveto 100,100";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(100, shapeFactory.penY);
        }

        /// <summary>
        /// Tests valid pen colour command 
        /// </summary>
        [TestMethod()]
        public void ParseCommand_PenColour_Success()
        {
            //Setup
            string command = "pen red";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(shapeFactory.penColor, Color.Red);
        }

        /// <summary>
        /// Tests valid fill command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Fill_On_Success()
        {
            //Setup
            string command = "fill on";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(shapeFactory.fill, true);
        }

        /// <summary>
        /// Tests valid fill off command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Fill_Off_Success()
        {
            //Setup
            string command = "fill off";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(shapeFactory.fill, false);
        }

        /// <summary>
        /// Tests valid circle command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Circle_Success()
        {
            //Setup
            string command = "circle 20";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Circle);
        }

        /// <summary>
        /// Tests valid rectangle command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Rectangle_Success()
        {
            //Setup
            string command = "rectangle 200,100";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Rectangle);
        }

        /// <summary>
        /// Tests valid triangle command 
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Triangle_Success()
        {
            //Setup
            string command = "triangle 20";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Triangle);
        }

        /// <summary>
        /// Test valid reset command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Reset_Success()
        {
            //Setup
            string command = "reset";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(0, shapeFactory.penX);
            Assert.AreEqual(0, shapeFactory.penY);
        }

        /// <summary>
        /// Tests valid clear command
        /// </summary>
        [TestMethod()]
        public void ParseCommand_Clear_Success()
        {
            //Setup
            shapeFactory.AddShape(new Circle(Color.Black, 100, 100, 100, false));
            shapeFactory.AddShape(new Rectangle(Color.Black, 100, 100, 200, 200, false));

            string command = "clear";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
           Assert.AreEqual(0, shapeFactory.shapes.Count);
        }

        /// <summary>
        /// Tests valid variable declaration with var keyword
        /// </summary>
        [TestMethod()]
        public void Parse_Command_Variable_Declaration_Success()
        {
            //Setup
            string command = "var x";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.IsTrue(variableManager.VariableExists(variableName));
        }

        /// <summary>
        /// Tests valid variable declaration without var keyword
        /// </summary>
        [TestMethod()]
        public void Parse_Command_Variable_Declaration_Success_No_Var_Keyword()
        {
            //Setup
            string command = "x = 100";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.IsTrue(variableManager.VariableExists(variableName));
        }

        /// <summary>
        /// Tests valid arithmetic operation addition using literal integers
        /// </summary>
        [TestMethod()]
        public void Parse_Arithmetic_Operation_Addition_Success()
        {
            //Setup
            string command = "var x = 100 + 200";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(300, variableManager.GetVariableValue(variableName));
        }

        /// <summary>
        /// Tests valid subtraction operation using literal integers
        /// </summary>
        [TestMethod()]
        public void Parse_Arithmetic_Operation_Subtraction_Success()
        {
            //Setup
            string command = "var x = 200 - 100";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(100, variableManager.GetVariableValue(variableName));

        }

        /// <summary>
        /// Tests valid arithmetic addition operation using variables only
        /// </summary>
        [TestMethod()]
        public void Parse_Arithmetic_Variables_Addition_Success()
        {
            //Setup
            variableManager.AddVariable("y", 10);
            variableManager.AddVariable("z", 50);
            string command = "var x = y + z";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(60, variableManager.GetVariableValue(variableName));
        }

        /// <summary>
        /// Tests valid arithmetic subtraction operation using variables only
        /// </summary>
        [TestMethod()]
        public void Parse_Arithmetic_Variables_Subtraction_Success()
        {
            //Setup
            variableManager.AddVariable("y", 10);
            variableManager.AddVariable("z", 50);
            string command = "var x = z - y";
            string variableName = "x";

            //Action
            commandParser.ParseCommand(command, lineNumber);

            //Assert
            Assert.AreEqual(40, variableManager.GetVariableValue(variableName));
        }

        /// <summary>
        /// Ensures the parameter count exception is thrown for an invalid empty command. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Parse_Command_Invalid_Command_Empty()
        {
            //Setup
            string command = " ";
            
            //Action
            commandParser.ParseCommand(command, lineNumber);
        }

        /// <summary>
        /// Ensures the command exception is thrown for invalid arithmetic operations.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CommandException))]
        public void Parse_Command_Invalid_Arithmetic_Command()
        {
            //Setup
            string command = "+ 100";
            
            //Action
            commandParser.ParseCommand(command, lineNumber);
        }
    }
}