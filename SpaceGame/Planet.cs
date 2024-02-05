using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Planet : CelestialObject
    {
        public string name;

        private string imagesource;

        public string ImageSource 
        {
            get 
            {
                return imagesource;
            }
            set 
            {
                imagesource = value;
            }
        }

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

        private double weight;

        public double Weight
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

        private int habitability;

        public int Habitability
        {
            get
            {
                return habitability;
            }
            set
            {
                habitability = value;
            }
        }

        private Dictionary<Resource, int> resourcelist = new Dictionary<Resource, int>();

        public Dictionary<Resource, int> ResourceList 
        {
            get 
            {
                return resourcelist;
            }
            set 
            {
                resourcelist = ResourceList;
            }
        }

        private double anomalyweight;

        public double AnomalyWeight
        {
            get
            {
                return anomalyweight;
            }
            set
            {
                anomalyweight = value;
            }
        }

        private string description;

        public string Description 
        {
            get 
            {
                return description;
            }
            set 
            {
                description = value;
            }
        }

        private string corruptingdescription;

        public string CorruptingDescription 
        {
            get 
            {
                return corruptingdescription;
            }
            set 
            {
                this.corruptingdescription = value;
            }
        }

        private string corrupteddescription;

        public string CorruptedDescription 
        {
            get 
            {
                return corrupteddescription;
            }
            set 
            {
                corrupteddescription = value;
            }
        }

        public List<String> PlanetNames = new List<String>()
        {
            "Milmiabos",
            "Midaozuno",
            "Occides",
            "Ntennonoe",
            "Nauturn",
            "Chuiter",
            "Leyatania",
            "Brubicarro",
            "Strorth",
            "Nsurn",
            "Ophocarro",
            "Zutretune",
            "Ingadus",
            "Sebroth",
            "Curu",
            "Loatania",
            "Bihoter",
            "Phosoruta",
            "Ilyria",
            "Grillon",
            "Nesenope",
            "Duchazuno",
            "Zichora",
            "Endion",
            "Youphus",
            "Peuclite",
            "Chuurus",
            "Gnegilea",
            "Drora",
            "Ileshan"
        };

        public Planet(string _type, ulong _maxage, double _weight, int _habitability, Dictionary<Resource, int> _PassedResources, double _anomalyweight,
                      string _description, string _corruptingdescription, string _corrupteddescription) 
        {
            this.type = _type;
            this.maxage = _maxage;
            this.weight = _weight;
            this.habitability = _habitability;
            this.resourcelist = _PassedResources;
            this.anomalyweight = _anomalyweight;
            this.description = _description;
            this.corruptingdescription = _corruptingdescription;
            this.corrupteddescription = _corrupteddescription;
        }

        // Use this method to execute a random anomaly event when the anomaly detection is successful
        public void exploreAnomaly()
        {
        
        }
    }
}
