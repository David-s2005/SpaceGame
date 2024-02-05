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

        private List<Planet> planetlist = new List<Planet>(); // This list contains ALL planets!

        public List<Planet> PlanetList
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

        private List<Planet> systemplanets = new List<Planet>(); // this list simply contains the planets orbiting the star, there is a range of 1 -> 6.

        public List<Planet> SystemPlanets 
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
        public Star(List<Planet> _planetlist) 
        {
            int range = _planetlist.Count - 1; // range stores the amount of planets within the passed planetList parameter
            int totalPlanets = random.Next(1, 6); // The amount of planets within the system can range from 1 -> 6
            int planetPointer; // Not initalized yet, but will store a random number from 0 to the length of the passed planet list. This is used to select a random planet from the planetlist among other uses.
            int iteration = 0; 

            while (iteration < totalPlanets)
            {
                planetPointer = random.Next(0, range);
                Planet planet = _planetlist[planetPointer]; // Random planet from the planet list is selected and passed to the Planet varible.
                planet.Name = planet.PlanetNames[planetPointer]; // Random name is selected for the planet.
                systemplanets.Add(planet);

                for (int iterator = 0; iterator <= totalPlanets; iterator++) // This for loop is used to find if the current planets name is identical to other planets in the system.
                {
                    if (planetlist.Count == 0) 
                    {
                        break;
                    }
                    else if (systemplanets[iterator].Name == planet.Name)
                    {
                        planet.Name = planet.PlanetNames[random.Next(0, planet.PlanetNames.Count)];
                        iterator = 0;
                    }
                }
                iteration++;
            }
        }
    }
}
