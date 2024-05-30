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
    [TestClass()]
    public class PenCommandTest
    {
        private ShapeFactory shapeFactory;
        private Panel panel;
        private PenCommand penCommand;

        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            shapeFactory = new ShapeFactory(panel);
            penCommand = new PenCommand();
        }

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

        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour()
        {
            //Setup
            string[] parameters = { "pen", "shrekgreen" }; //hope that's not a real colour

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour_EmptyStringPassed()
        {
            //Setup
            string[] parameters = { "pen", "" };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidColourException))]
        public void Execute_PenFail_UnknownColour_BlankSpacePassed()
        {
            //Setup
            string[] parameters = { "pen", "" };

            //Action
            penCommand.Execute(shapeFactory, parameters, false);
        }

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
