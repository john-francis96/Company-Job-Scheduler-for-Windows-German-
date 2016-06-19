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
    /// Interaction logic for newJob.xaml
    /// </summary>
    public partial class newJob : Page
    {
        public newJob()
        {
            InitializeComponent();

            if (Application.Current.Properties["job_edit_id"] == null) // not in edit mode
            {
                // Fill list of mitarbeiters
                var listMitarbeiter = Bibliothek.Mitarbeiter_Alle();
                for (var i = 0; i < listMitarbeiter.Count(); i++)
                {
                    ComboboxItem mitarbeiter = new ComboboxItem(); // Defined own combobox object (defined below) to save name & id
                    mitarbeiter.Text = listMitarbeiter[i].Name;
                    mitarbeiter.Value = listMitarbeiter[i].ID;
                    jobMit.Items.Add(mitarbeiter);
                }
                jobMit.SelectedIndex = 0;

                // Fill list of projects
                var listProjekte = Bibliothek.Projekt_Alle();
                for (var i = 0; i < listProjekte.Count(); i++)
                {
                    ComboboxItem projekt = new ComboboxItem(); // Defined own combobox object (defined below) to save name & id
                    projekt.Text = listProjekte[i].Name;
                    projekt.Value = listProjekte[i].ID;
                    jobProj.Items.Add(projekt);
                }
                jobProj.SelectedIndex = 0;
            }
            

            // Edit Employee auto fill form 
            if (Application.Current.Properties["job_edit_id"] != null)
            {
                int job_id;
                var bool5 = Int32.TryParse(Application.Current.Properties["job_edit_id"].ToString(), out job_id);
                Einsatz job = Bibliothek.Einsatz_nach_ID(job_id);
                this.jobName.Text = job.Name;

                // Fill list of mitarbeiters
                var listMit = Bibliothek.Mitarbeiter_Alle();
                var index1 = 0;
                for (var i = 0; i < listMit.Count(); i++)
                {
                    ComboboxItem mitarbeiter = new ComboboxItem(); // Defined own combobox object (defined below) to save name & id
                    mitarbeiter.Text = listMit[i].Name;
                    mitarbeiter.Value = listMit[i].ID;
                    jobMit.Items.Add(mitarbeiter);
                    if (job.Mitarbeiter.ID == listMit[i].ID)
                    {
                        jobMit.SelectedIndex = index1;
                    }
                    else
                    {
                        index1 += 1;
                    }
                }


                // Fill list of projects
                var listProj = Bibliothek.Projekt_Alle();
                var index2 = 0; 
                for (var i = 0; i < listProj.Count(); i++)
                {
                    ComboboxItem projekt = new ComboboxItem(); // Defined own combobox object (defined below) to save name & id
                    projekt.Text = listProj[i].Name;
                    projekt.Value = listProj[i].ID;
                    jobProj.Items.Add(projekt);
                    if (job.Projekt.ID == listProj[i].ID)
                    {
                        jobProj.SelectedIndex = index2;
                    }
                    else
                    {
                        index2 += 1;
                    }
                }

               
                this.jobStart.SelectedDate = job.Start;
                this.jobEnd.SelectedDate = job.Ende;

                this.jobSubmit.Tag = "edit";

                // Create hidden button to save employee id
                this.btnID.Tag = job_id.ToString();

                // Edit window opened and loaded.. reset trigger property
                Application.Current.Properties["job_edit_id"] = null;
            }
        }



        private void btnJob_Handler(object sender, RoutedEventArgs e)
        {

            Button myButton = (Button)sender;

            // Create new Job
            if (myButton.Tag.ToString() == "new")
            {
                Einsatz newJob = new Einsatz();
                int id;
                var bool4 = Int32.TryParse(Application.Current.Properties["job_id"].ToString(), out id); // saved max id to increment
                newJob.ID = id += 1;
                Application.Current.Properties["job_id"] = (id += 1).ToString();
                newJob.Name = this.jobName.Text;

                // Get Mitarbeiter und Projekte ID
                int mit_id;
                var bool1 = Int32.TryParse((jobMit.SelectedItem as ComboboxItem).Value.ToString(), out mit_id);
                int proj_id;
                var bool2 = Int32.TryParse((jobProj.SelectedItem as ComboboxItem).Value.ToString(), out proj_id);

                newJob.Mitarbeiter = Bibliothek.Mitarbeiter_nach_ID(mit_id); // Tag has id and save Bibliotek get from id
                newJob.Projekt = Bibliothek.Projekt_nach_ID(proj_id);
                newJob.Start = this.jobStart.SelectedDate.Value;
                newJob.Ende = this.jobEnd.SelectedDate.Value;

                // check validity
                bool valid = isValid(newJob, new Einsatz(), false);

                if (valid == true)
                {
                    // Create New Job
                    Bibliothek.EinsatzNeu(newJob);

                    // Job Created - show success and reset form
                    this.jobName.Text = "";
                    this.jobMit.SelectedIndex = 1;
                    this.jobProj.SelectedIndex = 1;
                    this.jobStart.SelectedDate = null;
                    this.jobEnd.SelectedDate = null;

                    this.success.Text = "Einsatz erfolgrichlich erstellt";
                    this.success.Background = Brushes.LimeGreen;
                    this.success.Visibility = Visibility.Visible;
                }
              
            }

            // Edit Job
            if (myButton.Tag.ToString() == "edit")
            {
                int id;
                var bool6 = Int32.TryParse(this.btnID.Tag.ToString(), out id);

                // Einsatz job = Bibliothek.Einsatz_nach_ID(id);
                Einsatz job = new Einsatz();

                job.Name = this.jobName.Text;

                // Get Mitarbeiter und Projekte ID
                int mit_id;
                var bool1 = Int32.TryParse((jobMit.SelectedItem as ComboboxItem).Value.ToString(), out mit_id);
                int proj_id;
                var bool2 = Int32.TryParse((jobProj.SelectedItem as ComboboxItem).Value.ToString(), out proj_id);

                job.Mitarbeiter = Bibliothek.Mitarbeiter_nach_ID(mit_id); // Tag has id and save Bibliotek get from id
                job.Projekt = Bibliothek.Projekt_nach_ID(proj_id);
                job.Start = this.jobStart.SelectedDate.Value;
                job.Ende = this.jobEnd.SelectedDate.Value;

                bool valid = isValid(job, Bibliothek.Einsatz_nach_ID(id), true);
                if (valid == true)
                {
                    Einsatz original_job = Bibliothek.Einsatz_nach_ID(id);
                    original_job.Name = this.jobName.Text;

                    // Get Mitarbeiter und Projekte ID
                    int _mit_id;
                    var _bool1 = Int32.TryParse((jobMit.SelectedItem as ComboboxItem).Value.ToString(), out _mit_id);
                    int _proj_id;
                    var _bool2 = Int32.TryParse((jobProj.SelectedItem as ComboboxItem).Value.ToString(), out _proj_id);

                    original_job.Mitarbeiter = Bibliothek.Mitarbeiter_nach_ID(_mit_id); // Tag has id and save Bibliotek get from id
                    original_job.Projekt = Bibliothek.Projekt_nach_ID(_proj_id);
                    original_job.Start = this.jobStart.SelectedDate.Value;
                    original_job.Ende = this.jobEnd.SelectedDate.Value;

                    this.btnID.Tag = ""; // reset edit job id holder

                    // Job Edited - show success and reset form
                    this.jobName.Text = "";
                    this.jobMit.SelectedIndex = 1;
                    this.jobProj.SelectedIndex = 1;
                    this.jobStart.SelectedDate = null;
                    this.jobEnd.SelectedDate = null;

                    this.success.Text = "Einsatz erfolgrichlich gearbeitet";
                    this.success.Background = Brushes.LimeGreen;
                    this.success.Visibility = Visibility.Visible;
                }

            }

        }

        private bool isValid(Einsatz newJob, Einsatz oldJob, bool isEdit) // oldJob only use when editing job
        {
            // check for invalidation
            bool valid = true;
            // Check if Mitarbeiter already has a job today
            int daydiff = newJob.Ende.DayOfYear - newJob.Start.DayOfYear;
            for (int i = 0; i <= daydiff; i++)
            {
                if (isEdit == true)
                {
                    if (oldJob.Start.DayOfYear <= newJob.Start.AddDays(i).DayOfYear && oldJob.Ende.DayOfYear >= newJob.Start.AddDays(i).DayOfYear)
                    {
                        // Only run when in edit mode
                        // Day already in original job so no need to check - skip to next day
                        continue;
                    }
                }
                List<Einsatz> jobList = Bibliothek.Einsaetz_an_Datum(newJob.Start.AddDays(i));
                for (var k = 0; k < jobList.Count(); k++)
                {
                    if (jobList[k].Mitarbeiter.ID == newJob.Mitarbeiter.ID)
                    {
                        valid = false;
                        this.success.Text = "Doppeleinsatz für Mitarbeiter. Das ist Nicht Erlaubt!";
                        this.success.Background = Brushes.Red;
                        this.success.Visibility = Visibility.Visible;
                    }
                }
            }

            // Check if job out of project bounds
            if (newJob.Projekt.EndDatum.DayOfYear < newJob.Ende.DayOfYear || newJob.Projekt.StartDatum.DayOfYear > newJob.Start.DayOfYear)
            {
                // Display Error
                this.success.Text = "Einsatz Zeitspanne ausserhalb Projekt Zeitspanne!";
                this.success.Background = Brushes.Red;
                this.success.Visibility = Visibility.Visible;
                valid = false;
            }
            return valid;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
