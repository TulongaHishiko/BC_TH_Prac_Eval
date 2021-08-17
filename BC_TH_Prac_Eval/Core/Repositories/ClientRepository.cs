using BC_TH_Prac_Eval.Core.Interfaces;
using BC_TH_Prac_Eval.Domain.Entities;
using BC_TH_Prac_Eval.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Repositories
{
    public class ClientRepository : BaseRepository<ClientModel>, IRepository<ClientModel>
    {
        public ClientRepository(string connectionString) : base(connectionString)
        {

        }
        protected override ClientModel PopulateRecord(SqlDataReader reader)
        {
            return new ClientModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                ClientCode = reader["ClientCode"].ToString(),
                LinkedContactsCount = Convert.ToInt32(reader["LinkedContactsCount"])
            };
        }

        public async Task<IEnumerable<ClientModel>> GetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetAllClients";
            cmd.CommandType = CommandType.StoredProcedure;
            return await GetRecords(cmd);
        }
        public async Task<ClientModel> GetSingle(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetSingleClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            return await GetRecord(cmd);
        }

        public async Task Add(ClientModel entity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spAddClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@ClientCode", entity.ClientCode);
            await ExecuteComand(cmd);
        }
        public async Task Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spDeleteClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            await ExecuteComand(cmd);
        }
    }
}
