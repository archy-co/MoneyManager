using Microsoft.VisualStudio.TestPlatform.TestHost;
using Npgsql;
using ProgramSoftwaringTask3;

namespace TestProject2
{
    public class Tests
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=test_db;Include Error Detail=true;";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FillUserTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                
                connection.Open();
                ProgramSoftwaringTask3.Program.FillUserTableWithRandomData(connection, 10, 20);

               
                int rowCount = ProgramSoftwaringTask3.Program.GetRowCount(connection, "users");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(20));
            }
        }

        [Test]
        public void FillAccountTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                

                connection.Open();

               
                ProgramSoftwaringTask3.Program.FillAccountTableWithRandomData(connection, 10, 20); 

              
                int rowCount = ProgramSoftwaringTask3.Program.GetRowCount(connection, "accounts");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(20));
            }
        }

        [Test]
        public void FillGoalTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
               
                connection.Open();

               
                ProgramSoftwaringTask3.Program.FillGoalTableWithRandomData(connection, 3, 5); 

        
                int rowCount = ProgramSoftwaringTask3.Program.GetRowCount(connection, "goals");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(3).And.LessThanOrEqualTo(5));
            }
        }

        [Test]
        public void FillTransactionTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                
                connection.Open();

             
                ProgramSoftwaringTask3.Program.FillTransactionTableWithRandomData(connection, 4, 7); 
                
                int rowCount = ProgramSoftwaringTask3.Program.GetRowCount(connection, "transactions");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(4).And.LessThanOrEqualTo(7));
            }
        }


    }
}