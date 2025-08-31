using Microsoft.EntityFrameworkCore.Query.Internal;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

    public partial class My_Profile : Window
    {
        public My_Profile()
        {
            InitializeComponent();
            
            LoadInformationProfile();
        }

        private void LoadInformationProfile()
        {
            byte[] imageData;
            using (var dbcontext = new ApplicationContext())
            {
                User user = dbcontext.Users.Where(u => u.UsersId == CurrentUser.UserId).FirstOrDefault();
                if (user != null)
                {
                    UsernameTextBlock.Text = user.UsersName;
                    EmailTextBlock.Text = user.UsersEmail;
                    byte[] imageBytes = user.UsersPhoto;
                    if (imageBytes != null)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(imageBytes);
                        bitmap.EndInit();

                        // Показати зображення на WPF
                        MyImage.Source = bitmap;
                    }
                    else
                    {
                        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                        string imagePath = System.IO.Path.Combine(projectDirectory, "images", "My_Photo.png");
                        byte[] imageBytesDefault = File.ReadAllBytes(imagePath);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(imageBytesDefault);
                        bitmap.EndInit();
                        MyImage.Source = bitmap;
                    }

                }
            }
        }

        static string ConvertByteaToString(byte[] byteaValue)
        {
         
            string stringValue = BitConverter.ToString(byteaValue).Replace("-", "");
            return stringValue;
        }

        static byte[] ConvertByteaToBytes(string byteaData)
        {
      
            byteaData = byteaData.Replace("\\x", "");
            string[] bytePairs = SplitString(byteaData, 2);

         
            byte[] byteArray = new byte[bytePairs.Length];
            for (int i = 0; i < bytePairs.Length; i++)
            {
                byteArray[i] = Convert.ToByte(bytePairs[i], 16);
            }

            return byteArray;
        }

        static string[] SplitString(string str, int chunkSize)
        {
            int stringLength = str.Length;
            int numOfChunks = stringLength / chunkSize + (stringLength % chunkSize == 0 ? 0 : 1);
            string[] chunks = new string[numOfChunks];

            for (int i = 0; i < numOfChunks; i++)
            {
                int length = Math.Min(chunkSize, stringLength - i * chunkSize);
                chunks[i] = str.Substring(i * chunkSize, length);
            }

            return chunks;
        }


        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            Edit_Profile edit_Profile = new Edit_Profile();
            edit_Profile.Show();
            this.Close();
        }

        private void SecuritySettings_Click(object sender, RoutedEventArgs e)
        {
            Security_Setting security_Setting = new Security_Setting();
            security_Setting.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.ClearCurrentUser();
            CountOfOpenningHomePage.Count = 1;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            

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
           
        }
    }
}
