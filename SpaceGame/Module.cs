using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpaceGame
{
    public class Module : CelestialObject
    {
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

        private int level;

        public int Level 
        {
            get 
            {
                return level;
            }
            set 
            {
                level = value;
            }
        }

        private int modulehealth;

        public int ModuleHealth
        {
            get
            {
                return modulehealth;
            }
            set
            {
                modulehealth = value;
            }
        }

        private int healthmodifier;

        public int HealthModifier
        {
            get
            {
                return healthmodifier;
            }
            set
            {
                healthmodifier = value;
            }
        }

        private int healthregenmodifier;

        public int HealthRegenModifier 
        {
            get 
            {
                return healthregenmodifier;
            }
            set 
            {
                healthregenmodifier = value;
            }
        }

        private int shieldmodifier;

        public int ShieldModifier 
        {
            get 
            {
                return shieldmodifier;
            }
            set 
            {
                shieldmodifier = value;
            }
        }

        private int shieldregenmodifier;

        public int ShieldRegenModifier 
        {
            get 
            {
                return shieldregenmodifier;
            }
            set 
            {
                shieldregenmodifier = value;
            }
        }

        private int DamageOutput;

        public int damageOutput
        {
            get 
            {
                return DamageOutput;
            }
            set 
            {
                DamageOutput = value;
            }
        }

        private bool unlocked;

        public bool Unlocked 
        {
            get 
            {
                return unlocked;
            }
            set 
            {
                unlocked = value;
            }
        }

        private bool isOperable;

        public bool IsOperable 
        {
            get 
            {
                return isOperable;
            }
            set 
            {
                isOperable = value;
            }
        }
            
        // implement resource list. This list is used to define what a module needs to be earnt.
        private Dictionary<Resource, int> upgraderesources = new Dictionary<Resource, int>();

        public Dictionary<Resource, int> Upgraderesources 
        {
            get 
            {
                return upgraderesources;
            }
            set 
            {
                upgraderesources = value;
            }
        }

        // implement resource list. This list is used to tell the program what is needed to fix the module.
        private Dictionary<Resource, int> repairresources = new Dictionary<Resource, int>();

        public Dictionary<Resource, int> RepairResources
        {
            get
            {
                return repairresources;
            }
            set
            {
                repairresources = value;
            }
        }

        public Module(string _name, string _description, int _level, int _modulehealth, int _healthmodifier, int _healthregenmodifier, int _shieldmodifier, int _shieldregenmodifier,
                      int _damageOutput, bool _unlocked, bool _operable, Dictionary<Resource, int> _repairDictionary, Dictionary<Resource, int> _upgradeResources)
        {
            name = _name;
            description = _description;
            level = _level;
            modulehealth = _modulehealth;
            healthmodifier = _healthmodifier;
            healthregenmodifier = _healthregenmodifier;
            shieldmodifier = _shieldmodifier;
            shieldregenmodifier = _shieldregenmodifier;
            DamageOutput = _damageOutput;
            unlocked = _unlocked;
            isOperable = _operable;
            repairresources = _repairDictionary;
            upgraderesources = _upgradeResources;
        }
        public void upgrade() 
        {
            
        }

        public void repair() 
        {
        
        }
    }
}
