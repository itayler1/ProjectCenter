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

namespace gameCenter.Projects
{
    /// <summary>
    /// Interaction logic for projectPresentationPage.xaml
    /// </summary>
    public partial class projectPresentationPage : Window
    {

        private Window currentProject;
        public projectPresentationPage()
        {
            InitializeComponent();
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSoursce, Window project, bool usesAPI = false, bool usesFileIO = false)
        {
            TitleLabel.Content = title;
            ProjectText.Text = projectDescription;
            ProjectImage.Source = imageSoursce;
            currentProject = project;
            apiICon.Visibility = usesAPI ? Visibility.Visible : Visibility.Collapsed;
            fileIOicon.Visibility = usesFileIO ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ProjectImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
            currentProject!.ShowDialog();
            currentProject!.Close();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 0.7;
        }


        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
        }

    }
}
