using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace ReadSQLDotNet5
{
    public class SyncDB
    {
        string connectionString;
        SqlConnection conn;
        public SyncDB(IConfigurationRoot c)
        {
            connectionString = c.GetSection("Data:ConnectionString").Value;
            try
            {
               conn = new SqlConnection(connectionString);
               Console.WriteLine("Opening the database connection.");
               conn.Open();
            }
            catch (Exception ex)
            {
               Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void FindAll()
        {
            string cmdText = "SELECT c1.Id, FirstName, LastName, c2.Name as CityName, s.Name as StateName FROM Contact c1 JOIN City c2 ON (c2.Id = c1.CityId) JOIN State s ON (s.Id = c2.StateId) WHERE c1.Active=1";
            int cnt = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("Row ({0}): {1}|{2}|{3}|{4}|{5}",
                                cnt, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                            cnt++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void FindByPK(int id)
        {
            string cmdText = "SELECT c1.Id, FirstName, LastName, c2.Name as CityName, s.Name as StateName FROM Contact c1 JOIN City c2 ON (c2.Id = c1.CityId) JOIN State s ON (s.Id = c2.StateId) WHERE c1.Active=1 AND c1.Id = @pk";

            try
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@pk", id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("Row: {0}|{1}|{2}|{3}|{4}",
                                reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void Close()
        {
            if (conn != null)
            {
                Console.WriteLine("Closing the database connection.");
                conn.Close();
            }
        }
    }
}
