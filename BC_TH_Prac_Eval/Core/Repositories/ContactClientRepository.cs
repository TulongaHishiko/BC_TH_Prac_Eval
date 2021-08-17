using BC_TH_Prac_Eval.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Repositories
{
    public class ContactClientRepository:BaseRepository<ContactClientModel>
    {
        public ContactClientRepository(string connectionString) : base(connectionString)
        {

        }
        protected override ContactClientModel PopulateRecord(SqlDataReader reader)
        {
            return new ContactClientModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                ClientId = Convert.ToInt32(reader["ClientId"]),
                ContactId = Convert.ToInt32(reader["ContactId"])
            };
        }

        public async Task<IEnumerable<ContactClientModel>> GetLinkedClients(int contactId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetLinkedClients";
            cmd.Parameters.AddWithValue("@ContactId", contactId);
            cmd.CommandType = CommandType.StoredProcedure;
            return await GetRecords(cmd);
        }
        public async Task LinkClient(int contactId,int clientId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spLinkClientToContact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@ContactId", contactId);
            await ExecuteComand(cmd);
        }
        public async Task UnLinkClient(int contactId,int clientId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spUnlinkClientFromContact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@ContactId", contactId);
            await ExecuteComand(cmd);
        }
    }
}
