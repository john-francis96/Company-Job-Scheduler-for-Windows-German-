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
    /// Interaction logic for listjobs.xaml
    /// </summary>
    public partial class listjobs : Page
    {

        public listjobs()
        {
            InitializeComponent();
            List<Einsatz> jobList = Bibliothek.Einsatz_Alle();
            for (var i = 0; i < jobList.Count(); i++)
            {
                ListBoxItem job = new ListBoxItem();
                job.Tag = jobList[i].ID;
                job.Content = jobList[i].Name + " - " + jobList[i].Start.Date;
                job.Background = Brushes.White;
                job.MouseDoubleClick += this.lsJob_Handler;
                this.jobsListBox.Items.Add(job);
            }
        }

        public void lsJob_Handler(object sender, RoutedEventArgs e)
        {
            ListBoxItem myButton = (ListBoxItem)sender;

            Application.Current.Properties["job_edit_id"] = myButton.Tag.ToString();
            NavigationService.Navigate(new Uri("newJob.xaml", UriKind.Relative));

        }
    }
}
