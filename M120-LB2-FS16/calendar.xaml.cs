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
    /// Interaction logic for calendar.xaml
    /// </summary>
    public partial class calendar : Page
    {

        public calendar()
        {
            InitializeComponent();
            constructCalendar();
            //fillCalendar();
                
        }

        // Handle month forward/backward clicks
        public void monthHandler_Click(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;
            int to_change; // month forward or backward
            var bool2 = Int32.TryParse(myButton.Tag.ToString(), out to_change);
            if (Application.Current.Properties["month_change"] != null)
            {
                int app; // current month from which to add/subtract month
                var bool3 = Int32.TryParse(Application.Current.Properties["month_change"].ToString(), out app);
                app += to_change;
                Application.Current.Properties["month_change"] = app.ToString();
            }
            else
            {
                Application.Current.Properties["month_change"] = to_change.ToString();
            }
            constructCalendar();

        }

        public void constructCalendar()
        {
            // Clear header
            for (var i = 0; i < calendarParent.Children.Count; i++)
            {
                if (Grid.GetRow(calendarParent.Children[i]) == 0)
                {
                    calendarParent.Children.Remove(calendarParent.Children[i]);
                }
            }
            // Clear calendar
            this.calendarGrid.Children.Clear();


            int month_diff = 0;
            // if pre or post month
            if (Application.Current.Properties["month_change"] != null)
            {;
                var bool_tmp = Int32.TryParse(Application.Current.Properties["month_change"].ToString(), out month_diff);
            }
            DateTime now = DateTime.Now;
            now = now.AddMonths(month_diff);

            var month = now.Month;
            var year = now.Year;
            var daysInMonth = System.DateTime.DaysInMonth(year, month);
            var dayCounter = 0;

            // Calendar header
            TextBlock month_name = new TextBlock();
            month_name.Text = now.ToString("MMMM") + " " + now.ToString("yyyy");
            month_name.TextAlignment = TextAlignment.Center;
            this.calendarParent.Children.Add(month_name);
            Grid.SetRow(month_name, 0);
            Grid.SetColumn(month_name, 1);
            // Previous month
            var pre_month = now.AddMonths(-1);
            Button pre_month_name = new Button();
            pre_month_name.Content = pre_month.ToString("MMM");
            this.calendarParent.Children.Add(pre_month_name);
            pre_month_name.Click += this.monthHandler_Click;
            pre_month_name.Tag = "-1";
            Grid.SetRow(pre_month_name, 0);
            Grid.SetColumn(pre_month_name, 0);
            // Next Month
            var post_month = now.AddMonths(1);
            Button post_month_name = new Button();
            post_month_name.Content = post_month.ToString("MMM");
            post_month_name.Click += this.monthHandler_Click;
            post_month_name.Tag = "1";
            this.calendarParent.Children.Add(post_month_name);
            Grid.SetRow(post_month_name, 0);
            Grid.SetColumn(post_month_name, 2);


            DateTime day = new DateTime(year, month, 1);

            List<Einsatz> jobList = Bibliothek.Einsatz_Alle();

            // Draw 28 days... base number for everymonth & is a multiple of 7 which is cal no. of columns
            for (var k = 0; k < 4; k++)
            {
                for (var l = 0; l < 7; l++)
                {
                    Grid cellGrid = new Grid();
                    cellGrid.RowDefinitions.Add(new RowDefinition());
                    cellGrid.RowDefinitions[0].Height = new GridLength(0.2, GridUnitType.Star);
                    cellGrid.RowDefinitions.Add(new RowDefinition());
                    dayCounter++;
                    TextBlock tblock = new TextBlock();
                    tblock.Text = dayCounter.ToString();
                    cellGrid.Children.Add(tblock);
                    tblock.FontSize = 10;
                    tblock.Background = Brushes.Azure;

                    ListBox cell = new ListBox();
                    for (var i = 0; i < jobList.Count(); i++)
                    {
                        if (jobList[i].Start.DayOfYear <= day.DayOfYear && jobList[i].Ende.DayOfYear >= day.DayOfYear && jobList[i].Ende.Year == day.Year) // for jobs in current month
                        {
                            ListBoxItem tblock2 = new ListBoxItem();
                            tblock2.Tag = jobList[i].ID; // Set id tag to open proper job on entry double click
                            tblock2.MouseDoubleClick += this.lsJob_Handler;

                            tblock2.Content = jobList[i].Name.ToString();
                            tblock2.FontSize = 8;
                            tblock2.Height = 20;
                            tblock2.Background = Brushes.BlueViolet;
                            cell.Items.Add(tblock2);
                        }
                    }
                    day = day.AddDays(1);
                    cellGrid.Children.Add(cell);
                    this.calendarGrid.Children.Add(cellGrid);
                    Grid.SetRow(cell, 1);
                    Grid.SetRow(cellGrid, k);
                    Grid.SetColumn(cellGrid, l);
                }
            }

            // Draw rest of the days for months which are longer than 28 days
            var daysRemaining = daysInMonth - dayCounter;
            for (var j = 0; j < daysRemaining; j++)
            {
                Grid cellGrid = new Grid();
                cellGrid.RowDefinitions.Add(new RowDefinition());
                cellGrid.RowDefinitions[0].Height = new GridLength(0.2, GridUnitType.Star);
                cellGrid.RowDefinitions.Add(new RowDefinition());
                dayCounter++;
                TextBlock tblock = new TextBlock();
                var text = 29 + j;
                tblock.Text = text.ToString();
                tblock.FontSize = 10;
                tblock.Background = Brushes.Azure;
                cellGrid.Children.Add(tblock);


                ListBox cell = new ListBox();
                for (var i = 0; i < jobList.Count(); i++)
                {
                    if (jobList[i].Start.DayOfYear <= day.DayOfYear && jobList[i].Ende.DayOfYear >= day.DayOfYear) // for jobs in current month
                    {
                        ListBoxItem tblock2 = new ListBoxItem();
                        tblock2.Tag = jobList[i].ID; // Set id tag to open proper job on entry double click
                        tblock2.MouseDoubleClick += this.lsJob_Handler;

                        tblock2.Content = jobList[i].Name.ToString();
                        tblock2.FontSize = 8;
                        tblock2.Height = 20;
                        tblock2.Background = Brushes.BlueViolet;
                        cell.Items.Add(tblock2);
                    }
                }
                day = day.AddDays(1);

                cellGrid.Children.Add(cell);
                this.calendarGrid.Children.Add(cellGrid);
                Grid.SetRow(cell, 1);
                Grid.SetRow(cellGrid, 5);
                Grid.SetColumn(cellGrid, j);
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
