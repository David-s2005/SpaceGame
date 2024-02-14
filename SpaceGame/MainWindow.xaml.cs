using SpaceGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        SpaceCraft Player = new SpaceCraft();

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
            0, // Shield regen modifier
            true, // unlocked

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

        Module modulelectromagneticreactivearmor = new Module
        (
            "Electromagnetic Reactive Armor",

            "When this armor system was developed for the ship on earth it was a relatively sophisticated piece" +
            " of technolgy by Earth standards. It utilizes the ships power generation to charge the armor with" +
            " a powerful electromagnetic field so when a projectile is about to strike the ship, the charge" +
            " will discharge into the projectile and destroy it.",
            0,
            2147483647,
            0,
            0,
            0,
            0,
            true,

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

        Module modulehighbetafusionreactor = new Module
        (
            "High Beta Fusion Reactor",
            
            "A compact and efficient source of energy. Capable of running early Nuemann probes.",
            0,
            100,
            0,
            0,
            0,
            0,
            true,

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

        Module modulebasicrepairsystem = new Module
        (
            "Basic Repair System",
            
            " a system of sensors, computers and autonomous systems that detect and repair" +
            " faults within the ship. Relies on the reactor for power.",
            0,
            150,
            0, 
            0,
            0,
            0,
            true,

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
            0,
            200,
            0,
            0,
            0,
            0,
            true,

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

        #endregion

        #region Anomalies

        #endregion

        private void damageEvent(int _damage) 
        {
            Player.Health -= _damage;

            if (Player.Health <= 0) 
            {
                // Player dies here.
            } 

            LabelHealth.Content = Player.Health.ToString();
            LabelShield.Content = Player.Shield.ToString();

            if (Player.Health >= 90)
            {
                LabelHealth.Foreground = Brushes.Green;
            }
            else if (Player.Health >= 70)
            {
                LabelHealth.Foreground = Brushes.LightGreen;
            }
            else if (Player.Health >= 45)
            {
                LabelHealth.Foreground = Brushes.Yellow;
            }
            else 
            {
                LabelHealth.Foreground = Brushes.Red;
            }


            if (Player.Shield >= 90) 
            {
                LabelShield.Foreground = Brushes.Green;
            }
            else if (Player.Shield >= 70)
            {
                LabelShield.Foreground = Brushes.LightGreen;
            }
            else if (Player.Shield >= 45)
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
            0.4, // weight
            15, // habitability
            new Dictionary<Resource, int> // resource dictionary
            {
                {Iron, 4},
                {Copper, 1},
                {Platinum, 1},
                {Uranium, 0},
                {Silicon, 2}
            },
            0.15, // anomaly probability

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
            0.4,

            "This thing is just a fat black dot on my sensors. It is seems that it is constantly hailing hydrogen, so very hostile for" +
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
            0.55,

            "",

            "",

            ""
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
            0.65,

            "",

            "",

            ""
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
            0.50,

            "",

            "",

            ""
        );

        static Planet EarthDuplicate = new Planet
        (
            "Earth Duplicate",
            50000000000,
            0.1,
            75,
            new Dictionary<Resource, int>
            {
                {Iron, 5},
                {Copper, 3},
                {Platinum, 1},
                {Uranium, 1},
                {Silicon, 4}
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
            String uniAgeString = LabelUniAge.Content.ToString();
            ComboBoxStar.Items.Clear();
            ComboBoxPlanet.Items.Clear();

            if (ulong.TryParse(uniAgeString, out ulong convertedAge) == true)
            {
                universe.ageUniverse(convertedAge);
            }
            else 
            {
                throw new Exception();
            }
            LabelUniAge.Content = universe.universeage; 

            Star star = new Star(planetList);
            int totalSystemPlanets = random.Next(1, 7);
            HotParadise.generatePlanetNames(totalSystemPlanets);

            foreach (string planetName in HotParadise.usedNames) 
            {
                ComboBoxPlanet.Items.Add(planetName);
            }
            
        }
    }
}
