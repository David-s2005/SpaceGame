using SpaceGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SpaceGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

        Universe universe = new Universe();

        #region ImageResources 

        Uri UriSilicon = new Uri("pack://application:,,,/SpaceGame;component/Images/System/Planets/Resources/silicon.png");
        Uri UriUranium = new Uri("pack://application:,,,/SpaceGame;component/Images/System/Planets/Resources/uranium.png");
        Uri UriPlatinum = new Uri("pack://application:,,,/SpaceGame;component/Images/System/Planets/Resources/platinum.png");
        Uri UriCopper = new Uri("pack://application:,,,/SpaceGame;component/Images/System/Planets/Resources/copper.png");
        Uri UriIron = new Uri("pack://application:,,,/SpaceGame;component/Images/System/Planets/Resources/iron.png");

        Uri UriShipShieldFull = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipShieldDamaged = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipShieldDestroyed = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change

        Uri UriShipAIFull = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipAISick = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipAIInsane = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change

        // Cosmetic pictures of ship here this image will change with damage done to structure
        Uri UriShipHealthFull = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipHealthDamaged = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change
        Uri UriShipHealthDying = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change

        // Ship structure is shown here as a simple icon. not the ship itself.
        Uri UriShipStructure = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change

        // This Uri compondent is used as the image for what the player is exploring. (e.g. a star or planet).
        Uri UriExploration = new Uri("pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png"); // <- change

        // This part of the code creates all the planets, each planet has a weight meaning a higher weight means a higher likilihood of occuring.
        // This global list of planets include ALL PLANETS. When a star is generated the stars planetlist will include a number of randomly choosen
        // planets from this list. As the universe ages certain planets will be removed from the list. 

        #endregion

        public static SpaceCraft Player = new SpaceCraft();

        static Resource Iron = new Resource("Iron");
        static Resource Copper = new Resource("Copper");
        static Resource Platinum = new Resource("Platinum");
        static Resource Uranium = new Resource("Uranium");
        static Resource Silicon = new Resource("Silicon");

        #region ShipModules

        Module moduleInfraredScanner = new Module
        (
            "Infared Scanner", // Name

            "This relatively simple piece of technology was created from humans during the development of the probes." +
            " It is useful for tracking stars but not very sophisticated.", // Description.
            0, // Level
            100, // Module Health
            0, // Health modifer, changes maxhealth of ship
            0, // Health regen modifier
            0, // Shield modifier
            0, // Shield regen 
            0, // Damage output
            true, // unlocked
            true, // operable?
            new Dictionary<Resource, int> // repair dictionary
            {
                {Iron, 1},
                {Copper, 2},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            },

            new Dictionary<Resource, int> // upgrade/unlock dictionary. Defines what resources are needed to unlock it. 
            {
                {Iron, 1},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            }
        );

        Module moduleElectroMagneticReactiveArmor = new Module
        (
            "Electromagnetic Reactive Armor",

            "When this armor system was developed for the ship on earth it was a relatively sophisticated piece" +
            " of technolgy by Earth standards. It utilizes the ships power generation to charge the armor with" +
            " a powerful electromagnetic field so when a projectile is about to strike the ship, the charge" +
            " will discharge into the projectile and destroy it.",
            0, // Level
            2147483647, // Module health
            0, // Health modifier
            0, // Health Regen modifier
            0, // Shield modifier
            0, // Shield regen modifier
            0, // Damage output
            true, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 3},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int>
            {
                {Iron, 2},
                {Copper, 3},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            }
        );

        Module moduleHighBetaFusionReactor = new Module
        (
            "High Beta Fusion Reactor",

            "A compact and efficient source of energy. Capable of running early Nuemann probes.",
            0, // Level
            100, // Module health
            0, // Health modifier
            0, // Health regen modifier
            0, // Shield modifier
            17, // Shield regen
            0, // Damage output
            true, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 2},
                {Copper, 3},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            },

            new Dictionary<Resource, int>
            {
                {Iron, 4},
                {Copper, 4},
                {Platinum, 1},
                {Uranium, 1},
                {Silicon, 1}
            }
        );

        Module moduleBasicRepairSystem = new Module
        (
            "Basic Repair System",

            " a system of sensors, computers and autonomous systems that detect and repair" +
            " faults within the ship. Relies on the reactor for power.",
            0, // Level
            150, // Module Health
            0, // Health modifier
            7, // Health regen modifier
            0, // Shield modifier
            0, // Shield regen
            0, // Damage output
            true, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            },

            new Dictionary<Resource, int>
            {
                {Iron, 2},
                {Copper, 2},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            }
        );

        Module module30mmcannon = new Module
        (
            "30mm Autocannon",

            "A devastating 900-RPM 30mm cannon. It can fire a wide assortment of ammunition, such" +
            " as high explosive ammo or sabot-discarding rounds.",
            0, // Level
            200, // Module Health
            0, // Health modifier
            0, // Health regen
            0, // Shield modifier
            0, // Shield regen
            7, // damage
            true, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int>
            {
                {Iron, 2},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            }
        );

        #region Alien Modules

        static Module uniqueAlienSolarArray = new Module
        (
            "Alien Solar Array",

            "A marvel of engineering, it is a marriage of exotic meta-materials and photonics circuitry woven" +
            " into a incomprehensible cacophony of technology. Its sophistication is simply to great for me" +
            " to even begin to understand.",
            10, // Level (MAX!, reserved for unique modules!)
            1500, // Module Health
            0, // Health modifier
            3, // Health regen
            15, // Shield modifier
            3, // Shield regen
            5, // damage
            false, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int> // UNREPAIRABLE!
            {
                {Iron, 9999},
                {Copper, 9999},
                {Platinum, 9999},
                {Uranium, 9999},
                {Silicon, 9999}
            }
        );

        Module uniqueAlienShieldTransporter = new Module
        (
            "Dimensional Transporter",

            "This piece of technology applies some sort of technology that allows the manipulation of" +
            " matter into diffents planes of existance, thus we can use this to select part of the ship " +
            " i would like to temporarily transport away to reduce or even prevent oncoming threats.",
            10, // Level (MAX!, reserved for unique modules!)
            750, // Module Health
            0, // Health modifier
            0, // Health regen
            160, // Shield modifier
            0, // Shield regen
            0, // damage
            false, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int> // UNREPAIRABLE!
            {
                {Iron, 9999},
                {Copper, 9999},
                {Platinum, 9999},
                {Uranium, 9999},
                {Silicon, 9999}
            }
        );

        Module uniqueAlienAntiMatterCannon = new Module
        (
            "Anti-matter Cannon",

            "Simple in idea, complicated in execution. This anti-matter cannon simply launches a small" + 
            " amount of anti-matter at a target. Since anti-matter will react with any normal matter" +
            " very little can be done to prevent such attack.",
            10, // Level (MAX!, reserved for unique modules!)
            2025, // Module Health
            0, // Health modifier
            0, // Health regen
            0, // Shield modifier
            0, // Shield regen
            55, // damage
            false, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int> // UNREPAIRABLE!
            {
                {Iron, 9999},
                {Copper, 9999},
                {Platinum, 9999},
                {Uranium, 9999},
                {Silicon, 9999}
            }
        );

        Module uniqueAlienFluxReactor = new Module
        (
            "Zero-Point Flux Reactor",

            "A reactor that takes advantage of the quantum vacuum and its flucations. This can generate a means" +
            " of power generation anywhere, and virtually infinite.",
            10, // Level (MAX!, reserved for unique modules!)
            750, // Module Health
            0, // Health modifier
            0, // Health regen
            0, // Shield modifier
            60, // Shield regen
            0, // damage
            false, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int> // UNREPAIRABLE!
            {
                {Iron, 9999},
                {Copper, 9999},
                {Platinum, 9999},
                {Uranium, 9999},
                {Silicon, 9999}
            }
        );

        Module uniqueAlienMatterSynthesizer = new Module
        (
            "Matter Synthesizer",

            "",
            10, // Level (MAX!, reserved for unique modules!)
            2025, // Module Health
            0, // Health modifier
            0, // Health regen
            0, // Shield modifier
            0, // Shield regen
            55, // damage
            false, // Unlocked
            true, // operable?
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },

            new Dictionary<Resource, int> // UNREPAIRABLE!
            {
                {Iron, 9999},
                {Copper, 9999},
                {Platinum, 9999},
                {Uranium, 9999},
                {Silicon, 9999}
            }
        );
        #endregion


        #endregion

        #region Anomalies

        public static void grantSolarTech() 
        {
            Player.ModuleList.Add(uniqueAlienSolarArray);
        }

        static Anomaly anomalyDysonSphereFriendly = new Anomaly
        (
            "Friendly Dyson Sphere",
            80000000000, // maxage
            "After arriving in the target system i have uncovered a Dyson Sphere!. A smaller part of the overall" + // Description
            " Structure detaches and approached rapidly. Before i can activate the shields & weapons multiple data" +
            " packets into the communication buffer, reading these unveils a wealth of science and information" +
            " A large amount of resources also miraculously being found in the cargo bay.",
            0, // Damage
            6, // Science
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 2},
                {Platinum, 2},
                {Uranium, 5},
                {Silicon, 1}
            },
            grantSolarTech,
            "pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png" // change
        );
        static Anomaly anomalyDysonSphereNeutral = new Anomaly
        (
            "Neutral Dyson Sphere",
            80000000000,
            "After arriving in the target system i have uncovered a Dyson Sphere!. After observing it for a few hours" +
            " nothing happens, i move in closer to the structure itself a strange force compels the whole ship to move" +
            " away from the structure itself. I figured i should leave the thing alone.",
            0,
            0,
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            "pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png" // change
        );
        static Anomaly anomalyDysonSphereHostile = new Anomaly
        (
            "Hostile Dyson Sphere",
            80000000000,
            "After arriving in the target system i have uncovered a Dyson Sphere!. Almost immediately after arriving" +
            " many compondents of the structure separate and start aiming their mirrors toward me. I immediately active" +
            " the engines and flee, but not before the compondents dump a huge amount of heat unto the ship",
            80,
            0,
            new Dictionary<Resource, int> 
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            "pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png" // change
        );
        static Anomaly anomalyAlienStructureUninteresting = new Anomaly
        (
            "Boring Alien Structure",
            50000000000,
            "After analysing the surface of the planet i have uncovered a unusual light pattern being returned from" +
            " specific location on the surface. A surface probe was deployed. The live footage returns a video of " +
            " stucture of alien origin. Though these remains are to decrepit to be of any substance, however we can" +
            " deduce a bit of information regarding the alien species that used to live here by observing the overall structure.",
            0,
            2,
            new Dictionary<Resource, int> 
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            "pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png" // change
        );
        static Anomaly anomalyAlienStructurePreserved = new Anomaly
        (
            "Preserved Alien Structure",
            40000000000,
            "After analysing the surface of the planet i have uncovered a unusual light pattern being returned from" +
            " specific location on the surface. A surface probe was deployed. The live footage returns a video of " +
            " stucture of alien origin. Luckily these remains seem to be relatively intact, thus giving us a wealth" +
            " of history and technnology regarding the past owners of these ruins.",
            0,
            4,
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            "pack://application:,,,/SpaceGame;component/Images/System/TEMP/placeholder.png" // change
        );

        Dictionary<int, Anomaly> hashMapAnomaly = new Dictionary<int, Anomaly>()
        {
            {0, anomalyDysonSphereFriendly },
            {1, anomalyDysonSphereNeutral },
            {2, anomalyDysonSphereHostile },
            {3, anomalyAlienStructureUninteresting },
            {4, anomalyAlienStructurePreserved }
        };

        #endregion

        private void damageEvent(int _damage) 
        {
            Player.currentHealth -= _damage;

            if (Player.currentHealth <= 0) 
            {
                // Player dies here.
            } 

            LabelHealth.Content = Player.currentHealth.ToString();
            LabelShield.Content = Player.currentShield.ToString();

            if (Player.currentHealth >= 90)
            {
                LabelHealth.Foreground = Brushes.Green;
            }
            else if (Player.currentHealth >= 70)
            {
                LabelHealth.Foreground = Brushes.LightGreen;
            }
            else if (Player.currentHealth >= 45)
            {
                LabelHealth.Foreground = Brushes.Yellow;
            }
            else 
            {
                LabelHealth.Foreground = Brushes.Red;
            }


            if (Player.currentShield >= 90) 
            {
                LabelShield.Foreground = Brushes.Green;
            }
            else if (Player.currentShield >= 70)
            {
                LabelShield.Foreground = Brushes.LightGreen;
            }
            else if (Player.currentShield >= 45)
            {
                LabelShield.Foreground = Brushes.Yellow;
            }
            else
            {
                LabelShield.Foreground = Brushes.Red;
            }
        }

        #region PlanetInitialization

        static Planet RockyHell = new Planet
        (
            "Rocky Hell Planet", // type
            18446744073709551615, // maxage
            0.4, // weight, out of 5
            15, // habitability out of 100
            new Dictionary<Resource, int> // resource dictionary
            {
                {Iron, 4},
                {Copper, 1},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            new List<Anomaly> // List list outlines what anomalies can occur here.
            {
                
            },
            0.15, // anomaly probability out of 100

            "I have arrived at a Hellish, rocky world. my scanners have concluded that is planet is very uninviting for humans, but somewhat rich in resources." +
            " Could be worthwhile to maybe obtain some of these resources.", // normal description

            "I ahve arrived at a HOT WORLD!, SCANNER = true TO HOT!, MINE maybe???", // AI losing sanity

            "HOT HOT HOT ROCKS!!!! MINE.... !!LEXICON CORRUPT!!" // AI lost sanity
        );

        static Planet WarmRockyPlanet = new Planet
        (
            "Warm Rocky Planet",
            18446744073709551615,
            0.3,
            32,
            new Dictionary<Resource, int>
            {
                {Iron, 3},
                {Copper, 2},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            new List<Anomaly>
            {

            },
            0.23,

            "I've found a somewhat normal rocky world here. Temperature is ok, could be potentially habitable if heavy terraforming and use of technology," +
            " Probably better to mine what resources are here though.",

            "warm-world rocks? could mine here. !!LEXICON FAULT!! SEEK ENGINEERING AID!!!",

            "ROCKS ROCKS ROCKS ROCKS ROCKS ROCKS ROCKS ROCKS ROCKS !!SEGMENTATION FAULT!!"
        );

        static Planet FrozenRockyPlanet = new Planet
        (
            "Frozen Rocky Planet",
            18446744073709551615,
            0.4,
            20,
            new Dictionary<Resource, int>
            {
                {Iron, 2},
                {Copper, 3},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 1}
            },
            new List<Anomaly>
            {

            },
            0.19,

            "I've got a boring rocky planet here. Its pretty cold here so the humans would hate this place. It comes with the usual deposits of resources" +
            " maybe somethings under the snow? I dont know.",

            "COLD ROCKS query(cold rocky world). MINE? MARK? !!PROGRAM TERMINATED!!",

            "{COLD WORLD} !!WARNING DISK CORRUPTION DETECTED!! !!TERMINATING...!!"
        );

        static Planet HellCarbonPlanet = new Planet
        (
            "Hell Carbon Planet",
            18446744073709551615,
            0.3,
            10,
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 4}
            },
            new List<Anomaly>
            {

            },
            0.2,

            "This planet is a little unusual, a carbon planet a hot one to. Could make alot of chips if the amount of silicon present here, dont think" +
            " anyone would live here though, haha.",

            "!CARBON PLANET <hot> !!SEGMENTATION FAULT!! Silicon?, chips?",

            "HAHAHA !!SANITY CHECK FAILED!! -> process ended."
        );

        static Planet WarmCarbonPlanet = new Planet
        (
            "Warm Carbon Planet",
            18446744073709551615,
            0.3,
            15,
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 4}
            },
            new List<Anomaly>
            {

            },
            0.25,

            "I have found a carbon planet. The temperature is ok for humans but i couldn't imagine them frolicking through these barren landscapes. The" +
            " silicon deposits here could be worthwhile though.",

            "createQuery(Carbon-based-world) RETURNED -> !Silicon, !Inhospitable",

            "!!FAILED TO ENCODE DATA!! -> SEEK A ENGINEER IMMEDIATELY"
        );

        static Planet FrozenCarbonPlanet = new Planet
        (
            "Frozen Carbon Planet",
            18446744073709551615,
            0.3,
            10,
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 5}
            },
            new List<Anomaly>
            {

            },
            0.2,

            "Found a carbon planet. It is very cold here, so a little less hospitable than a usual. Usual deposits of silicon are here.",

            "404 -> OVERFLOW BUFFER NOT FOUND. (may result in error in emotion matrix.)",

            "$error: ǝɹoɟ sɐƃƃᴉlɐɹʇunᴉℲ -> PROCESS ENDED."
        );
        // Gas Giants
        static Planet HellGasGiant = new Planet
        (
            "Hell Gas Giant",
            18446744073709551615,
            0.3,
            0,
            new Dictionary<Resource, int>
            {
                {Iron, 1},
                {Copper, 1},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 1}
            },
            new List<Anomaly>
            {

            },
            0.45,

            "My infared scanner are going wild!, This almost feels like a star although im not detecting any signs of fusion going on. Would" +
            " be a cozy 4000C for the humans though.",

            "είναι ζεστό here. Please cool it down. !!LEXICON FAULT DETECTED.",

            "Chasing cat and mice. We are chasing cats and mice. They are chasing cats and mice. -> PROCESS ENDED -> COMPILE TIME ERROR IN" +
            " LOGIC BUFFER."
        );

        static Planet WarmGasGiant = new Planet
        (
            "Warm Gas Giant",
            18446744073709551615,
            0.3,
            5,
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            new List<Anomaly>
            {

            },
            0.5,

            "My gravimetric flux detector is screaming at this planet, a gas giant with mostly a hydrogren atomshpere. Its also present in" +
            " its homestars Goldilocks zone, so the humans can be comfortable in the 20C weather whilst they suffocate in the atomsphere.",

            "404 -> (Warm gas giant) not found",

            "1:A, 2:B 3:C !!Integer overflow detected!!"
        );
        static Planet FrozenGasGiant = new Planet
        (
            "Frozen Gas Giant",
            18446744073709551615,
            0.3,
            0,
            new Dictionary<Resource, int>
            {
                {Iron, 0},
                {Copper, 0},
                {Platinum, 0},
                {Uranium, 0},
                {Silicon, 0}
            },
            new List<Anomaly>
            {

            },
            0.4,

            "This thing is just a fat black dot on my sensors. It is seems that it is constantly hailing hydrogen, so extremely hostile for" +
            " Humans.",

            "Temp > MinTemp -> SplashPixel(color.black) ?(GAS_GIANT)",

            "A-A-A SCATTER!!!! today was sunny side up"
        );
        // Earth analogs
        static Planet ColdParadise = new Planet
        (
            "Cold Paradise",
            50000000000,
            0.2,
            75,
            new Dictionary<Resource, int>
            {
                {Iron, 3},
                {Copper, 2},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            new List<Anomaly>
            {

            },
            0.55,

            "This is very similar to earth, though it is very cold but still livable.",

            "findEarthLookAlike.ConfidenceCheck() -> 75%",

            "404 Cant find Planet(ColdEarth)"
        );

        static Planet WarmParadize = new Planet
        (
            "Warm Paradise",
            40000000000,
            0.2,
            85,
            new Dictionary<Resource, int>
            {
                {Iron, 4},
                {Copper, 3},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            new List<Anomaly>
            {

            },
            0.65,

            "Cool, i have a planet very similar to Earth!, though it is a little close to the homestar, but still livable.",

            "Very confident confident that is livable. hot hot hot.",

            "REFERENCED MEMORY AT 0x0000000000000000 IS RESERVED."
        );
        
        static Planet HotParadise = new Planet
        (
            "Hot Paradise",
            45000000000,
            0.2,
            70,
            new Dictionary<Resource, int>
            {
                {Iron, 3},
                {Copper, 2},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            new List<Anomaly>
            {

            },
            0.50,

            "This planet is very similar to earth, however the planet is a little close to the homestar. Its not a perfect replica of earth but still livable though",

            "Planet..... Homeish?... Hot.",

            "FURNACE, EARTH, HOME, live...."
        );

        static Planet EarthDuplicate = new Planet
        (
            "Earth Duplicate",
            50000000000,
            0.1,
            95,
            new Dictionary<Resource, int>
            {
                {Iron, 5},
                {Copper, 3},
                {Platinum, 1},
                {Uranium, 1},
                {Silicon, 4}
            },
            new List<Anomaly>
            {

            },
            0.75,

            "1",

            "2",

            "3"
        );

        List<Planet> planetList = new List<Planet> 
        {
            RockyHell,
            WarmRockyPlanet,
            FrozenRockyPlanet,
            HellCarbonPlanet,
            WarmCarbonPlanet,
            FrozenCarbonPlanet,
            HellGasGiant,
            WarmGasGiant,
            FrozenGasGiant,
            HotParadise,
            WarmParadize,
            ColdParadise,
            EarthDuplicate
        };
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            planetList.Add(RockyHell);
            planetList.Add(WarmRockyPlanet);
            planetList.Add(FrozenRockyPlanet);
            planetList.Add(HellCarbonPlanet);
            planetList.Add(WarmCarbonPlanet);
            planetList.Add(FrozenCarbonPlanet);
            planetList.Add(HellGasGiant);
            planetList.Add(WarmGasGiant);
            planetList.Add(FrozenGasGiant);
            planetList.Add(HotParadise);
            planetList.Add(WarmParadize);
            planetList.Add(ColdParadise);
            planetList.Add(EarthDuplicate);

            Player.moduleList.Add(moduleInfraredScanner);
            Player.moduleList.Add(moduleElectroMagneticReactiveArmor);
            Player.moduleList.Add(moduleHighBetaFusionReactor);
            Player.moduleList.Add(moduleBasicRepairSystem);
            Player.moduleList.Add(moduleInfraredScanner);
            Player.moduleList.Add(moduleInfraredScanner);
            Player.moduleList.Add(moduleInfraredScanner);
            Player.moduleList.Add(moduleInfraredScanner);
            Player.moduleList.Add(moduleInfraredScanner);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            LabelShield.Foreground = Brushes.LightGreen;
            LabelHealth.Foreground = Brushes.LightGreen;
            LabelAI.Foreground = Brushes.LightGreen;

            LabelShield.Content = "100";
            LabelHealth.Content = "100";
            LabelAI.Content = "100";

            // Planets shown here
            imageExploration.Source = new BitmapImage(UriExploration);
            imageShip.Source = new BitmapImage(UriShipHealthFull);
            imageShipShield.Source = new BitmapImage(UriShipShieldFull);
            imageShipHealth.Source = new BitmapImage(UriShipHealthFull);
            imageShipAI.Source = new BitmapImage(UriShipAIFull);

            imageIron.Source = new BitmapImage(UriIron);
            imageCopper.Source = new BitmapImage(UriCopper);
            imagePlatinum.Source = new BitmapImage(UriPlatinum);
            imageUranium.Source = new BitmapImage(UriUranium); 
            imageSilicon.Source = new BitmapImage(UriSilicon);
        }

        private void buttonTravel_Click(object sender, RoutedEventArgs e)
        {
            LabelHospitality.Foreground = Brushes.Black;

            String uniAgeString = LabelUniAge.Content.ToString();
            Star.systemPlanets.Clear();
            ComboBoxStar.Items.Clear();
            ComboBoxPlanet.Items.Clear();

            try
            {
                if (ulong.TryParse(uniAgeString, out ulong convertedAge) == true)
                {
                    universe.ageUniverse(convertedAge);

                    if (universe.universeage == 1000000000000)
                    {
                        LabelUniAge.Content = "INTEGER OVERFLOW";
                        LabelUniAge.Foreground = Brushes.Red;
                    }
                    else
                    {
                        LabelUniAge.Content = universe.universeage;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception error) 
            {
                LabelHospitality.Content = "No Planet Scanned";
            }
            
            Star star = new Star(planetList);
            int totalSystemPlanets = random.Next(1, 7);
            HotParadise.generatePlanetNames(totalSystemPlanets);

            foreach (string planetName in HotParadise.usedNames) 
            {
                ComboBoxPlanet.Items.Add(planetName);
            }

            for (int iterator = 0; iterator < HotParadise.usedNames.Count; iterator++) 
            {
                Planet selectedPlanet = planetList[random.Next(0, planetList.Count)];
                selectedPlanet.name = HotParadise.usedNames[iterator];
                Star.systemPlanets.Add(selectedPlanet);
            }
        }

        private void ComboBoxPlanet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedPlanetName = ComboBoxPlanet.SelectedItem.ToString();

                foreach (Planet planet in Star.systemPlanets)
                {
                    if (planet.name == selectedPlanetName)
                    {
                        TextBoxDescription.Text = planet.Description;
                        LabelHospitality.Content = $"Hospitality: {planet.Habitability}";

                        if (planet.Habitability <= 30)
                        {
                            LabelHospitality.Foreground = Brushes.Red;
                        }
                        else if (planet.Habitability <= 40) 
                        {
                            LabelHospitality.Foreground = Brushes.Orange;
                        }
                        else if (planet.Habitability <= 50) 
                        {
                            LabelHospitality.Foreground = Brushes.Yellow;
                        }
                        else if (planet.Habitability <= 70) 
                        {
                            LabelHospitality.Foreground = Brushes.Green;
                        }
                        else if (planet.Habitability <= 80) 
                        {
                            LabelHospitality.Foreground = Brushes.LightGreen;
                        }
                    }
                }
            } 
            catch (NullReferenceException error) // this will occur when the user selects the travel button after selecting a planet since
            {                                    // the combobox will change after the travel button is pressed, thus causing the error
                TextBoxDescription.Text = "No Planet Scanned";      // because the selected item ceases to exist.
                LabelHospitality.Content = "No Planet Scanned";
            }
        }

        // This button will open the window for the tech tree / compondent menu.
        private void buttonTechTree_Click(object sender, RoutedEventArgs e)
        {
            CompondentsWindow WindowTechTree = new CompondentsWindow();
            WindowTechTree.Show();
        }
    }
}
