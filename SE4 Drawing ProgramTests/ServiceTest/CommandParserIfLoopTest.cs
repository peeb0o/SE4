using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Variables;

namespace SE4_Drawing_ProgramTests.ServiceTest
{
    /// <summary>
    /// Class testing Commandparser if and loop command logic
    /// </summary>
    [TestClass()]
    public class CommandParserIfLoopTest
    {
        private ShapeFactory shapeFactory;
        private VariableManager variableManager;
        private CommandParser commandParser;
        private Panel panel;

        /// <summary>
        /// Initialising classes needed for tests
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            shapeFactory = new ShapeFactory(panel);
            variableManager = VariableManager.Instance;
            commandParser = new CommandParser(shapeFactory, variableManager, 0);
            variableManager.VariablesClear();
        }

        /// <summary>
        /// Test ensuring if commands work as single line if command
        /// </summary>
        [TestMethod]
        public void Execute_IfConditionSuccess_SingleLineIf()
        {
            //Setup
            variableManager.AddVariable("x", 10);
            string[] commands = {
                "if x == 10 circle x" 
            };

            //Act
            foreach (var cmd in commands)
            {
                commandParser.ParseCommand(cmd, 0);
            }

            //Assert
            Assert.AreEqual(1, shapeFactory.shapes.Count);
            Assert.IsTrue(shapeFactory.shapes[0] is Circle);
        }

        /// <summary>
        /// Test ensuring a valid if block can be executed
        /// </summary>
        [TestMethod]
        public void Execute_IfConditionSuccess_IfBlock()
        {
            //Setup
            variableManager.AddVariable("x", 10);
            variableManager.AddVariable("y", 20);
            string[] commands = {
                "if x == 10",
                "circle x",
                "x = x + 1",
                "circle y",
                "circle x",
                "rectangle y,x",
                "endif"
            };

            //Act
            foreach (var cmd in commands)
            {
                commandParser.ParseCommand(cmd, 0);
            }

            //Assert
            Assert.AreEqual(4, shapeFactory.shapes.Count);
            Assert.IsTrue(shapeFactory.shapes[0] is Circle);
            Assert.IsTrue(shapeFactory.shapes[3] is Rectangle);
        }

        /// <summary>
        /// Test ensuring if block fails if comparison operation returns false
        /// </summary>
        [TestMethod]
        public void Execute_IfConditionFail_IfBlock()
        {
            //Setup
            variableManager.AddVariable("x", 2);
            variableManager.AddVariable("y", 20);
            string[] commands = {
                "if x == 10",
                "circle x",
                "x = x + 1",
                "circle y",
                "circle x",
                "rectangle y,x",
                "endif"
            };

            //Act
            foreach (var cmd in commands)
            {
                commandParser.ParseCommand(cmd, 0);
            }

            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count);
        }

        /// <summary>
        /// Test ensuring if commands work as single line if command
        /// </summary>
        [TestMethod]
        public void Execute_IfConditionFails_SingleLineIf()
        {
            //Setup
            variableManager.AddVariable("y", 5);
            string[] commands = {
                "if y == 10 circle y"
            };

            //Act
            foreach (var cmd in commands)
            {
                commandParser.ParseCommand(cmd, 0);
            }

            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count);
        }

        /// <summary>
        /// Test ensuring loopblock executes as intended
        /// </summary>
        [TestMethod]
        public void Execute_LoopCommand_DrawsMultipleShapes()
        {
            //Setup
            variableManager.AddVariable("x", 10);
            string[] commands = {
                "count = 0", 
                "loop count < 3",
                "circle x",
                "count = count + 1",
                "endloop"
            };

            // Act
            foreach (var cmd in commands)
            {
                commandParser.ParseCommand(cmd, 0);
            }

            // Assert
            Assert.AreEqual(3, shapeFactory.shapes.Count);
            Assert.IsTrue(shapeFactory.shapes[0] is Circle);
            Assert.IsTrue(shapeFactory.shapes[1] is Circle);
            Assert.IsTrue(shapeFactory.shapes[2] is Circle);
        }
    }
}
