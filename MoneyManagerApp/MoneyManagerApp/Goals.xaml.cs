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
using System.Windows.Shapes;

namespace MoneyManagerApp.Presentation
{

    public partial class Goals : Window
    {
        double accountBalance = GetAccountBalance(CurrentAccount.AccountId);
        public Goals()
        {
            InitializeComponent();
            LoadAccountGoals();
            
        }



        private void LoadAccountGoals()
        {
            List<Goal> accountGoals = GetAccountGoals();

            for (int i = 0; i < accountGoals.Count; i++)
            {
                TextBlock accountTextBlock = new TextBlock
                {
                    Text = accountGoals[i].GoalsTitle,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 160 + (80 * i), 410, 0),
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Top,
                    FontSize = 23,
                    Height = 30,
                    Width = 100
                };

                TextBlock balanceTextBlock = new TextBlock
                {
                    Text = (((accountBalance / (double)accountGoals[i].GoalsAmounttocollect)*100)).ToString("0.00") + "%",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 160 + (80 * i), 60, 0),
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 100,
                    FontSize = 23
                };

                Button seemoreButton = new Button
                {
                    Content = "See more",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(200, 160 + (80 * i), 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 83,
                    Height = 30,
                    FontSize = 20,
                    Tag = accountGoals[i],
                    Background = Brushes.Blue
                };
                seemoreButton.Click += SeemoreButton_Click;

                Button deleteButton = new Button
                {
                    Content = "Delete",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(450, 160 + (80 * i), 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 80,
                    Height = 30,
                    FontSize = 20,
                    Background = new SolidColorBrush(Color.FromRgb(170, 0, 215))
                };

                GoalsGrid.Children.Add(accountTextBlock);
                GoalsGrid.Children.Add(balanceTextBlock);
                GoalsGrid.Children.Add(seemoreButton);
                GoalsGrid.Children.Add(deleteButton);
            }
        }
        private static double GetAccountBalance(int accountId)
        {
            using (var dbContext = new ApplicationContext())
            {
                decimal sumType1 = dbContext.Transactions
                    .Where(t => t.FkAccountsIdTo == accountId && t.FkAccountsIdFrom == null && t.TransactionsType == 1)
                    .Sum(t => t.TransactionsSum);

                decimal sumType2 = dbContext.Transactions
                    .Where(t => t.FkAccountsIdTo == null && t.FkAccountsIdFrom == accountId && t.TransactionsType == 2)
                    .Sum(t => t.TransactionsSum);

                double percent = Convert.ToDouble((sumType1 - sumType2));
                return percent;
            }
        }

        private List<Goal> GetAccountGoals()
        {
            List<Goal> currentAccountGoals;
            using (var dbContext = new ApplicationContext())
            {
                currentAccountGoals = dbContext.Goals.Where(a => a.FkAccountsId == CurrentAccount.AccountId).ToList();
            }
            return currentAccountGoals;
        }

        private void SeemoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Goal selectedGoal)
            {
                Goal_Name goal_Name = new Goal_Name(selectedGoal.GoalsId);
                goal_Name.Show();
                this.Close();
            }
            
        }


        private void Button_Create_New_Goal_Click(object sender, RoutedEventArgs e)
        {
            Add_New_Goal add_New_Goal = new Add_New_Goal();
            add_New_Goal.Show();
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
