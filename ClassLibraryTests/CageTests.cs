using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class CageTests
    {
        [TestMethod()]
        public void GetAnimalsTestNoAnimals()
        {
            var cage = new Cage();
            var animals = cage.GetAnimals();
            Assert.AreEqual(0, cage.GetAnimals().Count);
        }

        [TestMethod()]
        public void GetAnimalsTestSimple()
        {
            var cage = new Cage();
            var animal = new Wolf("test", 1);
            cage.Children.Add(animal);
            var animals = cage.GetAnimals();
            Assert.AreEqual(1, cage.GetAnimals().Count);
            Assert.IsTrue(cage.GetAnimals().Exists(a => a.Name == "test" && a.Weight == 1));
        }

        [TestMethod()]
        public void GetAnimalsTestRecursive()
        {
            var cage = new Cage();
            var animal = new Wolf("test", 1);
            var cage2 = new Cage();
            cage2.Children.Add(animal);
            cage.Children.Add(cage2);
            var animals = cage.GetAnimals();
            Assert.AreEqual(1, cage.GetAnimals().Count);
            Assert.IsTrue(cage.GetAnimals().Exists(a => a.Name == "test" && a.Weight == 1));
        }

        [TestMethod()]
        public void SaySomethingTestNoAnimals()
        {
            var cage = new Cage();
            var str = cage.SaySomething();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void SaySomethingTestSimple()
        {
            var cage = new Cage();
            var animal = new Wolf("test", 1);
            cage.Children.Add(animal);
            Assert.AreEqual(animal.SaySomething(), cage.SaySomething());
        }

        [TestMethod()]
        public void SaySomethingTestRecursive()
        {
            var cage = new Cage();
            var animal = new Wolf("test", 1);
            var cage2 = new Cage();
            cage2.Children.Add(animal);
            cage.Children.Add(cage2);
            Assert.AreEqual(animal.SaySomething(), cage.SaySomething());
        }

    }
}