using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class Universe
    {
        private ulong uniage;

        public Universe()
        {
            uniage = 13700000000;
        }

        public ulong universeage 
        {
            get 
            {
                return uniage;
            }
            set 
            {
                uniage = value;
            }
        }

        public void ageUniverse(ulong _uniage) 
        {
            Random random = new Random();

            if (uniage <= 20000000000)
            {
                double randomrangeearly = random.NextDouble() * (1.05 - 1.07) + 1.05;
                uniage = (ulong)(randomrangeearly * _uniage);
            }
            else if (uniage <= 50000000000)
            {
                double randomrangeaging = random.NextDouble() * (1.07 - 1.11) + 1.07;
                uniage = (ulong)(randomrangeaging * _uniage);
            }
            else if (uniage <= 200000000000)
            {
                double randomrangeold = random.NextDouble() * (1.11 - 1.13) + 1.1;
                uniage = (ulong)(randomrangeold * _uniage);
            }
            else if (uniage < 1000000000000) 
            {
                uniage = 1000000000000;
                // once the universe gets this old give the label a fake interger overflow error. Possibly end of game.
            }
        }
    }
}
