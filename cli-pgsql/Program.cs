using System;
using Npgsql;

namespace cli_pgsql
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder() { 
            ConnectionString = "User ID=docker;Password=XdccDa85_JK;Server=localhost;Port=5432;Database=docker;Integrated Security=true;Pooling=false;CommandTimeout=300"};
            var Db = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
            Db.Open();

            var sqlCreate = "CREATE TABLE currencies(id SERIAL PRIMARY KEY, name VARCHAR(3))";
            var sqlInsert = "INSERT INTO currencies (name) VALUES (@n)"; 
            var sqlQuery = "SELECT id, name FROM currencies LIMIT @lim";

            using (var cmd = new NpgsqlCommand(sqlCreate, Db)) 
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new NpgsqlCommand(sqlInsert, Db)) 
            {
                cmd.Parameters.AddWithValue("n", "CHF"); 
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("n", "USD"); 
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new NpgsqlCommand(sqlQuery, Db)) 
            {
                cmd.Parameters.AddWithValue("lim", 1); 
                var dr = cmd.ExecuteReader(); 
                while (dr.Read())
                {
                    Console.WriteLine(dr["name"]); 
                }
            }

        }
    }
}
