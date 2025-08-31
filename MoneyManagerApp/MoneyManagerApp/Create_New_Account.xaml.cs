using System;
using System.Windows;
using System.Windows.Input;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;

namespace MoneyManagerApp.Presentation
{
    public partial class Create_New_Account : Window
    {
        private ApplicationContext _dbContext; 

        public Create_New_Account()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext(); 
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            string accountName = AccountNameTextBox.Text;

            if (!string.IsNullOrEmpty(accountName))
            {
                try
                {
         
                    Account newAccount = new Account();
                    newAccount.AccountsTitle = accountName;
                    newAccount.FkUsersId = CurrentUser.UserId;

                    _dbContext.Accounts.Add(newAccount);
                    _dbContext.SaveChanges();

                    if (CountOfOpenningHomePage.Count != 1)
                    {
                        Accounts accounts = new Accounts();
                        accounts.Show();
                        this.Close();
                    }
                    else
                    {
                        Home home = new Home();
                        home.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving account: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter an account name!");
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Accounts accounts = new Accounts();
            accounts.Show();
            this.Close();
        }
    }
}
