//using Microsoft.EntityFrameworkCore;
//using MoneyManagerApp.DAL;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation;
using MoneyManagerApp.Presentation.Models;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
//using MoneyManagerApp.DAL.Models;
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

namespace MoneyManagerApp
{

    public partial class MainWindow : Window
    {
        private  ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            this.db = new ApplicationContext();
        }

        public MainWindow(ApplicationContext db) : this()
        {
            this.db = db;
        }




        /*public ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            
        }*/

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {                               
            db.Database.EnsureCreated();
            UsernameOrEmailTextBox.Text = "";
            PasswordTextBox.Password = string.Empty;

        }


        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string UsernameOrEmail = UsernameOrEmailTextBox.Text;
            string password = PasswordTextBox.Password;

            User user = db.Users.FirstOrDefault(u => u.UsersEmail == UsernameOrEmail);

            if (user != null)
            {
                byte[] salt = Convert.FromBase64String(user.PasswordSalt);
                byte[] hash = PasswordHelper.GenerateHash(password, salt);

                string enteredPasswordHash = Convert.ToBase64String(hash);

                if (enteredPasswordHash == user.PasswordHash)
                {
                    CurrentUser.SetCurrentUser(user.UsersId, user.UsersName);
                    if (CurrentUserHasAccount(CurrentUser.UserId))
                    {
                        // Successful login
                        Home home = new Home();
                        home.Show();
                        this.Close();

                        

                    }
                    else
                    {
                        Create_New_Account create_New_Account = new Create_New_Account();
                        create_New_Account.Show();
                        this.Close();
                    }
                    
                }
                else
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Content = "Не правильньний логін або пароль";
                    UsernameOrEmailTextBox.ToolTip = toolTip;
                    UsernameOrEmailTextBox.BorderBrush = Brushes.Red;
                    PasswordTextBox.BorderBrush = Brushes.Red;
                    ClearFields();
                    return;

                }
            }
            else
            {

                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Не правильний логін або пароль";
                UsernameOrEmailTextBox.ToolTip = toolTip;
                UsernameOrEmailTextBox.BorderBrush = Brushes.Red;
                PasswordTextBox.BorderBrush = Brushes.Red;
                ClearFields();
                return;

            }
        }

        public bool CurrentUserHasAccount(int currentUserId)
        {
            using (var dbContext = new ApplicationContext()) 
            {
                var currentUserAccount = dbContext.Accounts.FirstOrDefault(a => a.FkUsersId == currentUserId);
                if (currentUserAccount != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
           
            Sing_Up signUpWindow = new Sing_Up();
            signUpWindow.Show();
            this.Close();

        }

        public void ClearFields()
        {
            UsernameOrEmailTextBox.Text = "";
            PasswordTextBox.Clear();

           

        }


        public string GetUsernameOrEmailText()
        {
            return UsernameOrEmailTextBox.Text;
        }

        public string GetPasswordText()
        {
            return PasswordTextBox.Password;
        }


        public void SetUsernameOrEmailText(string text)
        {
            UsernameOrEmailTextBox.Text = text;
        }

        public void SetPasswordText(string text)
        {
            PasswordTextBox.Password = text;
        }

    }
}
