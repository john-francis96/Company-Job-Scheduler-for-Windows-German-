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
    /// Interaction logic for newEmployee.xaml
    /// </summary>
    public partial class newEmployee : Page
    {

        public newEmployee()
        {
            InitializeComponent();

            // Edit Employee
            if (Application.Current.Properties["mit_edit_id"] != null)
            {
                int mit_id;
                var bool5 = Int32.TryParse(Application.Current.Properties["mit_edit_id"].ToString(), out mit_id);
                Mitarbeiter mit = Bibliothek.Mitarbeiter_nach_ID(mit_id);
                this.mitName.Text = mit.Name;
                this.mitVorname.Text = mit.Vorname;
                this.mitAktiv.IsChecked = mit.IstAktiv;
                this.mitSubmit.Tag = "edit";

                // Create hidden button to save employee id
                this.btnID.Tag = mit_id.ToString();

                // Edit window opened and loaded.. reset trigger property
                Application.Current.Properties["mit_edit_id"] = null;
            }
        }



        private void btnMit_Handler(object sender, RoutedEventArgs e)
        {

            Button myButton = (Button)sender;

            // Create new Mitarbeiter
            if (myButton.Tag.ToString() == "new")
            {
                Mitarbeiter newMitarbeiter = new Mitarbeiter();
                int id;
                var bool4 = Int32.TryParse(Application.Current.Properties["mit_id"].ToString(), out id); // saved max id to increment
                newMitarbeiter.ID = id += 1;
                Application.Current.Properties["mit_id"] = (id += 1).ToString();
                newMitarbeiter.Name = this.mitName.Text;
                newMitarbeiter.Vorname = this.mitVorname.Text;
                newMitarbeiter.IstAktiv = this.mitAktiv.IsChecked.Value;
                Bibliothek.Mitarbeiter_Neu(newMitarbeiter);
            }

            // Edit Mitarbeiter
            if (myButton.Tag.ToString() == "edit")
            {
                int id;
                var bool6 = Int32.TryParse(this.btnID.Tag.ToString(), out id);
                Mitarbeiter mit = Bibliothek.Mitarbeiter_nach_ID(id);
                mit.Name = this.mitName.Text;
                mit.Vorname = this.mitVorname.Text;
                mit.IstAktiv = this.mitAktiv.IsChecked.Value;
                this.btnID.Tag = ""; // reset edit employee id holder
            }

        }
    }
}
