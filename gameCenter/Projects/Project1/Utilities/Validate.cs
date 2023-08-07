using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace gameCenter.Projects.Project1.Utilities
{
    internal class Validate
    {
        public static bool Email(TextBox textBox)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(textBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Please enter valid email!!!");
                textBox.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return false;
            }
            textBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            return true;
        }
        public static bool UserName(TextBox textBox)
        {
            Regex regex = new(@"^[A-Za-z].{2,15}$");
            Match match = regex.Match(textBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Please enter valid UserName!!!");
                textBox.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return false;
            }
            textBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            return true;
        }
    }
}
