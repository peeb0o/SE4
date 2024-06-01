using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Commands;
using SE4.Exceptions;
using SE4.Variables;

namespace SE4_Drawing_ProgramTests.CommandsTest
{
    /// <summary>
    /// Class testing the DrawToCommand class. 
    /// </summary>
    [TestClass()]
    public class MoveToCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private MoveToCommand moveToCommand;

        /// <summary>
        /// Method initialising the classes needed for testing. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            moveToCommand = new MoveToCommand(variableManager);
        }

        /// <summary>
        /// Ensures that the pen marker is moved to the passed coordinates using literal values. 
        /// </summary>
        [TestMethod]
        public void Execute_MoveSuccess_WithLiteralCoordinates()
        {
            //Setup
            string[] parameters = { "moveto", "100,200" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
           
            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(200, shapeFactory.penY);
        }

        /// <summary>
        /// Ensures that the pen marker is moved to the passed coordinates using one variable and one literal value. 
        /// </summary>
        [TestMethod]
        public void Execute_MoveSuccess_WithVariableXCoordinate()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            string[] parameters = { "moveto", "x,100" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
           
            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
        }

        /// <summary>
        /// Ensures that the pen marker is moved to the passed coordinates using one variable and one literal value. 
        /// </summary>
        [TestMethod]
        public void Execute_MoveSuccess_WithVariableYCoordinate()
        {
            //Setup
            variableManager.AddVariable("y", 200);
            string[] parameters = { "moveto", "100,y" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
            
            //Assert
            Assert.AreEqual(200, shapeFactory.penY);
        }

        /// <summary>
        /// Ensures that the pen marker is moved to the passed coordinates using variable values.
        /// </summary>
        [TestMethod]
        public void Execute_MoveSuccess_WithVariableCoordinates()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            variableManager.AddVariable("y", 200);
            string[] parameters = { "moveto", "x,y" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
            
            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(200, shapeFactory.penY);
        }

        /// <summary>
        /// Ensures the pen marker isn't moved if in syntax mode.
        /// </summary>
        [TestMethod]
        public void Execute_SyntaxCheck_Success()
        {
            //Setup 
            string[] parameters = { "moveto", "10,10" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(0, shapeFactory.penX);
            Assert.AreEqual(0, shapeFactory.penY);

        }

        /// <summary>
        /// Ensures parameter count exception is thrown if too few parameters passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_NoCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "moveto" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures parameter count exception is thrown if too few coordinates passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_TooFewCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "moveto", "x" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures parameter count exception is thrown if empty string passed as coordinate. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "moveto", "x," };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures parameter count exception is thrown if blank space passed as coordinate. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_WhiteSpaceAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "moveto", "x, " };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures command exception is thrown if invalid values passed as coordinates.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_ThrowsException_CommandException()
        {
            //Setup
            string[] parameters = { "moveto", "1{}(),5()" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
