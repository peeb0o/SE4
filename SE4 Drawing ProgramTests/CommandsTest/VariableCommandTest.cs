using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using SE4.Exceptions;
using SE4.Service;
using SE4.Variables;

namespace SE4_Drawing_ProgramTests.CommandsTest
{
    /// <summary>
    /// Class testing the VariableCommand class
    /// </summary>
    [TestClass()]
    public class VariableCommandTest
    {
        private VariableManager variableManager;
        private VariableCommand variableCommand;
        private ShapeFactory shapeFactory;
        private Panel panel;

        /// <summary>
        /// Initialises the classes required for testing
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            variableManager = VariableManager.Instance;
            variableCommand = new VariableCommand(variableManager);
            shapeFactory = new ShapeFactory(panel);
            variableManager.VariablesClear();
        }

        /// <summary>
        /// Test ensuring a variable declared without a value assigned is successful
        /// </summary>
        [TestMethod]
        public void Execute_DeclareVariableSuccess_WithoutValue()
        {
            //Setup
            string[] parameters = { "var", "y" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);

            //Assert
            Assert.IsTrue(variableManager.VariableExists("y"));
            Assert.AreEqual(0, variableManager.GetVariableValue("y"));
        }

        /// <summary>
        /// Test ensuring a variable declared with a value assigned is successful
        /// </summary>
        [TestMethod]
        public void Execute_DeclareVariableSuccess_WithValue()
        {
            //Setup
            string[] parameters = { "var", "z", "=", "10" };
           
            //Act
            variableCommand.Execute(shapeFactory, parameters, false);

            //Assert
            Assert.IsTrue(variableManager.VariableExists("z"));
            Assert.AreEqual(10, variableManager.GetVariableValue("z"));
        }

        /// <summary>
        /// Test ensuring a variable declared with syntax mode enabled doesn't add variable
        /// </summary>
        [TestMethod]
        public void Execute_DeclareVariableSyntaxCheckSuccess_WithValue()
        {
            //Setup
            string[] parameters = { "var", "a", "=", "10" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, true);

            //Assert
            Assert.IsFalse (variableManager.VariableExists("a"));
            Assert.AreEqual(0, variableManager.GetVariableValue("a"));
        }

        /// <summary>
        /// Ensures a parameter count exception is thrown when too few parameters are passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterCountException))]
        public void Execute_DeclareVariableFail_TooFewParams_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "var", "b", "=" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown when the variable validation is failed due to an invalid variable name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_DeclareVariableFail_InvalidVariableName_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "var", "!nval1d" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown when the variable being declared already exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_DeclareVariableFail_VariableAlreadyExists_ShouldThrowException()
        {
            //Setup
            variableManager.AddVariable("c", 10);
            string[] parameters = { "var", "c" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown when the variable value passed is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_DeclareVariableFail_VariableValueInvalid_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "var", "d", "=", "blabla" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown when the variable being passed with a value already exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_DeclareVariableFail_VariableWithValueExists_ShouldThrowException()
        {
            //Setup
            variableManager.AddVariable("e", 10);
            string[] parameters = { "var", "e", "=", "10" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }

        /// <summary>
        /// Ensures a command exception is thrown when the variable name passed is invalid when passed with a value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void Execute_DeclareVariableFail_VariableNameInvalid_ShouldThrowException()
        {
            //Setup
            string[] parameters = { "var", "&adsa", "=", "blabla" };

            //Act
            variableCommand.Execute(shapeFactory, parameters, false);
        }
    }
}
