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
    public class MenagerieTests
    {

        [TestMethod()]
        public void AddAnimalTest()
        {
            var menagerie = new Menagerie();

            var animal = new Wolf("test", 1);
            menagerie.AddAnimal(animal);

            Assert.AreEqual(1, menagerie.GetAnimals().Count);
            Assert.IsTrue(menagerie.GetAnimals().Contains(animal));            
        }

        [TestMethod()]
        public void SetNightTest()
        {
            var menagerie = new Menagerie();

            var animal1 = new Wolf("test", 1);
            var animal2 = new Bear("test2", 2);
            animal2.NightNight();

            menagerie.AddAnimal(animal1);
            menagerie.AddAnimal(animal2);

            menagerie.SetNight();

            Assert.IsTrue(animal1.IsSleeping);
            Assert.IsTrue(animal2.IsSleeping);
        }

        [TestMethod()]
        public void SetNightInvalidTest()
        {
            var menagerie = new Menagerie();

            menagerie.SetNight();
            Assert.ThrowsException<IsNightException>(() => menagerie.SetNight());
        }

        [TestMethod()]
        public void SetDayTest()
        {
            var menagerie = new Menagerie();
            menagerie.SetNight();

            var animal1 = new Wolf("test", 1);
            var animal2 = new Bear("test2", 2);
            animal2.NightNight();

            menagerie.AddAnimal(animal1);
            menagerie.AddAnimal(animal2);

            menagerie.SetDay();

            Assert.IsFalse(animal1.IsSleeping);
            Assert.IsFalse(animal2.IsSleeping);
        }

        [TestMethod()]
        public void SetDayInvalidTest()
        {
            var menagerie = new Menagerie();

            Assert.ThrowsException<IsDayException>(() => menagerie.SetDay());
        }
    }
}