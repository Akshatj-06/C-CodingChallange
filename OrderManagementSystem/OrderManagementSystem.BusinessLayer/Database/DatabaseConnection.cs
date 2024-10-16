using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OrderManagementSystem.BusinessLayer.Database
{
    public static class DatabaseConnection
    {
        // Method to get the connection from the connection string
        public static SqlConnection GetConnection()
        {
            // Fetch the connection string from App.config by name
            string connectionString = ConfigurationManager.ConnectionStrings["OrderManagementDB"].ConnectionString;
            // Create and return the SQL connection
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
