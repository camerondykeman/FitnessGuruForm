using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessGuru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //onLoad
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load combo boxes
            //load years
            List<int> years = new List<int>();
            int end = DateTime.Now.Year - 16;
            for (int i = 1910; i != end; i++)
            {
                years.Add(i);
            }
            years.Reverse();
            comboBoxYear.ItemsSource = years;
            //load months
            List<string> months = new List<string> {"JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            comboBoxMonth.ItemsSource = months;

            //load provinces
            List<string> provinces = new List<string> { "AB", "BC", "MB", "NB", "NL", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };
            comboBoxProvince.ItemsSource = provinces;

            //load trainers
            List<string> trainers = new List<string> { "Nitro", "Blaze", "Laser", "Viper", "Elektra", "Storm", "Tank", "Jazz", "Hawk", "Diamond", "Lace", "Turbo" };
            comboBoxTrainer.ItemsSource = trainers;
        }

        //select month
        private void comboBoxMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected month
            int selectedMonth = comboBoxMonth.SelectedIndex + 1;
            int selectedYear = Int32.Parse(comboBoxYear.SelectedItem.ToString());
            //get days in current month
            int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth) + 1;
            List<int> days = new List<int>();
            for (int i = 1; i < daysInMonth; i++)
            {
                days.Add(i);
            }
            comboBoxDay.ItemsSource = days;
            comboBoxDay.IsEnabled = true;
        }

        //select Year
        private void comboBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxMonth.IsEnabled = true;
        }

        //Cancel Application
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Cancel this Applicaion?", "Cancel?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //Exit the form
                Close();
            }
        }

        //save application
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            List<string> info = new List<string>();
            //required
            string first = textBoxFirstName.Text;
            string last = textBoxLastName.Text;
            string dateofbirth = comboBoxYear.SelectedValue.ToString() + " " + comboBoxMonth.SelectedValue.ToString() + " " + comboBoxDay.SelectedValue.ToString();
            string address = textBoxAddress.Text;
            string city = textBoxCity.Text;
            string province = comboBoxProvince.SelectedValue.ToString();
            string postalcode = textBoxPostalCode.Text;
            //optional
            string comments = textBoxComments.Text;
            string trainer = comboBoxTrainer.SelectedValue.ToString();
            //get selected item from radio group
            string level = "";
            if (radioButtonBeginner.IsChecked == true)
                level = "Beginner";
            else if (radioButtonExperienced.IsChecked == true)
                level = "Experienced";
            else if (radioButtonSuperFreak.IsChecked == true)
                level = "Super Freak";
            //get selections from billing
            string billing = "";
            if (checkBoxDirect.IsChecked == true)
                billing = billing + "Bank Account Direct Deposit, ";
            else if (checkBoxMonthly.IsChecked == true)
                billing = billing + "Monthly, ";
            else if (checkBoxAnnual.IsChecked == true)
                billing = billing + "Annual Membership, ";

            //make sure none are null
            if (first.CompareTo("") == 0 || last.CompareTo("") == 0 || dateofbirth.CompareTo("") == 0 || address.CompareTo("") == 0 || city.CompareTo("") == 0 || postalcode.CompareTo("") == 0)
            {
                MessageBox.Show("Error - Please fill in all required fields.", "Incomplete", MessageBoxButton.OK);
            }
            //if not null, save to file
            else
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("C://NewMember.txt");
                file.WriteLine("First Name: " + first);
                file.WriteLine("Last Name: " + last);
                file.WriteLine("Date of Birth: " + dateofbirth);
                file.WriteLine("Address: " + address);
                file.WriteLine("City: " + city);
                file.WriteLine("Province: " + province);
                file.WriteLine("Postal Code: " + postalcode);
                file.WriteLine("Comments: " + comments);
                file.WriteLine("Trainer: " + trainer);
                file.WriteLine("Level: " + level);
                file.WriteLine("Billing Info: " + billing);
                file.Close();
                if (MessageBox.Show("Application Complete - Information has been saved to file.\rBegin new application?", "Complete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //clear all
                    try
                    {
                        textBoxFirstName.Clear();
                        textBoxLastName.Clear();
                        textBoxAddress.Clear();
                        textBoxCity.Clear();
                        textBoxComments.Clear();
                        textBoxPostalCode.Clear();
                        checkBoxAnnual.IsChecked = false;
                        checkBoxDirect.IsChecked = false;
                        checkBoxMonthly.IsChecked = false;
                        radioButtonBeginner.IsChecked = false;
                        radioButtonExperienced.IsChecked = false;
                        radioButtonSuperFreak.IsChecked = false;
                        comboBoxDay.SelectedIndex = -1;
                        comboBoxMonth.SelectedIndex = -1;
                        comboBoxYear.SelectedIndex = -1;
                        comboBoxTrainer.SelectedIndex = -1;
                        comboBoxProvince.SelectedIndex = -1;
                    }
                    catch (Exception ex) { };

                }
            
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - Please fill in all required fields.", "Incomplete", MessageBoxButton.OK);
            };
        }

        private void comboBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
