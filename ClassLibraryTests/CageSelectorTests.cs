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
    public class CageSelectorTests
    {
        [TestMethod()]
        public void CheckAddCageTest()
        {
            var rootCage = new Cage();
            var animal = new Wolf("test", 1);

            var result = new CheckAddCageCageSelector().SelectCage(rootCage, animal);

            Assert.AreEqual(1, rootCage.Children.Count);
            Assert.AreEqual(result, rootCage.Children[0]);
        }

        [TestMethod()]
        public void CheckAddCageTooManyCagesTest()
        {
            var rootCage = new Cage();
            for (int i = 0; i < 3; ++i)
            {
                rootCage.Children.Add(new Cage());
            }
            var animal = new Wolf("test", 1);

            var mockSelector = GetCageSelectorMock();

            var cageSelector = new CheckAddCageCageSelector();
            cageSelector.Successor = mockSelector.Object;

            var result = cageSelector.SelectCage(rootCage, animal);
            mockSelector.Verify(c => c.SelectCage(rootCage, animal));
            Assert.AreEqual(3, rootCage.Children.Count);
        }

        [TestMethod]
        public void FewAnimalsCageSelectorTest()
        {
            var rootCage = new Cage();
            var animal = new Wolf("test", 1);

            var result = new FewAnimalsCageSelector().SelectCage(rootCage, animal);

            Assert.AreEqual(0, rootCage.Children.Count);
            Assert.AreEqual(result, rootCage);
        }

        [TestMethod]
        public void FewAnimalsCageSelectorIncompatibleAnimalsTest()
        {
            var rootCage = new Cage();
            rootCage.Children.Add(new Bear("bear", 1));
            var animal = new Wolf("test", 1);

            var mockSelector = GetCageSelectorMock();

            var cageSelector = new FewAnimalsCageSelector();
            cageSelector.Successor = mockSelector.Object;

            var result = cageSelector.SelectCage(rootCage, animal);
            mockSelector.Verify(c => c.SelectCage(rootCage, animal));
        }

        [TestMethod]
        public void FewAnimalsCageSelectorCompatibleAnimalsTest()
        {
            var rootCage = new Cage();
            rootCage.Children.Add(new Wolf("wolf1", 1));
            rootCage.Children.Add(new Wolf("wolf2", 1));
            var animal = new Wolf("test", 1);

            var result = new FewAnimalsCageSelector().SelectCage(rootCage, animal);

            Assert.AreEqual(2, rootCage.Children.Count);
            Assert.AreEqual(result, rootCage);
        }

        [TestMethod]
        public void FewAnimalsCageSelectorTooManyAnimalsTest()
        {
            var rootCage = new Cage();
            rootCage.Children.Add(new Wolf("wolf1", 1));
            rootCage.Children.Add(new Wolf("wolf2", 1));
            rootCage.Children.Add(new Wolf("wolf3", 1));
            var animal = new Wolf("test", 1);

            var mockSelector = GetCageSelectorMock();

            var cageSelector = new FewAnimalsCageSelector();
            cageSelector.Successor = mockSelector.Object;

            var result = cageSelector.SelectCage(rootCage, animal);
            mockSelector.Verify(c => c.SelectCage(rootCage, animal));
        }

        [TestMethod]
        public void LeastPopulatedCageSelectorTest()
        {
            var rootCage = new Cage();

            var animal = new Wolf("test", 1);
            var animal1 = new Wolf("1", 1);
            var animal2 = new Wolf("2", 1);

            var cage1 = new Cage();
            var cage2 = new Cage();
            var cage3 = new Cage();

            cage1.Children.Add(animal1);
            cage3.Children.Add(animal2);

            rootCage.Children.Add(cage1);
            rootCage.Children.Add(cage2);
            rootCage.Children.Add(cage3);

            var mockSelector = GetCageSelectorMock();

            var cageSelector = new LeastPopulatedCageSelector();
            cageSelector.Successor = mockSelector.Object;

            var result = cageSelector.SelectCage(rootCage, animal);
            mockSelector.Verify(c => c.SelectCage(cage2, animal));
        }

        private Mock<CageSelector> GetCageSelectorMock()
        {
            var mockSelector = new Mock<CageSelector>();
            mockSelector.Setup(cs => cs.SelectCage(It.IsAny<Cage>(), It.IsAny<Animal>()))
                .Returns((Cage c, Animal a) => c);
            return mockSelector;
        }
    }
}