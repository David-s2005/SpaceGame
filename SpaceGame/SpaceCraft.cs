using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class SpaceCraft
    {
        Random random = new Random();

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

        public int ironQuantity;
        public int copperQuantity;
        public int platinumQuantity;
        public int uraniumQuantity;
        public int siliconQuantity;

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

        private int aiSanity;

        public int AISanity
        {
            get
            {
                return aiSanity;
            }
            set
            {
                aiSanity = value;
            }
        }

        private int maxSanity;

        public int MaxSanity 
        {
            get 
            {
                return maxSanity;
            }
            set 
            {
                maxSanity = value;
            }
        }

        private int sanityRegenAmount;

        public int SanityRegenAmount 
        {
            get 
            {
                return sanityRegenAmount;
            }
            set 
            {
                sanityRegenAmount = value;
            }
        }

        public List<Module> ScannerList = new List<Module>();
        public List<Module> ReactorList = new List<Module>();
        public List<Module> WeaponList = new List<Module>();
        public List<Module> ShieldList = new List<Module>();
        public List<Module> RepairSystemList = new List<Module>();
        public List<Module> StructureList = new List<Module>();
        public List<Module> AICoresList = new List<Module>();
        public List<Module> CAPList = new List<Module>();

        public void InflictDamage(int _damage) 
        {
            if (CurrentShield <= _damage) // This occurs when the shield is knocked out.
            {
                int healthBeforeDamage = CurrentHealth;
                CurrentHealth -= (_damage + CurrentShield);
                CurrentShield = 0;
                int damageInflictedResult = healthBeforeDamage - currentHealth; // This is the total damage the hull incurs after its calculated. (DOESNT INCLUDE THE SHIELD!)

                if (damageInflictedResult > 15)
                {
                    aiSanity -= random.Next(5, 10);
                }
                else if (damageInflictedResult > 20)
                {
                    aiSanity -= random.Next(10, 15);                // Sanity will regenerate when the player flags habitable planets. The greater the habitability the greater the sanity.
                }
                else if (damageInflictedResult > 30)
                {
                    aiSanity -= random.Next(15, 22);
                }
            }
            else // occurs when shield remains
            {
                CurrentShield -= _damage;
            }
        }

        public void regenerateSanity(int _planetHabitability)  // e.g. 80 / 100 = 0.8 -> AISanity += regenAmount * 0.8 <- ROUND!!!!  <------------------
        {                                                      // This function will regenerate sanity according to the current planets habitability.  |
            int regenTotal = (int)Math.Round(((double)_planetHabitability / 100) * sanityRegenAmount); //----------------------------------------------|

            if (aiSanity + regenTotal > maxSanity)
            {
                aiSanity = maxSanity;
            }
            else 
            {
                aiSanity += regenTotal;
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
            aiSanity = 100;
        }
    }
}
