using SpaceGame;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public static List<Planet> systemPlanets = new List<Planet>(); // this list simply contains the planets orbiting the star, there is a range of 1 -> 6.

        static List<Planet> returnPlanets() 
        {
            return systemPlanets;
        }

        // THIS IS IMPORTANT. This constructor will generate a number of planets. Each planets constructors must generate the planets unique values.
        public Star(List<Planet> _planetlist) 
        {

        }
    }
}
