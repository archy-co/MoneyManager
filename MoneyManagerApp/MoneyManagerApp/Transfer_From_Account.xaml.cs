using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    
    public partial class Transfer_From_Account : Window
    {
        public Transfer_From_Account()
        {
            InitializeComponent();
            LoadUserAccounts();
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

        private void LoadUserAccounts()
        {
            List<Account> userAccounts = GetUserAccounts();

            foreach (var account in userAccounts)
            {
                FromAccountComboBox.Items.Add(account.AccountsTitle);
            }
        }

        private void SumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            string newText = SumTextBox.Text + e.Text;

          
            if (!decimal.TryParse(newText, out _))
            {
                
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Transactions add_Transactions = new Add_Transactions();
            add_Transactions.Show();
            this.Close();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string selectedAccount = FromAccountComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedAccount))
            {
               
                using (var dbContext = new ApplicationContext())
                {
                    Account fromAccount = GetUserAccounts().FirstOrDefault(a => a.AccountsTitle == selectedAccount);
                    string description = DescriptionTextBox.Text;
                    string sum = SumTextBox.Text;
                    if (GetBalanceDifference(fromAccount.AccountsId) - Convert.ToDecimal(sum) < 0)
                    {
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = "Нехватає коштів для здійснення транзакції";
                        SumTextBox.ToolTip = toolTip;
                        SumTextBox.BorderBrush = Brushes.Red;
                        return;
                    }
                    else
                    {
                        Transaction transaction = new Transaction();
                        transaction.TransactionsType = 2;
                        transaction.TransactionsDescription = description;
                        transaction.TransactionsSum = Convert.ToDecimal(sum);
                        transaction.FkAccountsIdFrom = fromAccount.AccountsId;
                        dbContext.Transactions.Add(transaction);
                        dbContext.SaveChanges();
                        Home home = new Home();
                        home.Show();
                        this.Close();
                    }
                    
                }
            }
        }
        private decimal GetBalanceDifference(int accountId)
        {
            using (var dbContext = new ApplicationContext())
            {
                decimal sumType1 = dbContext.Transactions
                    .Where(t => t.FkAccountsIdTo == accountId && t.FkAccountsIdFrom == null && t.TransactionsType == 1)
                    .Sum(t => t.TransactionsSum);

                decimal sumType2 = dbContext.Transactions
                    .Where(t => t.FkAccountsIdTo == null && t.FkAccountsIdFrom == accountId && t.TransactionsType == 2)
                    .Sum(t => t.TransactionsSum);

                return sumType1 - sumType2;
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
    }
}
