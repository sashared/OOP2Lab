using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class CageSelector
    {
        public CageSelector Successor { get; set; }

        public abstract Cage SelectCage(Cage rootCage, Animal animal);
    }

    public class CheckAddCageCageSelector : CageSelector
    {
        public override Cage SelectCage(Cage rootCage, Animal animal)
        {
            if (rootCage.Children.OfType<Cage>().Count() < 3)
            {
                var newCage = new Cage();
                rootCage.Children.Add(newCage);
                return newCage;
            }
            else
            {
                return Successor.SelectCage(rootCage, animal);
            }
        }
    }

    public class FewAnimalsCageSelector : CageSelector
    {
        public override Cage SelectCage(Cage rootCage, Animal animal)
        {
            if (rootCage.Children.OfType<Animal>().Count() < 3 &&
                rootCage.Children.OfType<Animal>().All(a => a.GetType() == animal.GetType()))
            {
                return rootCage;
            }
            return Successor.SelectCage(rootCage, animal);
        }
    }

    public class LeastPopulatedCageSelector : CageSelector
    {
        public override Cage SelectCage(Cage rootCage, Animal animal)
        {
            var childCages = rootCage.Children.OfType<Cage>().ToList();
            var leastPopulatedCage = childCages.OrderBy(c => c.GetAnimals().Count()).First();

            return Successor.SelectCage(leastPopulatedCage, animal);
        }
    }
}
