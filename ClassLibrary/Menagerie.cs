using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Menagerie : IComponent
    {
        public Cage MainCage { get; } = new Cage();

        public bool IsNight { get; private set; } = false;

        private NoiseHandler handler;

        private CageSelector cageSelector;

        public Menagerie()
        {
            var nightHandler = new NightNoiseHandler();
            var noAnimalsHandler = new NoAnimalsNoiseHandler();
            var dayHandler = new DayNoiseHandler();

            noAnimalsHandler.Successor = nightHandler;
            nightHandler.Successor = dayHandler;

            handler = noAnimalsHandler;

            var addCageSelector = new CheckAddCageCageSelector();
            var leastPopulatedCageSelector = new LeastPopulatedCageSelector();
            var fewAnimalsSelector = new FewAnimalsCageSelector();

            fewAnimalsSelector.Successor = addCageSelector;
            addCageSelector.Successor = leastPopulatedCageSelector;            
            leastPopulatedCageSelector.Successor = fewAnimalsSelector;

            this.cageSelector = fewAnimalsSelector;
        }

        public void AddAnimal(Animal animal)
        {
            var selectedCage = cageSelector.SelectCage(this.MainCage, animal);

            selectedCage.Children.Add(animal);
        }

        public void SetNight()
        {
            if (IsNight) throw new IsNightException();
            foreach(var animal in this.GetAnimals())
            {
                if (!animal.IsSleeping)
                {
                    animal.NightNight();
                }
            }
            IsNight = true;
        }

        public void SetDay()
        {
            if (!IsNight) throw new IsDayException();
            foreach(var animal in this.GetAnimals())
            {
                if (animal.IsSleeping)
                {
                    animal.WakeUp();
                }
            }
            IsNight = false;
        }

        public string SaySomething()
        {
            return handler.Handle(this);
        }

        public List<Animal> GetAnimals() => this.MainCage.GetAnimals();

        public override string ToString()
        {
            return "Menagerie with population " + this.GetAnimals().Count() + Environment.NewLine + this.MainCage.ToString();
        }
    }
}
