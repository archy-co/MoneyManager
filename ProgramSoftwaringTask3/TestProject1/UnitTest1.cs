using Npgsql;
using ProgramSoftwaringTask3;

namespace TestProject1
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
                // ...Initialize the connection, create a transaction (if needed), etc.

                // Call the method being tested
                Program.FillUserTableWithRandomData(connection, 10, 20);

                // Assert the expected outcome or perform assertions on the database state
                // For example, check if the user table has been populated with the expected number of records
                int rowCount = Program.GetRowCount(connection, "users");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(20));
            }
        }

        [Test]
        public void FillAccountTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                // Arrange: Open the connection and prepare any necessary test data or conditions

                connection.Open();

                // Act: Call the method being tested
                Program.FillAccountTableWithRandomData(connection, 10, 20); // Adjust the range as needed

                // Assert: Verify the expected outcome or state after the method call
                int rowCount = Program.GetRowCount(connection, "accounts");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(20));
            }
        }

        [Test]
        public void FillGoalTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                // Arrange: Open the connection and prepare any necessary test data or conditions
                connection.Open();

                // Act: Call the method being tested
                Program.FillGoalTableWithRandomData(connection, 3, 5); // Adjust the range as needed

                // Assert: Verify the expected outcome or state after the method call
                int rowCount = Program.GetRowCount(connection, "goals");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(3).And.LessThanOrEqualTo(5));
            }
        }

        [Test]
        public void FillTransactionTableWithRandomData_Success()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                // Arrange: Open the connection and prepare any necessary test data or conditions
                connection.Open();

                // Act: Call the method being tested
                Program.FillTransactionTableWithRandomData(connection, 4, 7); // Adjust the range as needed

                // Assert: Verify the expected outcome or state after the method call
                int rowCount = Program.GetRowCount(connection, "transactions");
                Assert.That(rowCount, Is.GreaterThanOrEqualTo(4).And.LessThanOrEqualTo(7));
            }
        }

        [Test]
        public void IsPrimaryKeyInUse_AlreadyExists_ReturnsTrue()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                // Arrange: Insert a record with a known primary key value into the table being checked
                int primaryKeyValue = 1; // Replace this with an existing primary key value in your database
                

                // Act: Call the IsPrimaryKeyInUse method
                bool isInUse = Program.IsPrimaryKeyInUse(connection, "tableName", primaryKeyValue); // Replace "tableName"

                // Assert: Verify the method returns true since the primary key is in use
                Assert.IsTrue(isInUse);
            }
        }
    }
}