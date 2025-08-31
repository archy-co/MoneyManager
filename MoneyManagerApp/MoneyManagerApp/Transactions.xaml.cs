

using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;



namespace MoneyManagerApp.Presentation
{
    public partial class Transactions : Window
    {
        public ObservableCollection<Transaction> TransactionList { get; set; }

        public Transactions()
        {
            InitializeComponent();
            TransactionList = new ObservableCollection<Transaction>(GetCurrentUserTransactions());
            LoadUserTransactions();
        }

        private void LoadUserTransactions()
        {
            
            DataContext = this;
        }




        private void ButtonSaveAsExel_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var dbContext = new ApplicationContext())
            {
                var transactions = dbContext.Transactions.ToList();

                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (var excelPackage = new OfficeOpenXml.ExcelPackage())
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Transactions");

                    // Додайте заголовки для колонок
                    worksheet.Cells[1, 1].Value = "Transaction ID";
                    worksheet.Cells[1, 2].Value = "Transaction Type";
                    worksheet.Cells[1, 3].Value = "Account ID From";
                    worksheet.Cells[1, 4].Value = "Account ID To";
                    worksheet.Cells[1, 5].Value = "Description";
                    worksheet.Cells[1, 6].Value = "Transaction Sum";
                    worksheet.Cells[1, 7].Value = "Transaction Date";

                    int row = 2; // Початковий рядок для даних

                    foreach (var transaction in transactions)
                    {
                        worksheet.Cells[row, 1].Value = transaction.TransactionsId;
                        worksheet.Cells[row, 2].Value = transaction.TransactionsType ?? 0;
                        worksheet.Cells[row, 3].Value = transaction.FkAccountsIdFrom.HasValue ? transaction.FkAccountsIdFrom.ToString() : "null";
                        worksheet.Cells[row, 4].Value = transaction.FkAccountsIdTo.HasValue ? transaction.FkAccountsIdTo.ToString() : "null";

                        worksheet.Cells[row, 5].Value = transaction.TransactionsDescription ?? "";
                        worksheet.Cells[row, 6].Value = transaction.TransactionsSum;
                        worksheet.Cells[row, 7].Value = transaction.TransactionsDate;

                        row++;
                    }

                    // Зберігання файлу Excel
                    string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                    string filePath = System.IO.Path.Combine(projectDirectory, "ExelFile", "Transactions.xlsx");
                    
                    FileInfo excelFile = new FileInfo(filePath);
                    try
                    {
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Файл успішно збережено.", "Успіх");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося зберегти файл: {ex.Message}", "Помилка");
                    }
                }
            }
        }





        private List<Transaction> GetCurrentUserTransactions()
        {
            List<Transaction> userTransactions;

            using (var dbContext = new ApplicationContext())
            {
                var currentUserAccounts = dbContext.Accounts.Where(a => a.FkUsersId == CurrentUser.UserId).Select(a => a.AccountsId).ToList();

                userTransactions = dbContext.Transactions
                    .Where(t => currentUserAccounts.Contains((int)t.FkAccountsIdFrom) || currentUserAccounts.Contains((int)t.FkAccountsIdTo))
                    .ToList();
                foreach(var transaction in userTransactions)
                {
                    transaction.FkAccountsIdToNavigation = dbContext.Accounts.FirstOrDefault(a => a.AccountsId == transaction.FkAccountsIdTo);
                    transaction.FkAccountsIdFromNavigation = dbContext.Accounts.FirstOrDefault(a => a.AccountsId == transaction.FkAccountsIdFrom);
                }
            }

            return userTransactions;
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

        private void ButtonAddTransasction_Click(object sender, RoutedEventArgs e)
        {
            Add_Transactions add_Transactions = new Add_Transactions();
            add_Transactions.Show();
            this.Close();
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

        private void ButtonApplyFilters_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
