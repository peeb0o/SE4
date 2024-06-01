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
using System.Drawing;

namespace SE4_Drawing_ProgramTests.CommandsTest
{
    /// <summary>
    /// Class testing the PenCommand class.
    /// </summary>
    [TestClass()]
    public class PenCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private PenCommand penCommand;

        /// <summary>
        /// Initialises the classes needed for testing.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            shapeFactory = new ShapeFactory(panel);
            penCommand = new PenCommand();
        }

        /// <summary>
        /// Test ensuring that the pen colour is set to red.
        /// </summary>
        [TestMethod]
        public void Execute_PenSuccess_Red()
        {
            //Setup
            string[] parameters = { "pen", "red" };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);

            //Assert
            Assert.AreEqual(Color.Red, shapeFactory.GetPenColour());
        }

        /// <summary>
        /// Test ensuring that the pen colour remains the default black if command run in syntax check mode.
        /// </summary>
        [TestMethod]
        public void Execute_PenSuccess_SyntaxCheck_PenDefaultColour()
        {
            //Setup
            string[] parameters = { "pen", "red" };

            //Action
            penCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(Color.Black, shapeFactory.GetPenColour());
        }

        /// <summary>
        /// Test ensuring that the invalid colour exception is thrown if the colour passed is not known. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour()
        {
            //Setup
            string[] parameters = { "pen", "shrekgreen" }; //hope that's not a real colour

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring the invalid colour exception is thrown if the colour passed is an empty string. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour_EmptyStringPassed()
        {
            //Setup
            string[] parameters = { "pen", "" };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring the invalid colour exception is thrown if the colour passed is a blank string. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour_BlankSpacePassed()
        {
            //Setup
            string[] parameters = { "pen", "" };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring the parameter count exception is thrown if too few parameters are passed. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_PenFail_NotEnoughParams()
        {
            //Setup
            string[] parameters = { "pen", };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
