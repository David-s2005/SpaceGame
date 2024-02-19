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

        private Dictionary <Resource, int> anomalyResources = new Dictionary <Resource, int> ();

        public Uri UriAnomaly = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png");

        public Anomaly(string _name, ulong _maxage, string _description, int _damage, int _science, Dictionary<Resource, int> _anomalyresources, string _imageSource) // use this constructor when the anomaly has no unique events
        {
            name = _name;
            maxage = _maxage;
            Description = _description;
            Damage = _damage;
            Science = _science;
            anomalyResources = _anomalyresources;
            UriAnomaly = new Uri(_imageSource);
        }

        public Anomaly(string _name, ulong _maxage, string _description, int _damage, int _science, Dictionary<Resource, int> _anomalyresources, AnomalyAction uniqueAction, string _imageSource)// this constructor will be used for unique events 
        {                                                                                                                                                                   // e.g. a anomaly granting a unique tech.
            name = _name;
            maxage = _maxage;
            Description = _description;
            Damage = _damage;
            Science = _science;
            anomalyResources = _anomalyresources;
            this.uniqueAction = uniqueAction;
            UriAnomaly = new Uri(_imageSource);
        }
    }
}
