using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            LabelShield.Foreground = Brushes.LightGreen;
            LabelHealth.Foreground = Brushes.LightGreen;
            LabelAI.Foreground = Brushes.LightGreen;

            LabelShield.Content = "100";
            LabelHealth.Content = "100";
            LabelAI.Content = "100";

            imageExploration.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/TEMP/placeholder.png", UriKind.RelativeOrAbsolute));
            imageShip.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/TEMP/placeholder.png", UriKind.RelativeOrAbsolute));
            imageShipShield.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/TEMP/placeholder.png", UriKind.RelativeOrAbsolute));
            imageShipHealth.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/TEMP/placeholder.png", UriKind.RelativeOrAbsolute));
            imageShipAI.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/TEMP/placeholder.png", UriKind.RelativeOrAbsolute));

            imageIron.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/Planets/Resources/iron.png", UriKind.RelativeOrAbsolute));
            imageCopper.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/Planets/Resources/copper.png", UriKind.RelativeOrAbsolute));
            imagePlatinum.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/Planets/Resources/platinum.png", UriKind.RelativeOrAbsolute));
            imageUranium.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/Planets/Resources/uranium.png", UriKind.RelativeOrAbsolute));
            imageSilicon.Source = new BitmapImage(new Uri("C:/Users/David/projects/C#/SpaceGame/SpaceGame/Images/System/Planets/Resources/silicon.png", UriKind.RelativeOrAbsolute));
        }
    }
}
