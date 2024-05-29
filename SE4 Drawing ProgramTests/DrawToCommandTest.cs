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
    [TestClass()]
    public class DrawToCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private DrawToCommand drawToCommand;

        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            drawToCommand = new DrawToCommand(variableManager);
        }

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

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_NoCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "drawto" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_TooFewCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "drawto", "x" };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "drawto", "x," };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_WhiteSpaceAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "drawto", "x, " };

            //Action
            drawToCommand.Execute(shapeFactory, parameters, false);
        }


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
