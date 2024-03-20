using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media;

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

        private int scienceModifier;

        public int ScienceModifier
        {
            get 
            {
                return scienceModifier;
            }
            set 
            {
                scienceModifier = value;
            }
        }

        private int maxHealth;

        public int MaxHealth 
        {
            get 
            {
                return maxHealth;
            }
            set 
            {
                maxHealth = value;
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

        public int damageDeflect;

        private int DamageDeflect 
        {
            get
            {
                return damageDeflect;
            }
            set 
            {
                damageDeflect = value;
            }
        }

        public int maxSanityModifier;

        private int MaxDamageModifier 
        {
            get 
            {
                return maxSanityModifier;
            }
            set 
            {
                maxSanityModifier = value;
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

        

        public Module(string _name, string _type, string _description, int _level, int _scienceModifier, int _modulehealth, int _healthmodifier, int _healthregenmodifier, int _shieldmodifier, int _shieldregenmodifier,
                      int _damageOutput, int _damageDeflect, int _maxSanityModifier, bool _unlocked, bool _operable, Dictionary<Resource, int> _repairDictionary, Dictionary<Resource, int> _upgradeResources, SpaceCraft _player)
        {
            name = _name;
            type = _type;
            description = _description;
            level = _level;
            scienceModifier = _scienceModifier;
            maxHealth = _modulehealth;
            modulehealth = _modulehealth;
            healthmodifier = _healthmodifier;
            healthregenmodifier = _healthregenmodifier;
            shieldmodifier = _shieldmodifier;
            shieldregenmodifier = _shieldregenmodifier;
            damageDeflect = _damageDeflect;
            DamageOutput = _damageOutput;
            maxSanityModifier = _maxSanityModifier;
            unlocked = _unlocked;
            isOperable = _operable;
            repairresources = _repairDictionary;
            upgraderesources = _upgradeResources;

            Dictionary<string, List<Module>> moduleList = new Dictionary<string, List<Module>>
            {
                { "Scanner", _player.ScannerList},
                { "Reactor", _player.ReactorList},
                { "Weapon", _player.WeaponList},
                { "Shield", _player.ShieldList},
                { "Repair", _player.RepairSystemList},
                { "Structure", _player.StructureList},
                { "AICore", _player.AICoresList},
                { "CAP", _player.CAPList},
            };

            if (moduleList.ContainsKey(type)) // me likey thanks AI!
            {
                moduleList[type].Add(this);
            }
            else 
            {
                throw new Exception($"Module type wasnt found! type: {type}");
            }
        }
        public int upgrade(Dictionary<Resource, int> _playerResources) 
        {
            foreach (var resource in _playerResources) 
            {
                foreach (var upgradeResource in upgraderesources) 
                {
                    if (upgradeResource.Value > resource.Value) 
                    {
                        return -1; // player doesnt require the amount of resources required.
                    }
                }
            }
            unlocked = true;
            return 0; // use this to add the module to the player module list when returned. TURN OFF ALL OTHER MODULES IN THE SAME CATEGORY!
        }

        public int repair(Dictionary<Resource, int> _playerResources) 
        {
            if (isOperable)
            {
                return -2; // magic number. Use when the module is already operable.
            }
            foreach (var resource in _playerResources)
            {
                foreach (var repairResource in upgraderesources)
                {
                    if (repairResource.Value > resource.Value)
                    {
                        return -1; // player doesnt require the amount of resources required.
                    }
                }
            }
            modulehealth = maxHealth;
            isOperable = true;
            return 0; // module is fixed.
        }
    }
}
