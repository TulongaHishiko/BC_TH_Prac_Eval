using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Models
{
    public class ContactsVM
    {
        public ContactsVM()
        {
            Contacts = new List<ContactModel>();
            Contact = new ContactModel();
            LinkedClients = new List<ClientModel>();
        }

        public List<ContactModel> Contacts { get; set; }
        public ContactModel Contact { get; set; }
        public List<ClientModel> LinkedClients { get; set; }
    }
}
