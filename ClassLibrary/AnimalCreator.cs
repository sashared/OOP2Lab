using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AnimalCreator
    {
        private static Random r = new Random();

        public Animal CreateAnimal(string name, double weight)
        {
            var d = r.NextDouble();

            if (d <= 0.4)
            {
                return new Wolf(name, weight);
            }
            else if (d <= 0.8)
            {
                return new Bear(name, weight);
            }
            else
            {
                return new Giraffe(name, weight);
            }
        }
    }
}
