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
    [TestClass()]
    public class CircleCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private CircleCommand circleCommand;
        
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            circleCommand = new CircleCommand(variableManager);
        }

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

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException()
        {
            //Setup
            string[] parameters = { "circle" };

            //Action
            circleCommand.Execute(shapeFactory, parameters, false);
        }

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
