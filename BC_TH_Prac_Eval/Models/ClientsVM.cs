using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Models
{
    public class ClientsVM
    {
        public ClientsVM()
        {
            Clients = new List<ClientModel>();
            LinkedContacts = new List<ContactModel>();
            Client = new ClientModel();
        }

        public List<ClientModel> Clients { get; set; }
        public ClientModel Client { get; set; }
        public List<ContactModel> LinkedContacts { get; set; }
    }
}
