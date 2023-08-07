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
using System.Windows.Shapes;
using WpfApp1.Models;

namespace gameCenter.Projects.Project1
{
    /// <summary>
    /// Interaction logic for Project1.xaml
    /// </summary>
    public partial class Project1 : Window
    {
        List<User> Users { get; set; }
        public Project1()
        {
            Users = new()  {
                new User()
                {
                    Id = 1,
                    Name = "Joe",
                    Email = "joe@email.com"
                },
                new User()
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@email.com"
                },
                new User()
                {
                    Id = 3,
                    Name = "Gil",
                    Email = "gil@email.com"
                },
                new User()
                {
                    Id = 4,
                    Name = "Shosh",
                    Email = "shosh@email.com"
                }
        };

            InitializeComponent();
            usersList.ItemsSource = Users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Users.Add(new User()
            {
                Name = userName.Text,
                Email = userEmail.Text,
                Id = Users.Count + 1
            });

            usersList.ItemsSource = Users.ToList();
            ClearFields();
        }

        private void ClearFields()
        {
            userEmail.Text = string.Empty;
            userName.Text = string.Empty;
        }
    }
}
