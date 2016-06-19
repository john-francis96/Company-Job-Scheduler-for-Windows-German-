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
    /// Interaction logic for newProject.xaml
    /// </summary>
    public partial class newProject : Page
    {
        public newProject()
        {
            InitializeComponent();


            // Edit Employee auto fill form 
            if (Application.Current.Properties["proj_edit_id"] != null)
            {
                int proj_id;
                var bool5 = Int32.TryParse(Application.Current.Properties["proj_edit_id"].ToString(), out proj_id);
                Projekt proj = Bibliothek.Projekt_nach_ID(proj_id);
                this.projName.Text = proj.Name;

                this.projOffene.Text = proj.OffeneZeitStunden.ToString();
                this.projGesamt.Text = proj.GesamtZeitStunden.ToString();
                this.projAktiv.IsChecked = proj.IstAktiv;

                this.projStart.SelectedDate = proj.StartDatum;
                this.projEnd.SelectedDate = proj.EndDatum;

                // Create hidden button to save employee id
                this.btnID.Tag = proj_id.ToString();

                // Edit window opened and loaded.. reset trigger property
                Application.Current.Properties["proj_edit_id"] = null;
            }
        }



        private void btnProj_Handler(object sender, RoutedEventArgs e)
        {

            Button myButton = (Button)sender;

            // Create new Project
            if (myButton.Tag.ToString() == "new")
            {
                Projekt newProj = new Projekt();
                int id;
                var bool4 = Int32.TryParse(Application.Current.Properties["proj_id"].ToString(), out id); // saved max id to increment
                newProj.ID = id += 1;
                Application.Current.Properties["proj_id"] = (id += 1).ToString();
                newProj.Name = this.projName.Text;

                int offen;
                var bool1 = Int32.TryParse(projOffene.Text, out offen);
                int gesamt;
                var bool2 = Int32.TryParse(projGesamt.Text, out gesamt);
                newProj.OffeneZeitStunden = offen;
                newProj.GesamtZeitStunden = gesamt;

                newProj.IstAktiv = projAktiv.IsChecked.Value;

                newProj.StartDatum = this.projStart.SelectedDate.Value;
                newProj.EndDatum = this.projEnd.SelectedDate.Value;
                Bibliothek.Projekt_Neu(newProj);
            }

            // Edit Project
            if (myButton.Tag.ToString() == "edit")
            {
                int id;
                var bool6 = Int32.TryParse(this.btnID.Tag.ToString(), out id);
                Projekt proj = Bibliothek.Projekt_nach_ID(id);
                proj.Name = this.projName.Text;

                int offen;
                var bool1 = Int32.TryParse(projOffene.Text, out offen);
                int gesamt;
                var bool2 = Int32.TryParse(projGesamt.Text, out gesamt);
                proj.OffeneZeitStunden = offen;
                proj.GesamtZeitStunden = gesamt;

                proj.IstAktiv = projAktiv.IsChecked.Value;

                proj.StartDatum = this.projStart.SelectedDate.Value;
                proj.EndDatum = this.projEnd.SelectedDate.Value;
                this.btnID.Tag = ""; // reset edit job id holder
            }

        }
    }
}

