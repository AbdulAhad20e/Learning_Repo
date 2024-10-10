using System;
using System.IO;
using System.Text.Json;
using Microsoft.Data.SqlClient;
namespace HospitalSystem
{
    public class DataAccess
    {
        SqlConnection connection;
      
        public SqlConnection Connection
        {
            get
            {
                return connection;
            }
           
        }

        public SqlDataReader ExecuteQuery(string query, bool isReading, params SqlParameter[] Ps)
        {
            try
            {
                string connectionString =
    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalManagmentSystem;Integrated Security=True";
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                for(int i =0; i< Ps.Length; i++)
                {
                    command.Parameters.Add(Ps[i]);
                }
                if (!isReading)
                {
                    command.ExecuteNonQuery();
                    return null;
                }
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch 
            {
                // I am Handling Exception here
                // as Hospital DAL is not any stream dependent therefore not printing anything on console
            }

            return null;
        }

        
 
    }
}
