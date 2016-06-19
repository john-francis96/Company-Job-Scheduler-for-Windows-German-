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
    /// Interaction logic for listEmployee.xaml
    /// </summary>
    public partial class listEmployee : Page
    {
        public listEmployee()
        {
            InitializeComponent();
            List<Mitarbeiter> mitList = Bibliothek.Mitarbeiter_Alle();
            for (var i = 0; i < mitList.Count(); i++)
            {
                ListBoxItem mit = new ListBoxItem();
                mit.Tag = mitList[i].ID;
                mit.Content = mitList[i].Name;
                mit.Background = Brushes.White;
                mit.MouseDoubleClick += this.lsEmployee_Handler;
                this.mitListBox.Items.Add(mit);
            }
        }

        public void lsEmployee_Handler(object sender, RoutedEventArgs e)
        {
            ListBoxItem myButton = (ListBoxItem)sender;

            Application.Current.Properties["mit_edit_id"] = myButton.Tag.ToString();
            NavigationService.Navigate(new Uri("newEmployee.xaml", UriKind.Relative));

        }
    }
}
