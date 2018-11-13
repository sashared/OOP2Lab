using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class NoiseHandlerTests
    {
        [TestMethod()]
        public void NoAnimalsHandleTest()
        {
            var handler = new NoAnimalsNoiseHandler();

            var menagerie = new Menagerie();

            var result = handler.Handle(menagerie);

            Assert.AreEqual("No animals in the menagerie", result);
        }

        [TestMethod()]
        public void NoAnimalsHandleInvalidTest()
        {
            var handler = new NoAnimalsNoiseHandler();

            Mock<NoiseHandler> mockHandler = GetMockHandler();

            handler.Successor = mockHandler.Object;

            var menagerie = new Menagerie();
            var animal = new Wolf("test", 1);
            menagerie.AddAnimal(animal);

            var result = handler.Handle(menagerie);

            mockHandler.Verify(mh => mh.Handle(menagerie));
        }        

        [TestMethod()]
        public void NightHandleTest()
        {
            var handler = new NightNoiseHandler();

            var menagerie = new Menagerie();
            menagerie.SetNight();

            var result = handler.Handle(menagerie);

            Assert.AreEqual("Shhh... It's the night", result);
        }

        [TestMethod()]
        public void NightHandleInvalidTest()
        {
            var handler = new NightNoiseHandler();

            Mock<NoiseHandler> mockHandler = GetMockHandler();

            handler.Successor = mockHandler.Object;

            var menagerie = new Menagerie();

            var result = handler.Handle(menagerie);

            mockHandler.Verify(mh => mh.Handle(menagerie));
        }

        private Mock<NoiseHandler> GetMockHandler()
        {
            var mockHandler = new Mock<NoiseHandler>();
            mockHandler.Setup(nh => nh.Handle(It.IsAny<Menagerie>()));
            return mockHandler;
        }
    }
}