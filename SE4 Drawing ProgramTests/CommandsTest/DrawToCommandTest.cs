using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Exceptions;
using SE4.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4_Drawing_ProgramTests
{
    /// <summary>
    /// Class for testing the DrawToCommand class.
    /// </summary>
    [TestClass()]
    public class DrawToCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private DrawToCommand drawToCommand;

        /// <summary>
        /// Method initialising classes to be used for tests. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            drawToCommand = new DrawToCommand(variableManager);
        }

        /// <summary>
        /// Test ensuring that a line is drawn when a valid command is passed, using literal coordinates.
        /// </summary>
        [TestMethod]
        public void Execute_LineSuccess_WithLiteralCoordinates()
        {
            //Setup
            string[] parameters = { "drawto", "100,200" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
            Line line = (Line)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Line);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, line.X);
        }

        /// <summary>
        /// Test ensuring the line is drawn using a variable for the X coordinate. 
        /// </summary>
        [TestMethod]
        public void Execute_LineSuccess_WithVariableXCoordinate()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            string[] parameters = { "drawto", "x,100" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
            Line line = (Line)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Line);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, line.X);
        }

        /// <summary>
        /// Test ensuring the line is drawn using a variable for the Y coordinate. 
        /// </summary>
        [TestMethod]
        public void Execute_LineSuccess_WithVariableYCoordinate()
        {
            //Setup
            variableManager.AddVariable("y", 200);
            string[] parameters = { "drawto", "100,y" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
            Line line = (Line)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Line);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(200, line.Y);
        }

        /// <summary>
        /// Test ensuring a line is drawn using variables for both coordinates.
        /// </summary>
        [TestMethod]
        public void Execute_LineSuccess_WithVariableCoordinates()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            variableManager.AddVariable("y", 200);
            string[] parameters = { "drawto", "x,y" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
            Line line = (Line)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Line);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, line.X);
            Assert.AreEqual(200, line.Y);
        }

        /// <summary>
        /// Test ensuring no line is drawn when syntax mode is enabled.
        /// </summary>
        [TestMethod]
        public void Execute_SyntaxCheck_Success()
        {
            //Setup 
            string[] parameters = { "drawto", "10,10" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count);
        }

        /// <summary>
        /// Test ensuring parameter count exception is thrown when too few parameters are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_NoCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "drawto" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring parameter count exception is thrown when too few coordinates are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_TooFewCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "drawto", "x" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring parameter count exception is thrown when empty string is passed as second coordinate. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "drawto", "x," };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring parameter count exception is thrown when blank space is passed as second coordinate. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_WhiteSpaceAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "drawto", "x, " };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring command exception is thrown when invalid coordinates are passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_ThrowsException_CommandException()
        {
            //Setup
            string[] parameters = { "drawto", "1{}(),5()" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
