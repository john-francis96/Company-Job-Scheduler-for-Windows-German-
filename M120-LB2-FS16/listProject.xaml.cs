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

namespace M120_LB2_FS16
{
    /// <summary>
    /// Interaction logic for listProject.xaml
    /// </summary>
    public partial class listProject : Page
    {
        public listProject()
        {
            InitializeComponent();
            List<Projekt> projList = Bibliothek.Projekt_Alle();
            for (var i = 0; i < projList.Count(); i++)
            {
                ListBoxItem proj = new ListBoxItem();
                proj.Tag = projList[i].ID;
                proj.Content = projList[i].Name;
                proj.Background = Brushes.White;
                proj.MouseDoubleClick += this.lsProj_Handler;
                this.projListBox.Items.Add(proj);
            }
        }

        public void lsProj_Handler(object sender, RoutedEventArgs e)
        {
            ListBoxItem myButton = (ListBoxItem)sender;

            Application.Current.Properties["proj_edit_id"] = myButton.Tag.ToString();
            NavigationService.Navigate(new Uri("newProject.xaml", UriKind.Relative));

        }
    }
}
