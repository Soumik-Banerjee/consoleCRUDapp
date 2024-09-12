using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppCRUD
{
    class Program
    {
        static string connectionString = "Server=DESKTOP-4LSUBBA;Database=SAMPLE_DB;Trusted_Connection=True;";
        static IConfigurationRoot Configuration;

        static void Main(string[] args)
        {
            // Load configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // Get the connection string from appsettings.json
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Use the connection string for CRUD operations
            Console.WriteLine("Connection String: " + connectionString);

            // Perform CRUD operations here...
            // Menu to choose CRUD operation
            Console.WriteLine("Choose CRUD Operation:");
            Console.WriteLine("1: Create");
            Console.WriteLine("2: Read");
            Console.WriteLine("3: Update");
            Console.WriteLine("4: Delete");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    UpdateEmployee();
                    break;
                case "4":
                    DeleteEmployee();
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

        // Create: Insert a new employee
        static void CreateEmployee()
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Employee Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Position: ");
            string position = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Employees (Name, Age, Position) VALUES (@name, @age, @position)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Employee created successfully.");
            }
        }

        // Read: Display all employees
        static void ReadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}, Position: {reader["Position"]}");
                    }
                }
            }
        }

        // Update: Modify an existing employee's details
        static void UpdateEmployee()
        {
            Console.Write("Enter Employee ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter New Employee Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Employee Position: ");
            string position = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Employees SET Name = @name, Age = @age, Position = @position WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Employee updated successfully.");
            }
        }

        // Delete: Remove an employee from the database
        static void DeleteEmployee()
        {
            Console.Write("Enter Employee ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Employee deleted successfully.");
            }
        }
    }
}
