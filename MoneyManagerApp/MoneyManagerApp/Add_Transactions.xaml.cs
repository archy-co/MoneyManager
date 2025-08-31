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

namespace MoneyManagerApp.Presentation
{

    public partial class Add_Transactions : Window
    {
        public Add_Transactions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
         private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void Button_Transfer_To_Account_Click(object sender, RoutedEventArgs e)
        {
            Transfer_To_Account transfer_To_Account = new Transfer_To_Account();
            transfer_To_Account.Show();
            this.Close();
        }

        private void Button_Transfer_From_Account_Click(object sender, RoutedEventArgs e)
        {
            Transfer_From_Account transfer_From_Account = new Transfer_From_Account();
            transfer_From_Account.Show();
            this.Close();
        }

        private void Button_Transfer_Between_Accounts_Click(object sender, RoutedEventArgs e)
        {
            Transfer_Between_Accounts transfer_Between_Accounts = new Transfer_Between_Accounts();
            transfer_Between_Accounts.Show();
            this.Close();

        }
        private void HomeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void AccountsLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Accounts accounts = new Accounts();
            accounts.Show();
            this.Close();

        }

        private void MyGoalsLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Goals goals = new Goals();
            goals.Show();
            this.Close();
        }

        private void StatisticLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Line_Graph line_Graph = new Line_Graph();
            line_Graph.Show();
            this.Close();
        }

        private void MyProfileLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            My_Profile my_Profile1 = new My_Profile();
            my_Profile1.Show();
            this.Close();
        }
    }
}
