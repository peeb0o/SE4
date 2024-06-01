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
    /// <summary>
    /// Class testing the FillCommand class.
    /// </summary>
    [TestClass()]
    public class FillCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private FillCommand fillCommand;
        private CircleCommand circleCommand;

        /// <summary>
        /// Method initialising the required classes for testing. 
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            fillCommand = new FillCommand();
            circleCommand = new CircleCommand(variableManager);
        }

        /// <summary>
        /// Test ensuring the fill command works by drawing a filled circle. 
        /// </summary>
        [TestMethod]
        public void Execute_FillCommand_SuccessOn()
        {
            //Setup
            string[] parameters = { "fill", "on" };
            string[] circleParams = { "circle", "100" };
           
            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
            circleCommand.Execute(shapeFactory, circleParams, false);

            //Assert
            Circle circle = (Circle)shapeFactory.shapes.Last();
            Assert.AreEqual(true, shapeFactory.GetFill());
            Assert.AreEqual(true, circle.IsFilled());
        }

        /// <summary>
        /// Test ensuring the fill command works when set to off -> regular circle drawn. 
        /// </summary>
        [TestMethod]
        public void Execute_FillCommand_SuccessOff()
        {
            //Setup
            string[] parameters = { "fill", "off" };
            string[] circleParams = { "circle", "100" };

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
            circleCommand.Execute(shapeFactory, circleParams, false);

            //Assert
            Circle circle = (Circle)shapeFactory.shapes.Last();
            Assert.AreEqual(false, shapeFactory.GetFill());
            Assert.AreEqual(false, circle.IsFilled());
        }

        /// <summary>
        /// Test ensuring that fill value doesn't change when command is passed in syntax mode. 
        /// </summary>
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

        /// <summary>
        /// Test ensuring parameter count exception is thrown when too few parameters passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_FillCommand_ThrowsException_InvalidParameterCount_TooFewParameters()
        {
            //Setup
            string[] parameters = { "fill",};

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring command exception is thrown when invalid value passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FillCommand_ThrowsException_InvalidCommand_Passed()
        {
            //Setup
            string[] parameters = { "fill", "blablabla"};

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring command exception thrown when an empty string is passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FillCommand_ThrowsException_InvalidCommand_PassedEmpty()
        {
            //Setup
            string[] parameters = { "fill", "" };

            //Action 
            fillCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring command exception thrown when a blank space is passed. 
        /// </summary>
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
