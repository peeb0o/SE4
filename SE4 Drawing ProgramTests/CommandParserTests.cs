using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE4.Tests
{
    [TestClass()]
    public class CommandParserTests
    {
        private CommandParser commandParser;
        private ShapeFactory shapeFactory;
        private Panel panel;

        [TestInitialize]
        public void Setup()
        {
            panel = new Panel();
            shapeFactory = new ShapeFactory(panel);
            commandParser = new CommandParser(shapeFactory);
        }

        [TestMethod()]
        public void ParseCommand_DrawTo_Success()
        {
            //Setup
            string command = "drawto 100,100";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(100, shapeFactory.penY);
        }

        [TestMethod()]
        public void ParseCommand_MoveTo_Success()
        {
            //Setup
            string command = "moveto 100,100";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.AreEqual(100, shapeFactory.penX);
            Assert.AreEqual(100, shapeFactory.penY);
        }

        [TestMethod()]
        public void ParseCommand_PenColour_Success()
        {
            //Setup
            string command = "pen red";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.AreEqual(shapeFactory.penColor, Color.Red);
        }

        [TestMethod()]
        public void ParseCommand_FillOn_Success()
        {
            //Setup
            string command = "fill on";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.AreEqual(shapeFactory.fill, true);
        }

        [TestMethod()]
        public void ParseCommand_Circle_Success()
        {
            //Setup
            string command = "circle 20";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Circle);
        }

        [TestMethod()]
        public void ParseCommand_Rectangle_Success()
        {
            //Setup
            string command = "rectangle 200,100";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Rectangle);
        }

        [TestMethod()]
        public void ParseCommand_Triangle_Success()
        {
            //Setup
            string command = "triangle 20";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.IsTrue(shapeFactory.shapes.Last() is Triangle);
        }

        [TestMethod()]
        public void ParseCommand_Reset_Success()
        {
            //Setup
            string command = "reset";

            //Action
            commandParser.ParseCommand(command);

            //Assert
            Assert.AreEqual(0, shapeFactory.penX);
            Assert.AreEqual(0, shapeFactory.penY);
        }

        [TestMethod()]
        public void ParseCommand_Clear_Success()
        {
            //Setup
            shapeFactory.AddShape(new Circle(Color.Black, 100, 100, 100, false));
            shapeFactory.AddShape(new Rectangle(Color.Black, 100, 100, 200, 200, false));

            string command = "clear";

            //Action
            commandParser.ParseCommand(command);

            //Assert
           Assert.AreEqual(0, shapeFactory.shapes.Count);
        }
    }
}