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
    /// Class testing the TriangleCommand class
    /// </summary>
    [TestClass()]
    public class TriangleCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private TriangleCommand triangleCommand;

        /// <summary>
        /// Method initialising the classes needed for testing. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            triangleCommand = new TriangleCommand(variableManager);
        }

        /// <summary>
        /// Test ensuring that a triangle is drawn when a literal value is passed for sidelength.
        /// </summary>
        [TestMethod]
        public void Execute_TriangleSuccess_WithLiteral()
        {
            //Setup
            string[] parameters = { "triangle", "100" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, false);
            Triangle triangle = (Triangle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Triangle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, triangle.sideLength);
        }

        /// <summary>
        /// Test ensuring a triangle is drawn when a variable is passed for the sidelength.
        /// </summary>
        [TestMethod]
        public void Execute_TriangleSuccess_WithVariable()
        {
            //Setup
            variableManager.AddVariable("x", 100);
            string[] parameters = { "Triangle", "x" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, false);
            Triangle triangle = (Triangle)shapeFactory.shapes[0];

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Triangle);
            Assert.IsTrue(shapeFactory.shapes.Count == 1);
            Assert.AreEqual(100, triangle.sideLength);
        }

        /// <summary>
        /// Ensures a triangle isn't drawn when in syntax mode.
        /// </summary>
        [TestMethod]
        public void Execute_TriangleSuccess_SyntaxCheck()
        {
            //Setup
            string[] parameters = { "Triangle", "100" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(0, shapeFactory.shapes.Count());
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown when too few parameters are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException()
        {
            //Setup
            string[] parameters = { "triangle" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown when an empty string is passed as the dimension. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringPassedAsDimension()
        {
            //Setup
            string[] parameters = { "triangle", "" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a commmand exception is thrown when an invalid value is passed for the sidelength.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_ThrowsException_CommandException_InvalidValuePassedAsDimension()
        {
            //Setup
            string[] parameters = { "triangle", "1()(),2()()" };

            //Action
            triangleCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
