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
    public class ContactRepository : BaseRepository<ContactModel>, IRepository<ContactModel>
    {
        public ContactRepository(string connectionString):base(connectionString)
        {

        }
        protected override ContactModel PopulateRecord(SqlDataReader reader)
        {
            return new ContactModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(), 
                Surname = reader["Surname"].ToString(),
                Email = reader["Email"].ToString(),
                LinkedClientsCount = Convert.ToInt32(reader["LinkedClientsCount"])
            };
        }

        public async Task<IEnumerable<ContactModel>> GetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetAllContacts";
            cmd.CommandType = CommandType.StoredProcedure;
            return await GetRecords(cmd);
        }
        public async Task<ContactModel> GetSingle(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetSingleContact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            return await GetRecord(cmd);
        }

        public async Task Add(ContactModel entity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spAddContact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Surname", entity.Surname);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            await ExecuteComand(cmd);
        }
        public async Task Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spDeleteContact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            await ExecuteComand(cmd);
        }
    }
}
