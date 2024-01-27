using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class SpaceCraft
    {
        // new Resource("Iron", 0),
        // new Resource("Copper", 0),
        // new Resource("Platinum", 0),
        // new Resource("Uranium", 0),
        // new Resource("Silicon", 0)

        private int health;

        public int Health 
        {
            get 
            {
                return health;
            }
            set 
            {
                health = value;
            }
        }

        private int healthregen;

        public int HealthRegen 
        {
            get 
            { 
                return healthregen;
            }
            set 
            {
                healthregen = value;
            }
        }

        private int maxhealth;

        public int MaxHealth 
        {
            get 
            {
                return maxhealth;
            }
            set 
            {
                maxhealth = value;
            }
        }

        private int shield;

        public int Shield 
        {
            get 
            {
                return shield;
            }
            set 
            {
                shield = value;
            }
        }
        private int shieldregen;

        public int ShieldRegen 
        {
            get 
            {
                return shieldregen;
            }
            set 
            {
                shieldregen = value;
            }
        }

        private int maxshield;

        public int MaxShield 
        {
            get 
            {
                return maxshield;
            }
            set 
            {
                maxshield = value;
            }
        }

        private int science;

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

        private List<Resource> shipResource = new List<Resource>();

        public List<Resource> ShipResource 
        {
            get 
            {
                return shipResource;
            }
            set 
            {
                shipResource = value;
            }
        }

        private List<Module> moduleList = new List<Module>();

        public List<Module> ModuleList 
        {
            get 
            {
                return moduleList;
            }
            set 
            {
                moduleList = value;
            }
        }

        public void InflictDamage(int _damage) 
        {
            if (shield <= _damage)
            {
                health -= (_damage + shield);
                shield = 0;
            }
            else 
            {
                shield -= _damage;
            }
        }

        public void RegenerateStat() 
        {
            // This part of the code figures out if the regeneration rate is greater than the difference between the current shield health
            // and the maximum shield health. If so then the shield will be set to the max shield otherwise the normal regeneration amount
            // is added to the shield.
            if (shield != maxshield) 
            {
                if ((maxshield - shield) < shieldregen)
                {
                    shield = maxshield;
                }
                else 
                {
                    shield += shieldregen;
                }
            }
            // Read comment above, its essentially the same but for health. Just replace "shield" with "health".
            if (health != maxhealth) 
            {
                if ((maxhealth - health) < healthregen)
                {
                    health = maxhealth;
                }
                else 
                {
                    health += maxhealth;
                }
            }
        }
    }
}
