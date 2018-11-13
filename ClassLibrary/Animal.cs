using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class Animal : IComponent
    {
        public List<Animal> GetAnimals() => new List<Animal>() { this };

        public string Name { get; protected set; }

        protected double weight;
        public double Weight
        {
            get => weight;
            protected set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("Weight", "Weight must be positive");
                weight = value;
            }
        }

        public bool IsSleeping { get; protected set; } = false;

        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public virtual void WakeUp()
        {
            if (!IsSleeping) throw new IsNotSleepingException();
            IsSleeping = false;
        }

        public virtual void NightNight()
        {
            if (IsSleeping) throw new IsSleepingException();
            IsSleeping = true;
        }

        public abstract string SaySomething();
    }

    public class Giraffe : Animal
    {
        public Giraffe(string name, double weight) : base(name, weight) { }

        public override string SaySomething()
        {
            return IsSleeping ?
                $"Giraffe {Name} is sleeping" :
                $"Giraffe {Name} said hello";
        }

        public override string ToString()
        {
            return $"Giraffe {Name} with weight {Weight}{(IsSleeping ? " (sleeping)" : "")}";
        }
    }

    public class Bear : Animal
    {
        public Bear(string name, double weight) : base(name, weight) { }

        public override string SaySomething()
        {
            return IsSleeping ?
                $"Bear {Name} is sleeping" :
                $"Bear {Name} said hello";
        }

        public override string ToString()
        {
            return $"Bear {Name} with weight {Weight}{(IsSleeping ? " (sleeping)" : "")}";
        }
    }

    public class Wolf : Animal
    {
        public Wolf(string name, double weight) : base(name, weight) { }

        public override string SaySomething()
        {
            return IsSleeping ?
                $"Wolf {Name} is sleeping" :
                $"Wolf {Name} said hello";
        }

        public override string ToString()
        {
            return $"Wolf {Name} with weight {Weight}{(IsSleeping ? " (sleeping)" : "")}";
        }
    }
}
