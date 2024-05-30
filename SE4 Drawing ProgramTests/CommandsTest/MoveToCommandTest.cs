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
    [TestClass()]
    public class MoveToCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private MoveToCommand moveToCommand;


        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            moveToCommand = new MoveToCommand(variableManager);
        }

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

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_NoCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "moveto" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_TooFewCoordinatesPassed()
        {
            //Setup
            string[] parameters = { "moveto", "x" };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_EmptyStringAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "moveto", "x," };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_ThrowsException_InvalidParameterCountException_WhiteSpaceAsCoordinatePassed()
        {
            //Setup
            string[] parameters = { "moveto", "x, " };

            //Action
            moveToCommand.Execute(shapeFactory, parameters, false);
        }


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
