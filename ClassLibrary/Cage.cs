using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Cage : IComponent
    {
        public List<Animal> GetAnimals() => Children.Aggregate(
            new List<Animal>(), 
            (prev, next) => { prev.AddRange(next.GetAnimals()); return prev; });

        public List<IComponent> Children { get; internal set; } = new List<IComponent>();

        public virtual string SaySomething()
        {
            return Children.Count == 0 ? "" : Children.Aggregate("", (prev, next) => prev + next.SaySomething());
        }

        public override string ToString()
        {
            var result = $"Cage with population {this.GetAnimals().Count} ({this.Children.OfType<Animal>().Count()}) {Environment.NewLine}";
            foreach(var comp in Children)
            {
                result += "->" + comp.ToString() + Environment.NewLine;
            }
            return result;
        }
    }
}
