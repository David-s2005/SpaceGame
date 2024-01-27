using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public abstract class CelestialObject : Universe
    {
        private int science;

        public CelestialObject(int _science, string _name, string _description) 
        {
            science = _science;
            name = _name;
            description = _description;
        }
        public CelestialObject() 
        {
            science = 0;
            name = "No-Name";
            description = "No Description";
        }

        public int Science
        {
            get
            {
                return science;
            }
            set
            {
                science = value;
            }
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            // setter checks if the passed value is null
            set
            {
                if (value == null)
                {
                    value = "null";
                    name = value;
                }
                else
                {
                    name = value;
                }
            }
        }

        private string description;

        public string Description
        {
            get 
            {
                return description; 
            }
            // setter checks if the passed value is null
            set
            {
                if (value == null)
                {
                    value = "null";
                    description = value;
                }
                else
                {
                    description = value;
                }
            }
        }
    }
}
