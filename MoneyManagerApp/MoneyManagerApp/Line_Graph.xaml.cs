using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
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
using OxyPlot.Wpf;
using MoneyManagerApp.Presentation.Models;
using MoneyManagerApp.DAL.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OxyPlot.Axes;
using MaterialDesignThemes.Wpf;
using System.Globalization;

namespace MoneyManagerApp.Presentation
{
    public class TransactionDataPoint
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
    }



    public partial class Line_Graph : Window
    {
        ApplicationContext db = new ApplicationContext();

        public Line_Graph()
        {
           
            db.Database.EnsureCreated();
            InitializeComponent();
            CreateGraph();

        }

        private void CreateGraph()
        {
            int idOfCurrentUser = CurrentUser.UserId;
            int idOfAccountUser = CurrentAccount.AccountId;


            var data = db.Users.Where(x => x.UsersId == idOfCurrentUser).First().Accounts.ToList();

            /*DateTime startDate = new DateTime(2023, 12, 2, 0, 0, 0, DateTimeKind.Utc); // Початок діапазону
            DateTime endDate = new DateTime(2023, 12, 7, 23, 59, 59, DateTimeKind.Utc).AddTicks(-1); // Кінець діапазону*/



            /*DateTime? startDate = null;
            DateTime? endDate = null;
*/
            /* if (!string.IsNullOrWhiteSpace(startDateTextBox.Text) && DateTime.TryParse(startDateTextBox.Text, out DateTime parsedStartDate))
             {
                 startDate = parsedStartDate;
             }

             if (!string.IsNullOrWhiteSpace(endDateTextBox.Text) && DateTime.TryParse(endDateTextBox.Text, out DateTime parsedEndDate))
             {
                 endDate = parsedEndDate;
             }*/



            /*List<Transaction> tr = db.Transactions
                .Where(x => (x.FkAccountsIdTo == idOfAccountUser || x.FkAccountsIdFrom == idOfAccountUser)
                    && x.TransactionsDate >= startDate && x.TransactionsDate <= endDate)
                .OrderBy(x => x.TransactionsDate)
                .ToList();*/

            List<Transaction> tr = db.Transactions
                .Where(x => x.FkAccountsIdTo == idOfAccountUser || x.FkAccountsIdFrom == idOfAccountUser)
                .OrderBy(x => x.TransactionsDate)
                .ToList();

            decimal balance = 0;
            var groupedTransactions = tr.GroupBy(t => t.TransactionsDate.Date)
                .OrderBy(g => g.Key)
                .Select(g =>
                {
                    decimal totalSum = 0;
                    foreach (var transaction in g)
                    {
                        totalSum += transaction.TransactionsType == 1 ? transaction.TransactionsSum : -transaction.TransactionsSum;
                    }
                    return new
                    {
                        Date = g.Key,
                        TotalSum = totalSum
                    };
                })
                .ToList();

            List<TransactionDataPoint> dataPoints = new List<TransactionDataPoint>();

            foreach (var transaction in groupedTransactions)
            {
                balance += transaction.TotalSum;

                TransactionDataPoint dataPoint = new TransactionDataPoint
                {
                    Date = transaction.Date,
                    Sum = balance
                };

                dataPoints.Add(dataPoint);
            }



            var plotModel = new PlotModel { Title = "" };
            plotModel.Series.Clear();
        
            var series = new LineSeries
            {
                Title = "Transactions",
                MarkerType = OxyPlot.MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Blue
            };


            foreach (var dataPoint in dataPoints)
            {
                series.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(dataPoint.Date), Convert.ToDouble(dataPoint.Sum)));
            }



            plotModel.Series.Add(series);

            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "",
                StringFormat = "yyyy-MM-dd",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalType = DateTimeIntervalType.Days
            });

            plotView.Model = plotModel;

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateGraph();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CreateGraph();
        }









        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {

            int idOfCurrentUser = CurrentUser.UserId;
            int idOfAccountUser = CurrentAccount.AccountId;


            string startDateString = startDateTextBox.Text;
            string endDateString = endDateTextBox.Text;

            if (!DateTime.TryParse(startDateString, out DateTime startDate) || !DateTime.TryParse(endDateString, out DateTime endDate))
            {
                MessageBox.Show("Please enter valid date formats (yyyy-MM-dd) in both fields.");
                return;
            }

         
            if (startDate >= endDate)
            {
                MessageBox.Show("End date should be greater than the start date.");
                return;
            }

           
            if (string.IsNullOrWhiteSpace(startDateString) || string.IsNullOrWhiteSpace(endDateString))
            {
                MessageBox.Show("Please enter both start and end dates.");
                return;
            }

            DateTime startDateT = DateTime.Parse(startDateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
            DateTime endDateT = DateTime.Parse(endDateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime().AddDays(1).AddTicks(-1);

            List<Transaction> tr = db.Transactions
                .Where(x => (x.FkAccountsIdTo == idOfAccountUser || x.FkAccountsIdFrom == idOfAccountUser)
                    && x.TransactionsDate.ToUniversalTime() >= startDateT && x.TransactionsDate.ToUniversalTime() <= endDateT)
                .OrderBy(x => x.TransactionsDate)
                .ToList();


            decimal balance = 0;
            var groupedTransactions = tr.GroupBy(t => t.TransactionsDate.Date)
                .OrderBy(g => g.Key)
                .Select(g =>
                {
                    decimal totalSum = 0;
                    foreach (var transaction in g)
                    {
                        totalSum += transaction.TransactionsType == 1 ? transaction.TransactionsSum : -transaction.TransactionsSum;
                    }
                    return new
                    {
                        Date = g.Key,
                        TotalSum = totalSum
                    };
                })
                .ToList();

            List<TransactionDataPoint> dataPoints = new List<TransactionDataPoint>();

            foreach (var transaction in groupedTransactions)
            {
                balance += transaction.TotalSum;

                TransactionDataPoint dataPoint = new TransactionDataPoint
                {
                    Date = transaction.Date,
                    Sum = balance
                };

                dataPoints.Add(dataPoint);
            }



            var plotModel = new PlotModel { Title = "" };
            plotModel.Series.Clear();

            var series = new LineSeries
            {
                Title = "Transactions",
                MarkerType = OxyPlot.MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Blue
            };


            foreach (var dataPoint in dataPoints)
            {
                series.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(dataPoint.Date), Convert.ToDouble(dataPoint.Sum)));
            }



            plotModel.Series.Add(series);

            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "",
                StringFormat = "yyyy-MM-dd",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalType = DateTimeIntervalType.Days
            });

            plotView.Model = plotModel;
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
