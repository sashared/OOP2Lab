using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class NoiseHandler
    {
        public NoiseHandler Successor { get; set; }

        public abstract string Handle(Menagerie m);
    }

    public class NoAnimalsNoiseHandler : NoiseHandler
    {
        public override string Handle(Menagerie m)
        {
            if (m.GetAnimals().Count == 0)
            {
                return "No animals in the menagerie";
            }
            else
            {
                return Successor.Handle(m);
            }
        }
    }

    public class NightNoiseHandler : NoiseHandler
    {
        public override string Handle(Menagerie m)
        {
            if (m.IsNight)
            {
                return "Shhh... It's the night";
            }
            else
            {
                return Successor.Handle(m);
            }
        }
    }

    public class DayNoiseHandler : NoiseHandler
    {
        public override string Handle(Menagerie m)
        {
            return m.GetAnimals()
                .Select(a => a.SaySomething())
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
        }
    }
}
