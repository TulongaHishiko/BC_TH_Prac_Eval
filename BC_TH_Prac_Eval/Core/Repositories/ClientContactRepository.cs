using BC_TH_Prac_Eval.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Repositories
{
    public class ClientContactRepository:BaseRepository<ClientContactModel>
    {
        public ClientContactRepository(string connectionString) : base(connectionString)
        {

        }
        protected override ClientContactModel PopulateRecord(SqlDataReader reader)
        {
            return new ClientContactModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                ClientId = Convert.ToInt32(reader["ClientId"]),
                ContactId = Convert.ToInt32(reader["ContactId"])
            };
        }

        public async Task<IEnumerable<ClientContactModel>> GetLinkedContacts(int clientId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetLinkedContacts";
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.CommandType = CommandType.StoredProcedure;
            return await GetRecords(cmd);
        }
        public async Task LinkContact(int clientId,int contactId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spLinkContactToClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@ContactId", contactId);
            await ExecuteComand(cmd);
        }
        public async Task UnLinkContact(int clientId, int contactId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spUnlinkContactFromClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@ContactId", contactId);
            await ExecuteComand(cmd);
        }
    }
}
