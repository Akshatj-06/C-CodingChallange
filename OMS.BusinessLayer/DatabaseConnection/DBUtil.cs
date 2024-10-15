using System;
using System.Data.SqlClient;

namespace OMS.BusinessLayer.DatabaseConnection
{
    public static class DBUtil
    {
        // Define your connection string here
        private static readonly string connectionString = "Server=DEUSEXMACHINA;Database=OrderManagementSystem;Trusted_Connection=True;";

        // Method to get a database connection
        public static SqlConnection GetDBConn()
        {
            try
            {
                // Create a new SqlConnection instance
                var connection = new SqlConnection(connectionString);

                // Open the connection
                connection.Open();

                return connection; // Return the open connection
            }
            catch (SqlException ex)
            {
                // Handle any exceptions (you can throw a custom exception or log the error)
                throw new Exception("Could not establish a connection to the database.", ex);
            }
        }
    }
}
