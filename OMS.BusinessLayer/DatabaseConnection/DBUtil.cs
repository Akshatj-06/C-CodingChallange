using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BusinessLayer.DatabaseConnection;



namespace OMS.BusinessLayer.DatabaseConnection
{
    public static class DBUtil
    {
        // Connection string - update with your actual database details
        private static string connectionString = "Server=DEUSEXMACHINA;Database=OrderManagementSystem;Integrated Security=true;";

        // Method to get a database connection
        public static SqlConnection GetDBConn()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open(); // Open the connection
                return connection;
            }
            catch (Exception ex)
            {
                // Handle exceptions (consider logging the error)
                throw new Exception("Could not establish a connection to the database.", ex);
            }
        }
    }
}
