﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceGame
{
    /// <summary>
    /// Interaction logic for CompondentsWindow.xaml
    /// </summary>
    public partial class CompondentsWindow : Window
    {
        public CompondentsWindow()
        {
            InitializeComponent();
        }

        static SpaceCraft player = MainWindow.Player;

        static Resource Iron;
        static Resource Copper;
        static Resource Platinum;
        static Resource Uranium;
        static Resource Silicon;

        static Dictionary<Resource, int> playerResources = new Dictionary<Resource, int>
        {
            {Iron, 0},
            {Copper, 0},
            {Platinum, 0},
            {Uranium, 0},
            {Silicon, 0}
        };
        List<Module> playerModules = player.moduleList;
    }
}
