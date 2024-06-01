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

namespace SE4_Drawing_ProgramTests
{
    /// <summary>
    /// Class for testing the CircleCommand class.
    /// </summary>
    [TestClass()]
    public class CircleCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private CircleCommand circleCommand;
        
        /// <summary>
        /// Setup method which initialises the required classes. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            circleCommand = new CircleCommand(variableManager);
        }

        /// <summary>
        /// Tests circle command with literal radius.
        /// </summary>
        [TestMethod]
        public void Execute_CircleSuccess_WithLiteralRadius()
        {
            //Setup
            string[] parameters = { "circle", "50" };
            
            //Action
            circleCommand.Execute(shapeFactory, parameters, false);
            Circle circle = (Circle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Circle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(50, circle.radius);
        }

        /// <summary>
        /// Tests circle command with a variable passed as the radius. 
        /// </summary>
        [TestMethod]
        public void Execute_CircleSuccess_WithVariableRadius()
        {
            //Setup
            variableManager.AddVariable("radius", 100);
            string[] parameters = { "circle", "radius" };

            //Action
            circleCommand.Execute(shapeFactory, parameters, false);
            Circle circle = (Circle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Circle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, circle.radius);
        }

        /// <summary>
        /// Test checking circle isn't drawn when syntax mode is true. 
        /// </summary>
        [TestMethod]
        public void Execute_SyntaxCheck_Success()
        {
            //Setup 
            string[] parameters = { "circle", "50" };

            //Action
            circleCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count);
        }

        /// <summary>
        /// Test ensuring the parameter count exception is thrown when too few parameters are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException()
        {
            //Setup
            string[] parameters = { "circle" };

            //Action
            circleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring command exception is thrown when an invalid radius is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_ThrowsException_CommandException()
        {
            //Setup
            string[] parameters = { "circle", "1nv4l1drad1u?"};

            //Action
            circleCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
