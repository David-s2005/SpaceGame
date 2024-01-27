using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Star : CelestialObject
    {
        Random random = new Random();

        private string type;

        public string Type 
        {
            get 
            {
                return type;
            }
            set 
            {
                type = value;
            }
        }

        private ulong maxage;

        public ulong Maxage 
        {
            get 
            {
                return maxage;
            }
            set 
            {
                maxage = value;
            }
        }

        private int weight;

        public int Weight 
        {
            get 
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        private Planet planet;

        public Planet Planet
        {
            get 
            {
                return planet;
            }
            set 
            {
                planet = value;
            }
        }

        private Hashtable planetlist = new Hashtable();

        public Hashtable PlanetList
        {
            get
            {
                return planetlist;
            }
            set
            {
                planetlist = value;
            }
        }

        private Hashtable systemplanets = new Hashtable();

        public Hashtable SystemPlanets 
        {
            get 
            {
                return systemplanets;
            }
            set 
            {
                systemplanets = value;
            }
        }

        // THIS IS IMPORTANT. This constructor will generate a number of planets. Each planets constructors must generate the planets unique values.
        Star() 
        {
            int range = planetlist.Count - 1;
            int totalPlanets = random.Next(1, 6);
            int planetPointer = random.Next(0, range);
            int iteration = 0;

            while (iteration < totalPlanets)
            {
                planetPointer = random.Next(0, range);

            }
        }
    }
}
