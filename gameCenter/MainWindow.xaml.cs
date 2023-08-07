using gameCenter.Projects.Project1;
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

namespace gameCenter
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

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 0.7;
            if ((sender as Image)!.Name == "Image1")
            {
                GameText.Content = "Game No. 1 is a game about lorm ipsum & happy birthday";
            }
            if ((sender as Image)!.Name == "Image2")
            {
                GameText.Content = "Game No. 2 is a game about lorm ipsum & happy birthday";
            }
            if ((sender as Image)!.Name == "Image3")
            {
                GameText.Content = "Game No. 3 is a game about lorm ipsum & happy birthday";
            }
            if ((sender as Image)!.Name == "Image4")
            {
                GameText.Content = "Game No. 4 is a game about lorm ipsum & happy birthday";
            }
            if ((sender as Image)!.Name == "Image5")
            {
                GameText.Content = "Game No. 5 is a game about lorm ipsum & happy birthday";
            }
            if ((sender as Image)!.Name == "Image6")
            {
                GameText.Content = "Game No. 6 is a game about lorm ipsum & happy birthday";
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";
        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Project1 project1 = new();
            project1.ShowDialog();
        }
    }
}
