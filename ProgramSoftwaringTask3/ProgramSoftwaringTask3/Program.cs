using Npgsql;
using System.Data.SqlClient;
using Faker;
using System.Security.Cryptography;


namespace ProgramSoftwaringTask3
{
    public class Program
    {
        private static void Main(string[] args)
        {

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=test_db;Include Error Detail=true;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM users";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                            }
                        }
                    }
                }
                FillUserTableWithRandomData(connection, 30, 50);
                FillAccountTableWithRandomData(connection, 30, 50);
                FillGoalTableWithRandomData(connection, 30, 50);
                FillTransactionTableWithRandomData(connection, 30, 50);
                connection.Close();
            }
        }

        public static void FillUserTableWithRandomData(NpgsqlConnection connection, int minRecords, int maxRecords)
        {
            Random random = new Random();
            int numberOfRecords = random.Next(minRecords, maxRecords + 1);
            int rowCount = GetRowCount(connection, "users");

            using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO users VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    // Generate a unique primary key value
                    int id;
                    do
                    {
                        id = rowCount + 1 + i;
                    } while (IsPrimaryKeyInUse(connection, "users", id));

                    string name = Faker.Name.First();
                    string phoneNumber = Faker.Phone.Number();
                    string password = "Test" + $"{id}";
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
                    byte[] hash = pbkdf2.GetBytes(20);
                    string password_hash = Convert.ToBase64String(hash);
                    string password_salt = Convert.ToBase64String(salt);
                    string email = Faker.Internet.Email();
                    byte[] photo = GenerateRandomData(10);

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@value1", id);
                    command.Parameters.AddWithValue("@value2", name);
                    command.Parameters.AddWithValue("@value3", phoneNumber);
                    command.Parameters.AddWithValue("@value4", password_hash);
                    command.Parameters.AddWithValue("@value5", password_salt);
                    command.Parameters.AddWithValue("@value6", email);
                    command.Parameters.AddWithValue("@value7", photo);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void FillAccountTableWithRandomData(NpgsqlConnection connection, int minRecords, int maxRecords)
        {
            Random random = new Random();
            int numberOfRecords = random.Next(minRecords, maxRecords + 1);
            Console.WriteLine(numberOfRecords);
            int rowCount = GetRowCount(connection, "accounts");
            int usersRowCount = GetRowCount(connection, "users");
            List<int> usersIdList = GetIdList(connection, "users");

            using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO accounts VALUES (@value1, @value2, @value3)", connection))
            {
                Random random1 = new Random();
                for (int i = 0; i < numberOfRecords; i++)
                {
                    // Generate a unique primary key value
                    int id;
                    do
                    {
                        id = rowCount + 1 + i;
                    } while (IsPrimaryKeyInUse(connection, "accounts", id));

                    string title = Faker.Lorem.Sentence();
                    int user_id = usersIdList[random.Next(usersIdList.Count)];

                    command.Parameters.Clear();


                    command.Parameters.AddWithValue("@value1", id);
                    command.Parameters.AddWithValue("@value2", title);
                    command.Parameters.AddWithValue("@value3", user_id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void FillGoalTableWithRandomData(NpgsqlConnection connection, int minRecords, int maxRecords)
        {
            Random random = new Random();
            int numberOfRecords = random.Next(minRecords, maxRecords + 1);
            int rowCount = GetRowCount(connection, "goals");
            int accountsRowCount = GetRowCount(connection, "accounts");
            List<int> accountIdList = GetIdList(connection, "accounts");

            using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO goals VALUES (@value1, @value2, @value3, @value4, @value5)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    // Generate a unique primary key value
                    int id;
                    do
                    {
                        id = rowCount + 1 + i;
                    } while (IsPrimaryKeyInUse(connection, "goals", id));

                    string title = "Test" + $"{id}";
                    string descriptoin = Faker.Lorem.Sentence();
                    decimal amountToCollect = random.Next(100, 2012);
                    int account_id = accountIdList[random.Next(accountIdList.Count)];

                    command.Parameters.Clear();


                    command.Parameters.AddWithValue("@value1", id);
                    command.Parameters.AddWithValue("@value2", title);
                    command.Parameters.AddWithValue("@value3", descriptoin);
                    command.Parameters.AddWithValue("@value4", amountToCollect);
                    command.Parameters.AddWithValue("@value5", account_id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void FillTransactionTableWithRandomData(NpgsqlConnection connection, int minRecords, int maxRecords)
        {
            Random random = new Random();
            int numberOfRecords = random.Next(minRecords, maxRecords + 1);
            int rowCount = GetRowCount(connection, "transactions");
            int accountsRowCount = GetRowCount(connection, "accounts");
            List<int> accountIdList = GetIdList(connection, "accounts");

            using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO transactions VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    // Generate a unique primary key value
                    int id;
                    do
                    {
                        id = rowCount + 1 + i;
                    } while (IsPrimaryKeyInUse(connection, "transactions", id));

                    int type = random.Next(1, 4);
                    int fromAccountId = accountIdList[random.Next(accountIdList.Count)];
                    accountIdList.Remove(fromAccountId);
                    int toAccountId = accountIdList[random.Next(accountIdList.Count)];
                    string descriptoin = Faker.Lorem.Sentence();
                    decimal sum = random.Next(100, 2012);
                    DateTime date = DateTime.Now;

                    command.Parameters.Clear();


                    command.Parameters.AddWithValue("@value1", id);
                    command.Parameters.AddWithValue("@value2", type);
                    command.Parameters.AddWithValue("@value3", fromAccountId);
                    command.Parameters.AddWithValue("@value4", toAccountId);
                    command.Parameters.AddWithValue("@value5", descriptoin);
                    command.Parameters.AddWithValue("@value6", sum);
                    command.Parameters.AddWithValue("@value7", date);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static int GetRowCount(NpgsqlConnection connection, string tableName)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT count(*) " +
                $"FROM {tableName}", connection))
            {

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public static List<int> GetIdList(NpgsqlConnection connection, string tableName)
        {
            List<int> idList = new List<int>();

            using (NpgsqlCommand command = new NpgsqlCommand(
                $"SELECT {tableName}_id FROM {tableName}", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0); // Отримати значення id
                        idList.Add(id); // Додати id до списку
                    }
                }
            }

            return idList;
        }


        public static bool IsPrimaryKeyInUse(NpgsqlConnection connection, string tableName, int primaryKeyValue)
        {
            using (NpgsqlCommand command = new NpgsqlCommand($"SELECT 1 FROM {tableName} WHERE {tableName}_id = @id", connection))
            {
                command.Parameters.AddWithValue("@id", primaryKeyValue);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    return reader.Read(); // If a row is found, the primary key is in use
                }
            }
        }

        public static byte[] GenerateRandomData(int size)
        {
            byte[] buffer = new byte[size];
            new Random().NextBytes(buffer);
            return buffer;
        }
    }
}