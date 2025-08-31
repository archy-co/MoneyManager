using Microsoft.EntityFrameworkCore;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MoneyManagerApp.Presentation
{
    public partial class Accounts : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Accounts()
        {
            InitializeComponent();
            LoadUserAccounts();
        }

        private void LoadUserAccounts()
        {
            try
            {
                Style customButtonStyle = new Style(typeof(Button));

                customButtonStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(5)));


                Application.Current.Resources["CustomButtonStyle"] = customButtonStyle;
                List<Account> userAccounts = GetUserAccounts();

                for (int i = 0; i < userAccounts.Count; i++)
                {
                    TextBlock accountTextBlock = new TextBlock
                    {
                        Text = userAccounts[i].AccountsTitle,
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
                        Text = GetBalanceDifference(userAccounts[i].AccountsId).ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 160 + (80 * i), 60, 0),
                        TextWrapping = TextWrapping.Wrap,
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 100,
                        FontSize = 23
                    };

                    Button chooseButton = new Button
                    {
                        Content = "Choose",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(200, 160 + (80 * i), 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 80,
                        Height = 30,
                        FontSize = 20,
                        Tag = userAccounts[i],
                        Background = Brushes.Blue,
                        Foreground = Brushes.White,

                    };

                    chooseButton.Click += ChooseButton_Click;

                    Button deleteButton = new Button
                    {
                        Content = "Delete",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(450, 160 + (80 * i), 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 80,
                        Height = 30,
                        FontSize = 20,
                        Background = new SolidColorBrush(Color.FromRgb(170, 0, 215)),
                        Foreground = Brushes.White,
                        Tag = userAccounts[i],


                    };



                    deleteButton.Click += Delete_Button_Click;

                    AccountsGrid.Children.Add(accountTextBlock);
                    AccountsGrid.Children.Add(balanceTextBlock);
                    AccountsGrid.Children.Add(chooseButton);
                    AccountsGrid.Children.Add(deleteButton);
                }
                logger.Info("Рахунки користувача завантажені успішно.");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Помилка при завантаженні рахунків користувача");
            }
        }


        private decimal GetBalanceDifference(int accountId)
        {
            try
            {
                using (var dbContext = new ApplicationContext())
                {

                    decimal sumType1 = dbContext.Transactions
                        .Where(t => t.FkAccountsIdTo == accountId)
                        .Sum(t => t.TransactionsSum);


                    decimal sumType2 = dbContext.Transactions
                        .Where(t => t.FkAccountsIdFrom == accountId)
                        .Sum(t => t.TransactionsSum);


                    return sumType1 - sumType2;
                }
            } catch (Exception ex)
            {
                logger.Error(ex, "Помилка при обчисленні різниці балансу");
                return -1;
            }
        }

        public void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is Account selectedAccount)
                {


                    using (var dbContext = new ApplicationContext())
                    {
                        var accountToDelete = dbContext.Accounts.FirstOrDefault(a => a.AccountsId == selectedAccount.AccountsId);

                        if (accountToDelete != null)
                        {

                            var transactionsToDelete = dbContext.Transactions.Where(t => t.FkAccountsIdFrom == selectedAccount.AccountsId || t.FkAccountsIdTo == selectedAccount.AccountsId);
                            dbContext.Transactions.RemoveRange(transactionsToDelete);


                            dbContext.Accounts.Remove(accountToDelete);

                            dbContext.SaveChanges();
                        }
                    }
                }
                Accounts accounts = new Accounts();
                accounts.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Помилка при видаленні облікового запису");
            }
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Account selectedAccount)
            {
                CurrentAccount.SetCurrentAccount(selectedAccount.AccountsId, selectedAccount.AccountsTitle);
            }
            Home home = new Home();
            home.Show();
            this.Close();
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           
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

        private void Button_Create_New_Account_Click(object sender, RoutedEventArgs e)
        {
            Create_New_Account create_New_Account = new Create_New_Account();
            create_New_Account.Show();
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
