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
    public class AnimalTests
    {
        [TestMethod]
        public void CreateAnimalInvalidTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Wolf("test", -1));
        }

        [TestMethod()]
        public void GetAnimalsTest()
        {
            var animal = new Wolf("test", 1);

            var result = animal.GetAnimals();

            Assert.IsTrue(result.Count == 1 && result[0] == animal);
        }

        [TestMethod()]
        public void WakeUpTest()
        {
            var animal = new Giraffe("test", 1);

            animal.NightNight();
            animal.WakeUp();

            Assert.IsFalse(animal.IsSleeping);
        }

        [TestMethod()]
        public void WakeUpInvalidTest()
        {
            var animal = new Giraffe("test", 1);

            Assert.ThrowsException<IsNotSleepingException>(() => animal.WakeUp());
        }

        [TestMethod()]
        public void NightNightTest()
        {
            var animal = new Bear("test", 1);

            animal.NightNight();

            Assert.IsTrue(animal.IsSleeping);
        }

        [TestMethod()]
        public void NightNightInvalidTest()
        {
            var animal = new Bear("test", 1);

            animal.NightNight();

            Assert.ThrowsException<IsSleepingException>(() => animal.NightNight());
        }

        [TestMethod()]
        public void SaySomethingTest()
        {
            var animal = new Giraffe("test", 5);

            var result = animal.SaySomething();

            Assert.AreEqual("Giraffe test said hello", result);
        }

        [TestMethod()]
        public void SaySomethingIsSleepingTest()
        {
            var animal = new Wolf("test test", 5);
            animal.NightNight();

            var result = animal.SaySomething();

            Assert.AreEqual("Wolf test test is sleeping", result);
        }
    }
}