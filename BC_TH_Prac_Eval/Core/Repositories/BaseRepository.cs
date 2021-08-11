using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        private static string ConnectionString;

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected abstract T PopulateRecord(SqlDataReader reader);

        protected async Task<IEnumerable<T>> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader =await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(PopulateRecord(reader));
                        }
                    }
                }
            } 
            return list;
        }
        protected async Task<T> GetRecord(SqlCommand command)
        {
            T item = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = PopulateRecord(reader);
                        }
                    }
                }
            }
            return item;
        }
        protected async Task ExecuteComand(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

    }

}
