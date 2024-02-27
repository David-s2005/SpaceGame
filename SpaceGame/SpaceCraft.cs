using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class SpaceCraft
    {

        private int CurrentHealth;

        public int currentHealth 
        {
            get 
            {
                return CurrentHealth;
            }
            set 
            {
                CurrentHealth = value;
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

        private int CurrentShield;

        public int currentShield 
        {
            get 
            {
                return CurrentShield;
            }
            set 
            {
                CurrentShield = value;
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

        internal Dictionary<Resource, int> ShipResource = new Dictionary<Resource, int>();

        public Dictionary<Resource, int> shipResource
        {
            get
            {
                return ShipResource;
            }
            set
            {
                ShipResource = value;
            }
        }

        internal List<Module> moduleList = new List<Module>();

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
            if (CurrentShield <= _damage)
            {
                CurrentHealth -= (_damage + CurrentShield);
                CurrentShield = 0;
            }
            else 
            {
                CurrentShield -= _damage;
            }
        }

        public void RegenerateStat() 
        {
            // This part of the code figures out if the regeneration rate is greater than the difference between the current shield health
            // and the maximum shield health. If so then the shield will be set to the max shield otherwise the normal regeneration amount
            // is added to the shield.
            if (CurrentShield != maxshield) 
            {
                if ((maxshield - CurrentShield) < shieldregen)
                {
                    CurrentShield = maxshield;
                }
                else 
                {
                    CurrentShield += shieldregen;
                }
            }
            // Read comment above, its essentially the same but for health. Just replace "shield" with "health".
            if (CurrentHealth != maxhealth) 
            {
                if ((maxhealth - CurrentHealth) < healthregen)
                {
                    CurrentHealth = maxhealth;
                }
                else 
                {
                    CurrentHealth += maxhealth;
                }
            }
        }

        public SpaceCraft() 
        {
            CurrentHealth = 150;
            healthregen = 10;
            maxhealth = 150;
            currentShield = 100;
            shieldregen = 20;
            maxshield = 100;
        }
    }
}
