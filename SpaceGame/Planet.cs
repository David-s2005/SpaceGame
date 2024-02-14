using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Planet : CelestialObject
    {
        Random random = new Random();

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

        Dictionary<int, string> planetNames = new Dictionary<int, string>()
        {
            {1, "Milmiabos" },
            {2, "Midaozuno" },
            {3, "Occides" },
            {4, "Ntennonoe" },
            {5, "Nauturn" },
            {6, "Chuiter" },
            {7, "Leyatania" },
            {8, "Brubicarro" },
            {9, "Strorth" },
            {10, "Nsurn" },
            {11, "Ophocarro" },
            {12, "Zutretune" },
            {13, "Ingadus" },
            {14, "Sebroth" },
            {15, "Curu" },
            {16, "Loatania" },
            {17, "Bihoter" },
            {18, "Phosoruta" },
            {19, "Ilyria" },
            {20, "Grillon" },
            {21, "Nesenope" },
            {22, "Duchazuno" },
            {23, "Zichora" },
            {24, "Endion" },
            {25, "Youphus" },
            {26, "Peuclite" },
            {27, "Chuurus" },
            {28, "Gnegilea" },
            {29, "Drora" },
            {30, "Ileshan" }
        };

        public List<String> usedNames = new List<String>();

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

        public void generatePlanetNames(int _totalPlanets)
        {
            usedNames.Clear();
            string generatedName;
            bool isUnique;

            for (int i = 0; i != _totalPlanets; i++)
            {
                isUnique = false;

                while (isUnique == false)
                {
                    generatedName = planetNames[random.Next(1, planetNames.Count)];
                    if (usedNames.Contains(generatedName))
                    {
                        break;
                    }
                    else
                    {
                        isUnique = true;
                        name = generatedName;
                        usedNames.Add(generatedName);
                    }
                }
            }
        }

        // Use this method to execute a random anomaly event when the anomaly detection is successful
        public void exploreAnomaly()
        {
        
        }
    }
}
