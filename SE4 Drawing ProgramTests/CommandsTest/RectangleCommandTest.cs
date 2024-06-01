using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Exceptions;
using SE4.Variables;

namespace SE4_Drawing_ProgramTests.CommandsTest
{
    /// <summary>
    /// Class testing the RectangleCommand class.
    /// </summary>
    [TestClass()]
    public class RectangleCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private RectangleCommand rectangleCommand;

        /// <summary>
        /// Initialises the classes needed for testing. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            rectangleCommand = new RectangleCommand(variableManager);
        }

        /// <summary>
        /// Test method ensuring a rectangle is drawn using literal values for width and height. 
        /// </summary>
        [TestMethod]
        public void Execute_RectangleSuccess_WithLiteralWidthAndHeight()
        {
            //Setup
            string[] parameters = { "rectangle", "100,100" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
            Rectangle rectangle = (Rectangle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Rectangle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, rectangle.width);
        }

        /// <summary>
        /// Test method ensuring a rectangle is drawn using a variable for both width and height.
        /// </summary>
        [TestMethod]
        public void Execute_RectangleSuccess_WithVariableWidthAndHeight()
        {
            //Setup
            variableManager.AddVariable("width", 100);
            variableManager.AddVariable("height", 100);
            string[] parameters = { "rectangle", "width,height" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
            Rectangle rectangle = (Rectangle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Rectangle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, rectangle.width);
            Assert.AreEqual(100, rectangle.height);
        }

        /// <summary>
        /// Test method ensuring a rectangle is drawn using a variable for the width only. 
        /// </summary>
        [TestMethod]
        public void Execute_RectangleSuccess_WithVariableWidth()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            string[] parameters = { "rectangle", "x,100" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
            Rectangle rectangle = (Rectangle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Rectangle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, rectangle.width);
            Assert.AreEqual(100, rectangle.height);
        }

        /// <summary>
        /// Test method ensuring the rectangle isn't drawn when command is passed and syntax mode is true.
        /// </summary>
        [TestMethod]
        public void Execute_RectangleSuccess_SyntaxCheck()
        {
            //Setup
            string[] parameters = { "rectangle", "100,100" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, true);
            
            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count());
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown if too few parameters are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException()
        {
            //Setup
            string[] parameters = { "rectangle" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown if too few dimensions are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_NotEnoughDimensions()
        {
            //Setup
            string[] parameters = { "rectangle", "100" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown if an empty string is passed as a dimension. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringPassedAsDimension()
        {
            //Setup
            string[] parameters = { "rectangle", "100," };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown if an invalid value is passed as the dimension. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_ThrowsException_CommandException_InvalidValuePassedAsDimension()
        {
            //Setup
            string[] parameters = { "rectangle", "1()(),2()()" };

            //Action
            rectangleCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
