using Microsoft.EntityFrameworkCore;
using MoneyManagerApp;
using MoneyManagerApp.DAL.Helpers;
using MoneyManagerApp.Presentation;
using MoneyManagerApp.Presentation.Models;
using Moq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Test1
{

    public class UnitTest1
    {
        //MainWindow_LoginButton_Click_ValidCredentials_SuccessfulLogin
        [Fact]
        public void CanConnectToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=moneymanager;Username=postgres;Password=1212").Options;

            // Act
            using (var context = new ApplicationContext(options))
            {
                // Assert
                Assert.True(context.Database.CanConnect());
            }
        }





        [Fact]
        public void TestUserRegistration()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=moneymanager;Username=postgres;Password=1212")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                var signUpWindow = new Sing_Up(); // Створення екземпляру вашого вікна SignUp

                // Задайте дані, які ви хочете перевірити
                signUpWindow.SetUsernameTextBox("testUsername");
                signUpWindow.SetEmailOrPhoneTextBox("test@example.com");
                signUpWindow.SetPasswordTextBox("testPassword");

                // Act - виклик методу SignUpButton_Click
                signUpWindow.SignUpButton_Click(null, null);



                var user = context.Users.FirstOrDefault(u => u.UsersName == "testUsername");
                Assert.NotNull(user); // Ensure the user exists after registration
                Assert.Equal("test@example.com", user.UsersEmail); // Ensure the correct email was set during registration


                /*var signUpHandler = new SignUpWindowHandler(context);
                signUpHandler.SetUsername("Ostap");
                signUpHandler.SetEmailOrPhone("79ostap@ukr.net");
                signUpHandler.SetPassword("1212");

                // Act
                signUpHandler.ClickSignUpButton();*/

                // Assert
                // Додайте перевірки залишкових дій після реєстрації користувача
            }
        }

        /* var signUpWindow = new Sing_Up();

         signUpWindow.SetUsernameTextBox("ostap"); // Set username for registration
         signUpWindow.SetEmailOrPhoneTextBox("test@example.com"); // Set email or phone number for registration
         signUpWindow.SetPasswordText("test_password"); // Set password for registration

         signUpWindow.SignUpButton_Click(null, null);
*/
        // Assert
        // Check if the user was successfully registered
        /*var user = context.Users.FirstOrDefault(u => u.UsersName == "ostap");
        Assert.NotNull(user); // Ensure the user exists after registration
         Assert.Equal("test@example.com", user.UsersEmail); // Ensure the correct email was set during registration*/
        // Add more assertions as needed for other user properties or scenarios
    
        














        /*[WpfFact]
        public void Test1sdffsfsd()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=moneymanager;Username=postgres;Password=1212")
                .Options;

            *//*using (var context = new ApplicationContext(options))
            {
                // Ensure you have a user with given credentials in your test database
                var user = new User
                {
                    UsersName = "ostap",
                    PasswordHash = "hashed_password", // Replace with hashed password from your database
                                                      // Other user properties...
                };
                context.Users.Add(user);
                context.SaveChanges();
            }*//*

            // Act - Simulate the button click
            using (var context = new ApplicationContext(options))
            {
                var mainWindow = new MainWindow(context);
                mainWindow.SetUsernameOrEmailText("ostap");
                mainWindow.SetPasswordText("1212");

                // Simulate login button click
                mainWindow.LoginButton_Click(null, null);

                // Assert - Check if the Home window is opened after successful login
                var homeWindow = mainWindow.OwnedWindows.OfType<Home>().FirstOrDefault();
                Assert.NotNull(homeWindow); // Check if Home window is not null after login
            }
        }*/


        /*[Fact]
        public void Login_Successful_If_Credentials_Are_Correct()
        {
            // Arrange
            var mockDb = new Mock<ApplicationContext>();
            var users = new[]
            {
                new User { UsersId = 1, UsersName = "testUser", PasswordSalt = "someSalt", PasswordHash = "hashedPassword" }
                // Додайте більше користувачів, якщо потрібно
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockDb.Setup(db => db.Users).Returns(mockDbSet.Object);

            var mainWindow = new MainWindow { db = mockDb.Object };

            // Act
            mainWindow.SetUsernameOrEmailText("testUser");
            mainWindow.SetPasswordText("password");
            mainWindow.LoginButton_Click(null, null);

            // Assert
            Assert.NotNull(CurrentUser.GetCurrentUser());
            // Додайте інші перевірки, які підтверджують успішний вхід
        }*/




    }
}