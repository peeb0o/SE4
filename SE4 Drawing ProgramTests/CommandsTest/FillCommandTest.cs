using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE4;
using SE4.Exceptions;
using SE4.Variables;
using System.Windows.Forms;


namespace SE4_Drawing_ProgramTests.CommandsTest
{
    [TestClass()]
    public class FillCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private FillCommand fillCommand;

        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            fillCommand = new FillCommand();
        }

        [TestMethod]
        public void Execute_FillCommand_SuccessOn()
        {
            //Setup
            string[] parameters = { "fill", "on" };
            string[] circleParams = { "circle", "100" };
            CircleCommand circleCommand = new CircleCommand(variableManager);

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
            circleCommand.Execute(shapeFactory, circleParams, false);

            //Assert
            Circle circle = (Circle)shapeFactory.shapes.Last();
            Assert.AreEqual(true, shapeFactory.GetFill());
            Assert.AreEqual(true, circle.IsFilled());
        }

        [TestMethod]
        public void Execute_FillCommand_SuccessOff()
        {
            //Setup
            string[] parameters = { "fill", "off" };
            string[] circleParams = { "circle", "100" };
            CircleCommand circleCommand = new CircleCommand(variableManager);

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
            circleCommand.Execute(shapeFactory, circleParams, false);

            //Assert
            Circle circle = (Circle)shapeFactory.shapes.Last();
            Assert.AreEqual(false, shapeFactory.GetFill());
            Assert.AreEqual(false, circle.IsFilled());
        }

        [TestMethod]
        public void Execute_FillCommand_Success_SyntacCheckTrue()
        {
            //Setup
            string[] parameters = { "fill", "on" };

            //Action 
            fillCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(false, shapeFactory.GetFill());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_FillCommand_ThrowsException_InvalidParameterCount_TooFewParameters()
        {
            //Setup
            string[] parameters = { "fill",};

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FillCommand_ThrowsException_InvalidCommand_Passed()
        {
            //Setup
            string[] parameters = { "fill", "blablabla"};

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FillCommand_ThrowsException_InvalidCommand_PassedEmpty()
        {
            //Setup
            string[] parameters = { "fill", "" };

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FillCommand_ThrowsException_InvalidCommand_PassedWhiteSpace()
        {
            //Setup
            string[] parameters = { "fill", "  " };

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
