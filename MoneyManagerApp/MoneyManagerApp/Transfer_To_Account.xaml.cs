using Microsoft.EntityFrameworkCore;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
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

namespace MoneyManagerApp.Presentation
{

    public partial class Transfer_To_Account : Window
    {
        public Transfer_To_Account()
        {
            InitializeComponent();
            LoadUserAccounts();
        }

        private void LoadUserAccounts()
        {
            List<Account> userAccounts = GetUserAccounts();

            foreach (var account in userAccounts)
            {
                AccountComboBox.Items.Add(account.AccountsTitle);
            }
        }
        private List<Account> GetUserAccounts()
        {
            List<Account> currentUserAccounts;
            using (var dbContext = new ApplicationContext())
            {
                currentUserAccounts = dbContext.Accounts.Where(a => a.FkUsersId == CurrentUser.UserId).ToList();
            }
            return currentUserAccounts;
        }
        private void SumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            string newText = SumTextBox.Text + e.Text;

           
            if (!decimal.TryParse(newText, out _))
            {
               
                e.Handled = true;
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string selectedAccount = AccountComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedAccount))
            {

                using (var dbContext = new ApplicationContext())
                {
                    Account account = GetUserAccounts().FirstOrDefault(a => a.AccountsTitle == selectedAccount);
                    string description = DescriptionTextBox.Text;
                    string sum = SumTextBox.Text;
                    Transaction transaction = new Transaction();
                    transaction.TransactionsType = 1;
                    transaction.TransactionsDescription = description;
                    try
                    {
                        transaction.TransactionsSum = Convert.ToDecimal(sum);
                    }
                    catch (Exception)
                    {


                        return;
                    }
                
                    transaction.FkAccountsIdTo = account.AccountsId;
                    dbContext.Transactions.Add(transaction);
                    dbContext.SaveChanges();
                    Home home = new Home();
                    home.Show();
                    this.Close();
                }
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
            Add_Transactions add_Transactions = new Add_Transactions();
            add_Transactions.Show();
            this.Close();
            
        }
    }
}
