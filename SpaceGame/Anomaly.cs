﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Anomaly : CelestialObject
    {
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

        private List <Resource> anomalyResources = new List <Resource> ();

        private int weight;

        public int Weight 
        {
            get 
            {
                return weight;
            }
            set 
            {
                weight = value;
            }
        }
    }
}