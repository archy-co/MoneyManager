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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyManagerApp.Presentation
{

    public partial class Security_Setting : Window
    {
        public Security_Setting()
        {
            InitializeComponent();
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            My_Profile my_Profile = new My_Profile();
            my_Profile.Show();
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

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = OldPasswordTextBox.Password;
            string newPassword = NewPasswordTextBox.Password;
            string repeatPassword = RepeatPasswordTextBox.Password;
            var dbContext = new ApplicationContext();

            User user = dbContext.Users.FirstOrDefault(u => u.UsersId == CurrentUser.UserId);
            
            if (user != null)
            {
                byte[] salt = Convert.FromBase64String(user.PasswordSalt);
                byte[] hash = PasswordHelper.GenerateHash(oldPassword, salt);

                string oldPasswordHash = Convert.ToBase64String(hash);

                if (oldPasswordHash == user.PasswordHash)
                {
                    if(newPassword == repeatPassword)
                    {
                        (byte[], byte[]) T = PasswordHelper.GetHashAndSalt(newPassword);

                        byte[] new_salt = T.Item1;
                        byte[] new_hash = T.Item2;

                        user.PasswordHash = Convert.ToBase64String(new_hash);
                        user.PasswordSalt = Convert.ToBase64String(new_salt);

                        dbContext.SaveChanges();

                
                        
                        My_Profile my_Profile = new My_Profile();
                        my_Profile.Show();
                        this.Close();
                    }
                    else
                    {
                        OldPasswordTextBox.ToolTip = null;
                        OldPasswordTextBox.BorderBrush = Brushes.LightGray;
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = "Паролі не співпадають";
                        NewPasswordTextBox.BorderBrush = Brushes.Red;
                        RepeatPasswordTextBox.BorderBrush = Brushes.Red;
                        NewPasswordTextBox.ToolTip = toolTip;
                        RepeatPasswordTextBox.ToolTip = toolTip;
                        ClearFields();
                        return;
                    }

                }
                else
                {
                    NewPasswordTextBox.BorderBrush = Brushes.LightGray;
                    RepeatPasswordTextBox.BorderBrush = Brushes.LightGray;
                    NewPasswordTextBox.ToolTip = null;
                    RepeatPasswordTextBox.ToolTip = null;
                    ToolTip toolTip = new ToolTip();
                    toolTip.Content = "Неправильньний пароль";
                    OldPasswordTextBox.BorderBrush = Brushes.Red;
                    OldPasswordTextBox.ToolTip = toolTip;
                    ClearFields();
                    return;
                }
            }
            else
            {

                

            }
        }
        private void ClearFields()
        {
            OldPasswordTextBox.Clear();
            NewPasswordTextBox.Clear();
            RepeatPasswordTextBox.Clear();



        }
    }

}


