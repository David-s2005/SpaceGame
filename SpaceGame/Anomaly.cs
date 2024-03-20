using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpaceGame
{
    class Anomaly : CelestialObject
    {
        public delegate void AnomalyAction();

        private AnomalyAction uniqueAction;

        public void ExecuteUniqueEvent() 
        {
            uniqueAction();
        }

        private string name;

        public string Name 
        {
            get 
            {
                return name;
            }
            set 
            {
                name = value;
            }
        }

        private ulong maxage;

        public ulong MaxAge
        {
            get 
            {
                return maxage;
            }
            set 
            {
                maxage = MaxAge;
            }
        }

        private String description;

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

        private int damage;

        public int Damage
        {
            get 
            {
                return damage;
            }
            set 
            {
                damage = value;
            }
        }

        public int ironQuantity;
        public int copperQuantity;
        public int platinumQuantity;
        public int uraniumQuantity;
        public int siliconQuantity;

        public Uri UriAnomaly = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png");

        public Anomaly(string _name, ulong _maxage, string _description, int _damage, int _science, List<int> _resources, string _imageSource) // use this constructor when the anomaly has no unique events
        {
            name = _name;
            maxage = _maxage;
            Description = _description;
            Damage = _damage;
            Science = _science;
            ironQuantity = _resources[0];
            copperQuantity = _resources[1];
            platinumQuantity = _resources[2];
            uraniumQuantity = _resources[3];
            siliconQuantity = _resources[4];
            UriAnomaly = new Uri(_imageSource);
        }

        public Anomaly(string _name, ulong _maxage, string _description, int _damage, int _science, List<int> _resources, AnomalyAction uniqueAction, string _imageSource)// this constructor will be used for unique events 
        {                                                                                                                                                                   // e.g. a anomaly granting a unique tech.
            name = _name;
            maxage = _maxage;
            Description = _description;
            Damage = _damage;
            Science = _science;
            ironQuantity = _resources[0];
            copperQuantity = _resources[1];
            platinumQuantity = _resources[2];
            uraniumQuantity = _resources[3];
            siliconQuantity = _resources[4];
            this.uniqueAction = uniqueAction;
            UriAnomaly = new Uri(_imageSource);
        }
    }
}
