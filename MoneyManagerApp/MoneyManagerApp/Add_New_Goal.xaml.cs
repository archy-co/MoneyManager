using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
using NLog;
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
    public partial class Add_New_Goal : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Add_New_Goal()
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

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string selectedAccount = AccountComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedAccount))
            {

                using (var dbContext = new ApplicationContext())
                {
                    Account account = GetUserAccounts().FirstOrDefault(a => a.AccountsTitle == selectedAccount);
                    string title = GoalNameTextBox.Text;
                    string description = DescriptionTextBox.Text;
                    string ammountToCollect = AmmountToCollectTextBox.Text;
                    Goal goal = new Goal();
                    goal.GoalsTitle = title;
                    goal.GoalsDescription = description;
                    goal.GoalsAmounttocollect = Convert.ToDecimal(ammountToCollect);
                    goal.FkAccountsId = account.AccountsId;
                    dbContext.Goals.Add(goal);
                    dbContext.SaveChanges();
                    Home home = new Home();
                    home.Show();
                    this.Close();
                }
            }
        }


        private void AmmountToCollectTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            string newText = AmmountToCollectTextBox.Text + e.Text;


            if (!decimal.TryParse(newText, out _))
            {

                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
