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
using System.Drawing;

namespace SE4_Drawing_ProgramTests.CommandsTest
{
    /// <summary>
    /// Class testing the FlashCommand class
    /// </summary>
    [TestClass()]
    public class FlashCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private VariableManager variableManager;
        private FlashingCommand flashingCommand;

        /// <summary>
        /// Initialising the classes needed for testing
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            shapeFactory = new ShapeFactory(panel);
            flashingCommand = new FlashingCommand();
        }

        /// <summary>
        /// Test ensuring the flash command input is correct, exposed the colours array in shapefactory which will verify the logic across mutliple classes
        /// </summary>
        [TestMethod]
        public void Execute_FlashingSuccess_RedGreen()
        {
            //Setup
            string[] parameters = { "flash", "redgreen" };

            //Action
            flashingCommand.Execute(shapeFactory, parameters, false);
            
            //Assert
            Assert.AreEqual(2, shapeFactory.flashingColours.Length);
            Assert.AreEqual(Color.Red, shapeFactory.flashingColours[0]);
            Assert.AreEqual(Color.Green, shapeFactory.flashingColours[1]);
        }

        /// <summary>
        /// Test ensuring the flash command input is correct when in syntax check mode so nothing flashes
        /// </summary>
        [TestMethod]
        public void Execute_FlashingSuccessSyntaxCheck_RedGreen()
        {
            //Setup
            string[] parameters = { "flash", "redgreen" };

            //Action
            flashingCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.AreEqual(null, shapeFactory.flashingColours);
        }

        /// <summary>
        /// Test ensuring that an invalid parameter count exception is thrown when too few parameters are passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_FlashingParamFailure_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "flash" };

            //Action
            flashingCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring that a command exception is thrown when a colour not matching an entry in the colourMap dictionary is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_FlashingFailureInvalidColour_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "flash", "yojimbo" };

            //Action
            flashingCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Test ensuring that a command exception is thrown when a colour not matching an entry in the colourMap dictionary is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_FlashingFailureTooManyColoursPassed_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "flash", "redgreenyellow" };

            //Action
            flashingCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
