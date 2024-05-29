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
    [TestClass()]
    public class CommandParserTests
    {
        private CommandParser commandParser;
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private int lineNumber = 0; // add logic later
        private bool syntaxCheck = false;

        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            commandParser = new CommandParser(shapeFactory, variableManager, lineNumber);
            variableManager.VariablesClear();
        }

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

        [TestMethod()]
        public void Parse_Command_Invalid_Command_Empty()
        {
            //Setup
            string command = " ";
            try
            {
                //Action
                commandParser.ParseCommand(command, lineNumber);

                //Assert
                Assert.Fail("No exception thrown for empty command");
            }
            catch (InvalidParameterCountException ex)
            {
                Assert.AreEqual("Invalid command entered", ex.Message);
            }
        }

        [TestMethod()]
        public void Parse_Command_Invalid_Arithmetic_Command()
        {
            //Setup
            string command = "+ 100";
            try
            {
                //Action
                commandParser.ParseCommand(command, lineNumber);

                //Assert
                Assert.Fail("No exception thrown for empty command");
            }
            catch (CommandException ex)
            {
                Assert.AreEqual("Invalid Arithmetic Command", ex.Message);
            }
        }
    }
}